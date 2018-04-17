using System.Threading.Tasks;

namespace ToucanTesting.Data
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}