using GardenManager.DAL.DataContexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenManager.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        GardenContext DbContext { get; }
        int Save();
    }
}
