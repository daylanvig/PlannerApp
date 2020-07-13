using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerApp.Shared.Exceptions
{
    public class CacheExpiredException : Exception
    {
        public CacheExpiredException() : base("Cache content has expired")
        {

        }
    }
}
