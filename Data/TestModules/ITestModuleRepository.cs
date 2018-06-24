using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Models;

namespace ToucanTesting.Data
{
    public interface ITestModuleRepository
    {
        Task<TestModule> Get (long testModuleId);
        Task<List<TestModule>> GetAll(long testSuiteId, DateTime? beforeDate, bool? isReport);
        void Update(TestModule testModule);
        Task<List<TestModule>> Sort (TestModule fromModule, long targetId);
        void Add (TestModule testModule);
        void Remove (TestModule testModule);
    }
}