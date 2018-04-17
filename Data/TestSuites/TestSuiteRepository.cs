using ToucanTesting.Models;
using ToucanTesting.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ToucanTesting.Controllers.TestSuites;
using System.Linq;

namespace ToucanTesting.Data
{
    public class TestSuiteRepository : ITestSuiteRepository
    {
        private readonly TucanDbContext _context;
        public TestSuiteRepository(TucanDbContext context)
        {
            this._context = context;

        }
        public async Task<TestSuite> GetSuite(long testSuiteId)
        {
            return await _context.TestSuites.SingleOrDefaultAsync(s => s.Id == testSuiteId);
        }

        public async Task<List<TestSuite>> GetSuites()
        {
            return await _context.TestSuites.Where(s => s.IsEnabled).ToListAsync();
        }

        public void Add(TestSuite testSuite)
        {
            testSuite.IsEnabled = true;
            _context.TestSuites.Add(testSuite);
        }

        public void Update(TestSuite testSuite)
        {
            _context.TestSuites.Update(testSuite);
        }

        public void Remove(TestSuite testSuite)
        {
            testSuite.IsEnabled = false;
            _context.TestSuites.Update(testSuite);
        }
    }
}