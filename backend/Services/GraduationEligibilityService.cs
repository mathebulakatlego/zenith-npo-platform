using Microsoft.EntityFrameworkCore;
using Zenith.Api.Data;

namespace Zenith.Api.Services;

public class GraduationEligibilityService
{
    private readonly ZenithDbContext _context;

    public GraduationEligibilityService(ZenithDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsEligibleAsync(int learnerId)
    {
        var learner = await _context.Learners
            .Include(l => l.Application)
            .FirstOrDefaultAsync(l => l.Id == learnerId);

        if (learner == null)
        {
            return false;
        }

        var programmeId = learner.Application.ProgrammeId;

        var moduleCount = await _context.Modules
            .CountAsync(m => m.ProgrammeId == programmeId);

        var completedModuleCount = await _context.Progress
            .CountAsync(p =>
                p.LearnerId == learnerId &&
                p.Module.ProgrammeId == programmeId &&
                p.Status == "Completed");

        var averageMark = await _context.Results
            .Where(r =>
                r.LearnerId == learnerId &&
                r.Module.ProgrammeId == programmeId)
            .Select(r => (decimal?)r.Mark)
            .AverageAsync() ?? 0;

        return completedModuleCount == moduleCount &&
               averageMark >= 70;
    }
}