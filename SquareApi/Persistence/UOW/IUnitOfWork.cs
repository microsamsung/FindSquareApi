using SquareApi.Core.Service;

namespace SquareApi.Persistence.UOW
{
    public interface IUnitOfWork
    {

        IPointService PointService { get; }

        void Commit();
    }
}
