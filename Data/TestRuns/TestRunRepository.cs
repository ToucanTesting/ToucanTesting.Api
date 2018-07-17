using ToucanTesting.Models;
using ToucanTesting.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToucanTesting.Controllers.TestSuites;
using System.Linq;
using System;

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
                .Include(r => r.TestIssues)
                .SingleOrDefaultAsync(r => r.Id == id);
            }
            return await _context.TestRuns
            .Include(r => r.TestSuite)
            .SingleOrDefaultAsync(r => r.Id == id);
        }
        public async Task<List<TestRun>> GetPage(int pageNumber, int pageSize, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return await _context.TestRuns
                    .OrderByDescending(r => r.CreatedAt)
                    .Skip(pageNumber * pageSize - pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await _context.TestRuns
                .Where(r => r.Name.Contains(searchText))
                .OrderByDescending(r => r.CreatedAt)
                .Skip(pageNumber * pageSize - pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetPageCount(int pageSize, string searchText)
        {
            int result = (string.IsNullOrEmpty(searchText)) ?
                await _context.TestRuns.CountAsync() :
                await _context.TestRuns.Where(r => r.Name.Contains(searchText)).CountAsync();
            int fullPages = result / pageSize;
            int lastPage = (result % pageSize != 0) ? 1 : 0;

            return fullPages + lastPage;
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