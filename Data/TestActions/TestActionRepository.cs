using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ToucanTesting.Data
{
    public class TestActionRepository : ITestActionRepository
    {
        private readonly ToucanDbContext _context;
        public TestActionRepository(ToucanDbContext context)
        {
            this._context = context;
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

        public async Task<List<TestAction>> Sort(TestAction fromAction, long targetActionId)
        {
            var testActions = await _context.TestActions.Where(a => a.TestCaseId == fromAction.TestCaseId).OrderBy(a => a.Sequence).ToListAsync();
            var target = testActions.SingleOrDefault(t => t.Id == targetActionId);
            var from = testActions.SingleOrDefault(t => t.Id == fromAction.Id);

            using (var db = _context.Database.BeginTransaction())
            {
                // normalize
                foreach (var a in testActions)
                {
                    a.Sequence = testActions.IndexOf(a) + 1;
                }

                var start = testActions.IndexOf(target);

                if (from.Sequence > target.Sequence)
                {
                    for (var i = start; i < testActions.Count - 1; i++)
                    {
                        testActions[i].Sequence = testActions[i].Sequence + 1;
                    }
                    from.Sequence = target.Sequence - 1;
                }
                else
                {
                    for (var i = start; i > 0; i--)
                    {
                        testActions[i].Sequence = testActions[i].Sequence - 1;
                    }
                    from.Sequence = target.Sequence + 1;
                }

                await _context.SaveChangesAsync();
                db.Commit();
                return testActions.OrderBy(a => a.Sequence).ToList();
            }



        }

        public void Remove(TestAction testAction)
        {
            _context.TestActions.Remove(testAction);
        }
    }
}