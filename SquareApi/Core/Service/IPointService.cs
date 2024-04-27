using SquareApi.Core.Dto;
using SquareApi.Core.Model;

namespace SquareApi.Core.Service
{
    public interface IPointService
    {
        Task Add(Point point);

        Task<bool> Delete(int id);

        Task<IEnumerable<Point>> GetAll();
    }
}
