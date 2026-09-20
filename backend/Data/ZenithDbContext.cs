using Microsoft.EntityFrameworkCore;
using Zenith.Api.Models;

namespace Zenith.Api.Data;

public class ZenithDbContext : DbContext
{
    public ZenithDbContext(DbContextOptions<ZenithDbContext> options)
        : base(options)
    {
    }

    public DbSet<Programme> Programmes => Set<Programme>();
    public DbSet<Applicant> Applicants => Set<Applicant>();
    public DbSet<Application> Applications => Set<Application>();
    public DbSet<ApplicationDocument> ApplicationDocuments => Set<ApplicationDocument>();

    public DbSet<Learner> Learners => Set<Learner>();

    public DbSet<Cohort> Cohorts => Set<Cohort>();

    public DbSet<Module> Modules => Set<Module>();

    public DbSet<Progress> Progress => Set<Progress>();
    public DbSet<Result> Results => Set<Result>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>()
            .HasMany(application => application.Documents)
            .WithOne(document => document.Application)
            .HasForeignKey(document => document.ApplicationId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}