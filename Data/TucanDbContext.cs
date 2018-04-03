using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TucanTesting.Models;

namespace TucanTesting.Data
{
    public class TucanDbContext : DbContext
    {
        public TucanDbContext(DbContextOptions<TucanDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = now;
                }
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

        public DbSet<TestSuite> TestSuites { get; set; }
        public DbSet<TestModule> TestModules { get; set; }
        public DbSet<TestRun> TestRuns { get; set; }
        public DbSet<TestCase> TestCases { get; set; }
        public DbSet<TestResult> TestResults { get; set; }

        // Necesssary???
        public DbSet<TestCondition> TestConditions { get; set; }
        public DbSet<TestAction> TestActions { get; set; }
        public DbSet<ExpectedResult> ExpectedResults { get; set; }
        public DbSet<TestIssue> TestIssues { get; set; }
    }
}