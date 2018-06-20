using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ToucanTesting.Interfaces;
using System;

namespace ToucanTesting.Data
{
    public class TestActionRepository : BaseRepository, ITestActionRepository
    {
        public TestActionRepository(ToucanDbContext context) : base(context)
        {
        }

        public async Task<TestAction> Get(long id)
        {
            return await _context.TestActions.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<TestAction>> GetAll(long testCaseId)
        {
            return await _context.TestActions
                .Where(a => a.TestCaseId == testCaseId)
                .OrderBy(a => a.Sequence)
                .ToListAsync();
        }

        public void Add(TestAction testAction)
        {
            _context.TestActions.Add(testAction);
        }

        public void Update(TestAction testAction)
        {
            _context.TestActions.Update(testAction);
        }

        public async Task<List<TestAction>> Sort(TestAction fromAction, long targetId)
        {
            var testActions = await _context.TestActions.Where(a => a.TestCaseId == fromAction.TestCaseId).OrderBy(a => a.Sequence).ToListAsync();
            var origin = testActions.SingleOrDefault(t => t.Id == fromAction.Id);
            var target = testActions.SingleOrDefault(t => t.Id == targetId);

            var result = await SortBySequence(testActions, origin, target);
            return result.ConvertAll(x => (TestAction)x);
        }

        public void Remove(TestAction testAction)
        {
            _context.TestActions.Remove(testAction);
        }
    }
}