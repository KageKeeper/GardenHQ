using GardenManager.DAL.DataContexts;
using GardenManager.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GardenManager.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        protected string _connectionString;
        private GardenContext _context;

        public UnitOfWork()
        {
            this._connectionString = "DefaultConnection";
        }

        public UnitOfWork(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public GardenContext DbContext
        {
            get
            {
                if (_context == null)
                {
                    _context = new GardenContext(_connectionString);
                }
                return _context;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
