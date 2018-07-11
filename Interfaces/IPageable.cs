using System.Threading.Tasks;

namespace ToucanTesting.Api.Interfaces
{
    public interface IPageable
    {
         Task<int> GetPageCount(int pageSize, string searchText);
    }
}