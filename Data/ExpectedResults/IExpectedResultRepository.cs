using System.Collections.Generic;
using System.Threading.Tasks;
using ToucanTesting.Models;

namespace ToucanTesting.Data
{
    public interface IExpectedResultRepository
    {
        Task<ExpectedResult> Get (long id);
        Task<List<ExpectedResult>> GetAll(long testCaseId);
        void Add (ExpectedResult expectedResult);
        void Update (ExpectedResult expectedResult);
        Task<List<ExpectedResult>> Sort (ExpectedResult fromExpectedResult, long targetId);
        void Remove (ExpectedResult expectedResult);
    }
}