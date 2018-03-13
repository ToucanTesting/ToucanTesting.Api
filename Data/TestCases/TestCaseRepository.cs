using TucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace TucanTesting.Data
{
    public class TestCaseRepository : ITestCaseRepository
    {
        private readonly TucanDbContext _context;
        public TestCaseRepository(TucanDbContext context)
        {
            this._context = context;
        }

        public async Task<TestCase> Get(long id, bool includeRelated = true)
        {
            if (includeRelated)
            {
                return await _context.TestCases
                .Include(c => c.TestActions)
                .Include(c => c.TestConditions)
                .Include(c => c.ExpectedResults)
                .SingleOrDefaultAsync(c => c.Id == id);
            }
            return await _context.TestCases
            .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<TestCase>> GetAll(long testModuleId, DateTime? beforeDate)
        {
            if (beforeDate.HasValue)
            {
                return await _context.TestCases
                .Where(c => c.CreatedAt < beforeDate && c.TestModuleId == testModuleId)
                .ToListAsync();
            }
            return await _context.TestCases
                .Where(c => c.IsEnabled == true && c.TestModuleId == testModuleId)
                .ToListAsync();
        }

        public async Task<List<TestCase>> GetIssues()
        {
            return await _context.TestCases
            .Where(c => !string.IsNullOrEmpty(c.BugId))
            .ToListAsync();
        }

        public void Add(TestCase testCase)
        {
            _context.TestCases.Add(testCase);
        }

        public void Update(TestCase testCase)
        {
            _context.TestCases.Update(testCase);
        }


        public void Remove(TestCase testCase)
        {
            testCase.IsEnabled = false;
            _context.TestCases.Update(testCase);
        }
    }
}