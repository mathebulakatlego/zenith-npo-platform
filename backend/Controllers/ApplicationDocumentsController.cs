
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;
using Zenith.Api.DTOs;
using Zenith.Api.Models;

namespace Zenith.Api.Controllers;

[ApiController]
[Route("api/applications/{applicationId:int}/documents")]
public class ApplicationDocumentsController : ControllerBase
{
    private const long MaxFileSize = 10 * 1024 * 1024;

    private static readonly string[] AllowedDocumentTypes =
        ["ID", "CV", "Qualification"];

    private static readonly Dictionary<string, string> AllowedExtensions =
        new(StringComparer.OrdinalIgnoreCase)
        {
            [".pdf"] = "application/pdf",
            [".png"] = "image/png",
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg"
        };

    private readonly ZenithDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ApplicationDocumentsController(
        ZenithDbContext context,
        IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApplicationDocumentResponseDto>>> GetDocuments(
        int applicationId)
    {
        var applicationExists = await _context.Applications
            .AnyAsync(application => application.Id == applicationId);

        if (!applicationExists)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Application not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        var documents = await _context.ApplicationDocuments
            .Where(document => document.ApplicationId == applicationId)
            .OrderBy(document => document.UploadedAt)
            .Select(document => ToResponse(document))
            .ToListAsync();

        return Ok(documents);
    }

    [HttpPost]
    [RequestSizeLimit(MaxFileSize)]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<ApplicationDocumentResponseDto>> UploadDocument(
        int applicationId,
        [FromForm] string documentType,
        IFormFile? file)
    {
        var request = new ApplicationDocumentUploadDto
        {
            DocumentType = documentType ?? string.Empty,
            File = file
        };

        var applicationExists = await _context.Applications
            .AnyAsync(application => application.Id == applicationId);

        if (!applicationExists)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Application not found",
                Status = StatusCodes.Status404NotFound
            });
        }

        var normalizedDocumentType = request.DocumentType.Trim();

        if (!AllowedDocumentTypes.Contains(
                normalizedDocumentType,
                StringComparer.OrdinalIgnoreCase))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid document type",
                Detail = "DocumentType must be ID, CV, or Qualification.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        if (request.File is null || request.File.Length == 0)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "File is required",
                Status = StatusCodes.Status400BadRequest
            });
        }

        if (request.File.Length > MaxFileSize)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "File is too large",
                Detail = "Files must be 10 MB or smaller.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var extension = Path.GetExtension(request.File.FileName);

        if (!AllowedExtensions.TryGetValue(
                extension,
                out var expectedContentType) ||
            !string.Equals(
                request.File.ContentType,
                expectedContentType,
                StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Unsupported file type",
                Detail = "Only PDF, PNG, and JPEG files are supported.",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var originalFileName = Path.GetFileName(request.File.FileName);

        if (string.IsNullOrWhiteSpace(originalFileName) ||
            originalFileName.Length > 255)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Invalid file name",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var storedFileName =
            $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";

        var relativeFilePath = Path.Combine(
            "uploads",
            "applications",
            applicationId.ToString(),
            storedFileName);

        var absoluteDirectory = Path.Combine(
            _environment.ContentRootPath,
            "uploads",
            "applications",
            applicationId.ToString());

        var absoluteFilePath = Path.Combine(
            absoluteDirectory,
            storedFileName);

        Directory.CreateDirectory(absoluteDirectory);

        await using (var stream = new FileStream(
            absoluteFilePath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.None))
        {
            await request.File.CopyToAsync(stream);
        }

        var document = new ApplicationDocument
        {
            ApplicationId = applicationId,
            DocumentType = AllowedDocumentTypes.First(type =>
                string.Equals(
                    type,
                    normalizedDocumentType,
                    StringComparison.OrdinalIgnoreCase)),
            FileName = originalFileName,
            FilePath = relativeFilePath.Replace(
                Path.DirectorySeparatorChar,
                '/'),
            UploadedAt = DateTime.UtcNow
        };

        try
        {
            _context.ApplicationDocuments.Add(document);
            await _context.SaveChangesAsync();
        }
        catch
        {
            System.IO.File.Delete(absoluteFilePath);
            throw;
        }

        return CreatedAtAction(
            nameof(GetDocuments),
            new { applicationId },
            ToResponse(document));
    }

    private static ApplicationDocumentResponseDto ToResponse(
        ApplicationDocument document)
    {
        return new ApplicationDocumentResponseDto
        {
            Id = document.Id,
            ApplicationId = document.ApplicationId,
            DocumentType = document.DocumentType,
            FileName = document.FileName,
            UploadedAt = document.UploadedAt
        };
    }
}

