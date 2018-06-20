using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ToucanTesting.Data
{
    public class TestConditionRepository : BaseRepository, ITestConditionRepository
    {
        public TestConditionRepository(ToucanDbContext context) : base(context)
        {
        }

        public async Task<TestCondition> Get(long id)
        {
            return await _context.TestConditions.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<TestCondition>> GetAll(long testCaseId)
        {
            return await _context.TestConditions
                .Where(c => c.TestCaseId == testCaseId)
                .ToListAsync();
        }

        public void Add(TestCondition testCondition)
        {
            _context.TestConditions.Add(testCondition);
        }

        public void Update(TestCondition testCondition)
        {
            _context.TestConditions.Update(testCondition);
        }

        public async Task<List<TestCondition>> Sort(TestCondition fromCondition, long targetId)
        {
            var testConditions = await _context.TestConditions.Where(a => a.TestCaseId == fromCondition.TestCaseId).OrderBy(a => a.Sequence).ToListAsync();
            var origin = testConditions.SingleOrDefault(t => t.Id == fromCondition.Id);
            var target = testConditions.SingleOrDefault(t => t.Id == targetId);

            var result = await SortBySequence(testConditions, origin, target);
            return result.ConvertAll(x => (TestCondition)x);
        }

        public void Remove(TestCondition testCondition)
        {
            _context.TestConditions.Remove(testCondition);
        }
    }
}