using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ToucanTesting.Data
{
    public class ExpectedResultRepository : BaseRepository, IExpectedResultRepository
    {
        public ExpectedResultRepository(ToucanDbContext context) : base(context)
        {
        }

        public async Task<ExpectedResult> Get(long id)
        {
            return await _context.ExpectedResults.SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<ExpectedResult>> GetAll(long testCaseId)
        {
            return await _context.ExpectedResults
                .Where(e => e.TestCaseId == testCaseId)
                .ToListAsync();
        }

        public void Add(ExpectedResult expectedResult)
        {
            _context.ExpectedResults.Add(expectedResult);
        }

        public void Update(ExpectedResult expectedResult)
        {
            _context.ExpectedResults.Update(expectedResult);
        }

        public async Task<List<ExpectedResult>> Sort(ExpectedResult fromExpectedResult, long targetId)
        {
            var expectedResults = await _context.ExpectedResults.Where(a => a.TestCaseId == fromExpectedResult.TestCaseId).OrderBy(a => a.Sequence).ToListAsync();
            var origin = expectedResults.SingleOrDefault(t => t.Id == fromExpectedResult.Id);
            var target = expectedResults.SingleOrDefault(t => t.Id == targetId);

            var result = await SortBySequence(expectedResults, origin, target);
            return result.ConvertAll(x => (ExpectedResult)x);
        }

        public void Remove(ExpectedResult expectedResult)
        {
            _context.ExpectedResults.Remove(expectedResult);
        }
    }
}