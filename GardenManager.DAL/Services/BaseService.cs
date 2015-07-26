using GardenManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.DAL.Services
{
    public interface IBaseService : IDisposable
    {
        int Save();
    }

    public class BaseService : IBaseService
    {
        protected IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public int Save()
        {
            return _unitOfWork.Save();
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
