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
    public interface ISeedService : IDisposable
    {
        IEnumerable<Seed> GetSeeds(
            Expression<Func<Seed, bool>> filter = null,
            Func<IQueryable<Seed>, IOrderedQueryable<Seed>> orderBy = null,
            string includeProperties = "");
        Seed GetSeedByID(object id);
        void AddSeed(Seed garden);
        void UpdateSeed(Seed garden);
        void DeleteSeed(Seed garden);
    }

    public class SeedService : BaseService, ISeedService
    {
        private IBaseRepository<Seed> _seedRepository;

        public SeedService(IUnitOfWork unitOfWork, IBaseRepository<Seed> seedRepository)
            : base(unitOfWork)
        {
            this._seedRepository = seedRepository;
        }

        public IEnumerable<Seed> GetSeeds(Expression<Func<Seed, bool>> filter = null,
            Func<IQueryable<Seed>, IOrderedQueryable<Seed>> orderBy = null,
            string includeProperties = "")
        {
            return _seedRepository.Get(filter, orderBy, includeProperties);
        }

        public Seed GetSeedByID(object id)
        {
            return _seedRepository.GetByID(id);
        }

        public void AddSeed(Seed garden)
        {
            _seedRepository.Insert(garden);
        }
        public void UpdateSeed(Seed garden)
        {
            _seedRepository.Update(garden);
        }

        public void DeleteSeed(Seed garden)
        {
            _seedRepository.Delete(garden);
        }
    }
}
