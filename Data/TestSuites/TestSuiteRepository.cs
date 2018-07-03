using ToucanTesting.Models;
using ToucanTesting.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToucanTesting.Controllers.TestSuites;
using System.Linq;

namespace ToucanTesting.Data
{
    public class TestSuiteRepository : ITestSuiteRepository
    {
        private readonly ToucanDbContext _context;
        public TestSuiteRepository(ToucanDbContext context)
        {
            this._context = context;

        }
        public async Task<TestSuite> GetSuite(long testSuiteId, bool includeChildren = false)
        {
            if (!includeChildren)
            {
                return await _context.TestSuites.SingleOrDefaultAsync(s => s.Id == testSuiteId);
            }
            return await _context.TestSuites
                .Include(s => s.TestModules)
                    .ThenInclude(m => m.TestCases)
                        .ThenInclude(c => c.TestActions)
                .Include(s => s.TestModules)
                    .ThenInclude(m => m.TestCases)
                        .ThenInclude(c => c.TestConditions)
                .Include(m => m.TestModules)
                    .ThenInclude(c => c.TestCases)
                        .ThenInclude(c => c.ExpectedResults)
                .Where(s => s.Id == testSuiteId)
                .Select(ts => new TestSuite()
                {
                    Name = ts.Name,
                    TestModules = ts.TestModules.Where(tm => tm.IsEnabled).OrderBy(tm => tm.Sequence).Select(tm => new TestModule()
                    {
                        Name = tm.Name,
                        TestCases = tm.TestCases.Where(tc => tc.IsEnabled).Select(tc => new TestCase()
                        {
                            Description = tc.Description,
                            LastTested = tc.LastTested,
                            IsAutomated = tc.IsAutomated,
                            HasCriteria = tc.HasCriteria,
                            ExpectedResults = tc.ExpectedResults.OrderBy(e => e.Sequence).ToArray(),
                            TestConditions = tc.TestConditions.OrderBy(c => c.Sequence).ToArray(),
                            TestActions = tc.TestActions.OrderBy(a => a.Sequence).ToArray()
                        }).ToArray()

                    }).ToArray()
                })
                .SingleOrDefaultAsync();
        }

        public async Task<List<TestSuite>> GetSuites()
        {
            return await _context.TestSuites.Where(s => s.IsEnabled).ToListAsync();
        }

        public void Add(TestSuite testSuite)
        {
            testSuite.IsEnabled = true;
            _context.TestSuites.Add(testSuite);
        }

        public void Update(TestSuite testSuite)
        {
            _context.TestSuites.Update(testSuite);
        }

        public void Remove(TestSuite testSuite)
        {
            testSuite.IsEnabled = false;
            _context.TestSuites.Update(testSuite);
        }
    }
}