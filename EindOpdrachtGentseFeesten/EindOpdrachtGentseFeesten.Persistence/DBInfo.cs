using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindOpdrachtGentseFeesten.Persistence
{
    internal static class DBInfo
    {
        public const string ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=GentseFeestenEvents;Integrated Security=True;Encrypt=False";
        public const string EvenementTableName = "GentseFeestenInt";
    }
    
}
