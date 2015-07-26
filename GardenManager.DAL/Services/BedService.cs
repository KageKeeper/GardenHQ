using GardenManager.DAL.Interfaces;
using GardenManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.DAL.Services
{
    public interface IBedService : IDisposable
    {
        IEnumerable<Bed> GetBeds(
            Expression<Func<Bed, bool>> filter = null,
            Func<IQueryable<Bed>, IOrderedQueryable<Bed>> orderBy = null,
            string includeProperties = "");
        IEnumerable<Bed> GetUnassignedBeds();
        Bed GetBedByID(object id);
        void AddBed(Bed bed);
        void UpdateBed(Bed bed);
        void DeleteBed(Bed bed);

        IEnumerable<Garden> GetGardens();
    }

    public class BedService : BaseService, IBedService
    {
        private IBaseRepository<Bed> _bedRepository;

        public BedService(IUnitOfWork unitOfWork, IBaseRepository<Bed> bedRepository)
            : base(unitOfWork)
        {
            this._bedRepository = bedRepository;
        }

        public IEnumerable<Bed> GetBeds(Expression<Func<Bed, bool>> filter = null,
            Func<IQueryable<Bed>, IOrderedQueryable<Bed>> orderBy = null,
            string includeProperties = "")
        {
            return _bedRepository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<Bed> GetUnassignedBeds()
        {
            return _bedRepository.Get(b => b.AssignedToGarden == false);
        }

        public Bed GetBedByID(object id)
        {
            return _bedRepository.GetByID(id);
        }

        public void AddBed(Bed bed)
        {
            _bedRepository.Insert(bed);
        }
        public void UpdateBed(Bed bed)
        {
            _bedRepository.Update(bed);
        }

        public void DeleteBed(Bed bed)
        {
            _bedRepository.Delete(bed);
        }

        public IEnumerable<Garden> GetGardens()
        {
            return _bedRepository.Get<Garden>();
        }
    }
}
