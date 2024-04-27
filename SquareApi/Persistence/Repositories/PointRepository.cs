using SquareApi.Core.Dto;
using SquareApi.Core.Model;
using SquareApi.Core.Service;
using SquareApi.Persitence;

namespace SquareApi.Persistence.Repositories
{
    public class PointRepository : IPointService
    {
        private SquareApiContext _context;
        public PointRepository(SquareApiContext context)
        {
            _context = context;
        }

        public async Task Add(Point point)
        {
            _context.Point.Add(point);
        }

        public async Task<bool> Delete(int id)
        {
            var point = _context.Point.SingleOrDefault(p => p.Id == id);
            if (point == null)
                return false;

            _context.Point.Remove(point);
            return true;
        }

        public async Task<IEnumerable<Point>> GetAll()
        {
            return _context.Point.ToList();
        }
    }
}
