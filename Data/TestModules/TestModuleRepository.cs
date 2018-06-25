using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ToucanTesting.Data
{
    public class TestModuleRepository : BaseRepository, ITestModuleRepository
    {
        public TestModuleRepository(ToucanDbContext context) : base(context)
        {
        }

        public async Task<TestModule> Get(long testModuleId)
        {
            return await _context.TestModules.SingleOrDefaultAsync(s => s.Id == testModuleId);
        }

        public async Task<List<TestModule>> GetAll(long testSuiteId, DateTime? testRunCreation, bool? isReport)
        {
            // debug
            if (isReport.HasValue)
            {
                return await _context.TestModules
                    .Include(tm => tm.TestCases)
                    .Where(m =>
                        m.CreatedAt < testRunCreation
                        && m.TestSuiteId == testSuiteId
                        && (m.IsEnabled || m.DisabledAt > testRunCreation)) // Get Test Modules that are active, or were still active at time of Test Run Creation
                    .OrderBy(a => a.Sequence)
                    .Select(tm => new TestModule()
                    {
                        Id = tm.Id,
                        CreatedAt = tm.CreatedAt,
                        UpdatedAt = tm.UpdatedAt,
                        TestSuiteId = tm.TestSuiteId,
                        Name = tm.Name,
                        TestCases = tm.TestCases.Where(tc =>
                            tc.CreatedAt < testRunCreation
                            && (tc.IsEnabled || tc.DisabledAt > testRunCreation)).ToArray() // Get Test Cases that are active, or were still active at time of Test Run Creation
                    })
                    .ToListAsync();
            }

            else if (testRunCreation.HasValue)
            {
                return await _context.TestModules
                    .Where(m =>
                        m.CreatedAt < testRunCreation
                        && m.TestSuiteId == testSuiteId
                        && (m.IsEnabled || m.DisabledAt > testRunCreation))
                    .OrderBy(a => a.Sequence)
                    .ToListAsync();
            }

            else
            {
                return await _context.TestModules
                    .Where(m => m.IsEnabled && m.TestSuiteId == testSuiteId)
                    .OrderBy(a => a.Sequence)
                    .ToListAsync();
            }

        }

        public void Update(TestModule testModule)
        {
            _context.TestModules.Update(testModule);
        }

        public async Task<List<TestModule>> Sort(TestModule fromModule, long targetId)
        {
            var modules = await _context.TestModules.Where(a => a.TestSuiteId == fromModule.TestSuiteId && a.IsEnabled).OrderBy(a => a.Sequence).ToListAsync();
            var origin = modules.SingleOrDefault(t => t.Id == fromModule.Id);
            var target = modules.SingleOrDefault(t => t.Id == targetId);
            var result = await SortBySequence(modules, origin, target);

            return result.ConvertAll(x => (TestModule)x);
        }

        public void Add(TestModule testModule)
        {
            testModule.IsEnabled = true;
            _context.TestModules.Add(testModule);
        }

        public void Remove(TestModule testModule)
        {
            testModule.IsEnabled = false;
            testModule.DisabledAt = DateTime.UtcNow;
            _context.TestModules.Update(testModule);
        }
    }
}