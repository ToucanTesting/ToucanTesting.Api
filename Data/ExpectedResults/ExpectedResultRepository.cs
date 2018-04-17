using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ToucanTesting.Data
{
    public class ExpectedResultRepository : IExpectedResultRepository
    {
        private readonly ToucanDbContext _context;
        public ExpectedResultRepository(ToucanDbContext context)
        {
            this._context = context;
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


        public void Remove(ExpectedResult expectedResult)
        {
            _context.ExpectedResults.Update(expectedResult);
        }
    }
}