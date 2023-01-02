using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace EindOpdrachtGentseFeesten.Persistence.ExtensionMethods
{

    public static class DbHelper
    {
        public static T GetValue<T>(this SqlDataReader sqlDataReader, string columnName, T defaultValue = default)
        {
            var value = sqlDataReader[columnName];

            if (value != DBNull.Value)
            {
                return (T)value;
            }

            return defaultValue;
        }
    }
}
