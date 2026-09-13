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
        var learnerExists = await _context.Learners
            .AnyAsync(l => l.Id == learnerId);

        if (!learnerExists)
        {
            return false;
        }

        var moduleCount = await _context.Modules
            .CountAsync();

        var completedModuleCount = await _context.Progress
            .CountAsync(p =>
                p.LearnerId == learnerId &&
                p.Status == "Completed");

        var averageMark = await _context.Results
            .Where(r => r.LearnerId == learnerId)
            .Select(r => (decimal?)r.Mark)
            .AverageAsync() ?? 0;

        return completedModuleCount == moduleCount &&
               averageMark >= 70;
    }
}
