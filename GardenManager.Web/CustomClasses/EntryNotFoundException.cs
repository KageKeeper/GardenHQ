using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GardenManager.Web.CustomClasses
{
    public class EntryNotFoundException : Exception
    {
        public EntryNotFoundException()
        {
        }

        public EntryNotFoundException(string message)
            : base(message)
        {
        }

        public EntryNotFoundException(string message, Exception inner)
            : base (message, inner)
        {
        }
    }
}