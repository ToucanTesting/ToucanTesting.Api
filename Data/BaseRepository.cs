using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToucanTesting.Interfaces;

namespace ToucanTesting.Data
{
    public class BaseRepository
    {
        protected readonly ToucanDbContext _context;

        public BaseRepository(ToucanDbContext context)
        {
            _context = context;
        }
        public async Task<List<ISequential>> SortBySequence(IEnumerable<ISequential> enumerable, ISequential origin, ISequential target)
        {
            var list = enumerable.ToList();
            using (var db = _context.Database.BeginTransaction())
            {
                // normalize
                foreach (var a in list)
                {
                    a.Sequence = list.IndexOf(a) + 1;
                }

                var start = list.IndexOf(target);

                if (origin.Sequence > target.Sequence)
                {
                    for (var i = start; i < list.Count - 1; i++)
                    {
                        list[i].Sequence = list[i].Sequence + 1;
                    }
                    origin.Sequence = target.Sequence - 1;
                }
                else
                {
                    for (var i = start; i >= origin.Sequence; i--)
                    {
                        list[i].Sequence = list[i].Sequence - 1;
                    }
                    origin.Sequence = target.Sequence + 1;
                }

                await _context.SaveChangesAsync();
                db.Commit();
                return list.OrderBy(a => a.Sequence).ToList();
            }
        }
    }
}