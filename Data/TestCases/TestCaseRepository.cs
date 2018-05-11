using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ToucanTesting.Data
{
    public class TestCaseRepository : ITestCaseRepository
    {
        private readonly ToucanDbContext _context;
        public TestCaseRepository(ToucanDbContext context)
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
                .Include(c => c.TestIssues)
                .SingleOrDefaultAsync(c => c.Id == id);
            }
            return await _context.TestCases
            .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<TestCase>> GetAll(long testModuleId, DateTime? beforeDate)
        {
            // debug
            if (beforeDate.HasValue)
            {
                return await _context.TestCases
                .Where(c => c.CreatedAt < beforeDate && c.TestModuleId == testModuleId)
                .Include(c => c.TestIssues)
                .ToListAsync();
            }
            return await _context.TestCases
                .Where(c => c.IsEnabled == true && c.TestModuleId == testModuleId)
                .Include(c => c.TestIssues)
                .ToListAsync();
        }

        public async Task<List<TestIssue>> GetIssues(long testCaseId)
        {
            return await _context.TestIssues.Where(ti => ti.TestCaseId == testCaseId).ToListAsync();
        }

        public void Add(TestCase testCase)
        {
            _context.TestCases.Add(testCase);
        }

        public async Task<TestCase> Duplicate(long testCaseId)
        {
            var testCase = await _context.TestCases
            .AsNoTracking()
            .Include(c => c.TestActions)
            .Include(c => c.TestConditions)
            .Include(c => c.ExpectedResults)
            .FirstOrDefaultAsync(c => c.Id == testCaseId);

            var cloneTestCase = new TestCase()
            {
                Description = $"{testCase.Description} (copy)",
                Priority = testCase.Priority,
                TestModuleId = testCase.TestModuleId,
                IsEnabled = testCase.IsEnabled,
                IsAutomated = testCase.IsAutomated,
                LastTested = null,
                TestActions = new List<TestAction>(),
                TestConditions = new List<TestCondition>(),
                ExpectedResults = new List<ExpectedResult>()
            };

            foreach (var testAction in testCase.TestActions)
            {
                var cloneTestAction = new TestAction()
                {
                    Description = testAction.Description,
                    Sequence = testAction.Sequence
                };

                cloneTestCase.TestActions.Add(cloneTestAction);
                

            }

            foreach (var testCondition in testCase.TestConditions)
            {
                var cloneTestCondition = new TestCondition()
                {
                    Description = testCondition.Description
                };

                cloneTestCase.TestConditions.Add(cloneTestCondition);
            }

            foreach (var expectedResult in testCase.ExpectedResults)
            {
                var cloneExpectedResult = new ExpectedResult()
                {
                    Description = expectedResult.Description
                };

                cloneTestCase.ExpectedResults.Add(cloneExpectedResult);
            }

            return cloneTestCase;
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