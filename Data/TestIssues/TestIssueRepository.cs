using ToucanTesting.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ToucanTesting.Data
{
    public class TestIssueRepository : ITestIssueRepository
    {
        private readonly ToucanDbContext _context;
        public TestIssueRepository(ToucanDbContext context)
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
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<TestIssue>> GetPage(int pageNumber, int pageSize, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return await _context.TestIssues
                    .OrderByDescending(i => i.CreatedAt)
                    .Skip(pageNumber * pageSize - pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await _context.TestIssues
                .Where(i => i.Description.Contains(searchText) || i.Reference.Contains(searchText))
                .OrderByDescending(i => i.CreatedAt)
                .Skip(pageNumber * pageSize - pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetPageCount(int pageSize, string searchText)
        {
            int result = (string.IsNullOrEmpty(searchText)) ?
                await _context.TestIssues.CountAsync() :
                await _context.TestIssues.Where(r => r.Description.Contains(searchText)).CountAsync();
            int fullPages = result / pageSize;
            int lastPage = (result % pageSize != 0) ? 1 : 0;

            return fullPages + lastPage;
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