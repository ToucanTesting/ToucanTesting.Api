using TucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TucanTesting.Data
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

        public async Task<List<TestModule>> GetAll(long testSuiteId)
        {
            return await _context.TestModules.Where(m => m.IsEnabled && m.TestSuiteId == testSuiteId).ToListAsync();
        }

        public async Task<List<TestModule>> GetAll(long testSuiteId, DateTime? beforeDate)
        {
            if (!beforeDate.HasValue)
            {
                return await _context.TestModules.Where(m => m.IsEnabled && m.TestSuiteId == testSuiteId).ToListAsync();
            }
            return await _context.TestModules.Where(m => m.CreatedAt < beforeDate && m.TestSuiteId == testSuiteId).ToListAsync();
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