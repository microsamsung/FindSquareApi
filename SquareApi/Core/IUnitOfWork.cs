using SquareApi.Core.Service;

namespace SquareApi.Core
{
    public interface IUnitOfWork
    {

        IPointService PointService { get; }

        void Commit();
    }
}
