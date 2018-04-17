using ToucanTesting.Models;
using ToucanTesting.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToucanTesting.Controllers.TestSuites;

namespace ToucanTesting.Data
{
    public class TestRunRepository : ITestRunRepository
    {
        private readonly ToucanDbContext _context;
        public TestRunRepository(ToucanDbContext context)
        {
            this._context = context;

        }
        public async Task<TestRun> Get(long id, bool includeRelated)
        {
            if (includeRelated)
            {
                return await _context.TestRuns
                .Include(r => r.TestSuite)
                .Include(r => r.TestResults)
                .SingleOrDefaultAsync(r => r.Id == id);
            }
            return await _context.TestRuns
            .Include(r => r.TestSuite)
            .SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<TestRun>> GetAll()
        {
            return await _context.TestRuns.ToListAsync();
        }

        public void Add(TestRun testRun)
        {
            _context.TestRuns.Add(testRun);
        }

        public void Update(TestRun testRun)
        {
            _context.TestRuns.Update(testRun);
        }
        public void Remove(TestRun testRun)
        {
            _context.TestRuns.Remove(testRun);
        }
    }
}