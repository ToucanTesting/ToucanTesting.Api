using TucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TucanTesting.Controllers.TestCases;
using System;

namespace TucanTesting.Data
{
    public class TestResultRepository : ITestResultRepository
    {
        private readonly TucanDbContext _context;
        public TestResultRepository(TucanDbContext context)
        {
            this._context = context;
        }

        public async Task<TestResult> Get(long id)
        {
            return await _context.TestResults.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<TestResult>> GetAll(long testRunId)
        {
            return await _context.TestResults.Where(r => r.TestRunId == testRunId).ToListAsync();
        }

        public void Add(TestResult testResult)
        {
            _context.TestResults.Add(testResult);
        }

        public void UpdateOrInsert(TestResult[] testResults)
        {
            foreach (var testResult in testResults)
            {
                if (testResult.Id == 0)
                {
                    _context.TestResults.Add(testResult);
                }
                else
                {
                    _context.TestResults.Update(testResult);
                }
            }
        }
    }
}