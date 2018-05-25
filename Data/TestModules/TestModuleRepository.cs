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
        private readonly ToucanDbContext _context;
        public TestModuleRepository(ToucanDbContext context)
        {
            this._context = context;
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
            testModule.DisabledAt = DateTime.UtcNow;
            _context.TestModules.Update(testModule);
        }
    }
}