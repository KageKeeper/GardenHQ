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
    public interface IGardenService : IBaseService, IDisposable
    {
        IEnumerable<Garden> GetGardens(
            Expression<Func<Garden, bool>> filter = null,
            Func<IQueryable<Garden>, IOrderedQueryable<Garden>> orderBy = null,
            string includeProperties = "");
        Garden GetGardenByID(object id);
        void AddGarden(Garden garden);
        void UpdateGarden(Garden garden);
        void DeleteGarden(Garden garden);

        IEnumerable<PlantHardinessZone> GetZones();
        PlantHardinessZone GetZoneByID(int id);
    }

    public class GardenService : BaseService, IGardenService
    {
        private IBaseRepository<Garden> _gardenRepository;

        public GardenService(IUnitOfWork unitOfWork, IBaseRepository<Garden> gardenRepository)
            : base (unitOfWork)
        {
            this._gardenRepository = gardenRepository;
        }

        public IEnumerable<Garden> GetGardens(Expression<Func<Garden, bool>> filter = null,
            Func<IQueryable<Garden>, IOrderedQueryable<Garden>> orderBy = null,
            string includeProperties = "")
        {
            return _gardenRepository.Get(filter, orderBy, includeProperties);
        }

        public Garden GetGardenByID(object id)
        {
            return _gardenRepository.GetByID(id);
        }

        public void AddGarden(Garden garden)
        {
            _gardenRepository.Insert(garden);
        }
        public void UpdateGarden(Garden garden)
        {
            _gardenRepository.Update(garden);
        }

        public void DeleteGarden(Garden garden)
        {
            _gardenRepository.Delete(garden);
        }

        public IEnumerable<PlantHardinessZone> GetZones()
        {
            return _gardenRepository.Get<PlantHardinessZone>();
        }

        public PlantHardinessZone GetZoneByID(int id)
        {
            return _gardenRepository.Fetch<PlantHardinessZone>().Where(z => z.Id == id).Single();
        }
    }
}
