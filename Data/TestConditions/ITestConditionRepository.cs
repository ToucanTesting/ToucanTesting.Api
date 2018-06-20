using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Models;

namespace ToucanTesting.Data
{
    public interface ITestConditionRepository
    {
        Task<TestCondition> Get (long id);
        Task<List<TestCondition>> GetAll(long testCaseId);
        void Add (TestCondition testCondition);

        void Update (TestCondition testCondition);
        Task<List<TestCondition>> Sort (TestCondition fromCondition, long targetId);

        void Remove (TestCondition testCondition);
    }
}
