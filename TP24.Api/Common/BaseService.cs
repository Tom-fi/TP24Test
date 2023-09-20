using TP24.Domain.Interfaces;

namespace TP24.Api.Common
{
    public class BaseService
    {
        protected IUnitOfWork UnitOfWork { get; set; }

        public BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
