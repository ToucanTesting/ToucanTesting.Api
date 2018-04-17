using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ToucanTesting.Data
{
    public class TestModuleRepository : ITestModuleRepository
    {
        private readonly TucanDbContext _context;
        public TestModuleRepository(TucanDbContext context)
        {
            this._context = context;
        }

        public async Task<TestModule> Get(long testModuleId)
        {
            return await _context.TestModules.SingleOrDefaultAsync(s => s.Id == testModuleId);
        }

        public async Task<List<TestModule>> GetAll(long testSuiteId, DateTime? beforeDate, bool? isReport)
        {
            if (isReport.HasValue)
            {
                return await _context.TestModules
                    .Include(tm => tm.TestCases)
                    .Where(m => m.IsEnabled && m.CreatedAt < beforeDate && m.TestSuiteId == testSuiteId)
                    .Select(tm => new TestModule()
                    {
                        Id = tm.Id,
                        CreatedAt = tm.CreatedAt,
                        UpdatedAt = tm.UpdatedAt,
                        TestSuiteId = tm.TestSuiteId,
                        Name = tm.Name,
                        TestCases = tm.TestCases.Where(tc => tc.IsEnabled).ToArray()
                    })
                    .ToListAsync();
            }
            
            else if (beforeDate.HasValue)
            {
                return await _context.TestModules
                    .Where(m => m.CreatedAt < beforeDate && m.TestSuiteId == testSuiteId)
                    .ToListAsync();
            }

            else
            {
                return await _context.TestModules
                    .Where(m => m.IsEnabled && m.TestSuiteId == testSuiteId)
                    .ToListAsync();
            }

        }
        
        public void Update(TestModule testModule)
        {
            _context.TestModules.Update(testModule);
        }

        public void Add(TestModule testModule)
        {
            testModule.IsEnabled = true;
            _context.TestModules.Add(testModule);
        }

        public void Remove(TestModule testModule)
        {
            testModule.IsEnabled = false;
            _context.TestModules.Update(testModule);
        }
    }
}