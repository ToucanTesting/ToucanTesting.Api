using TucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TucanTesting.Data
{
    public class TestIssueRepository : ITestIssueRepository
    {
        private readonly TucanDbContext _context;
        public TestIssueRepository(TucanDbContext context)
        {
            this._context = context;
        }

        public async Task<TestIssue> Get(long id)
        {
            return await _context.TestIssues
            .SingleOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<TestIssue>> GetAll()
        {
            return await _context.TestIssues
                .ToListAsync();
        }

        public void Add(TestIssue testIssue)
        {
            _context.TestIssues.Add(testIssue);
        }

        public void Update(TestIssue testIssue)
        {
            _context.TestIssues.Update(testIssue);
        }


        public void Remove(TestIssue testIssue)
        {
            _context.TestIssues.Remove(testIssue);
        }
    }
}