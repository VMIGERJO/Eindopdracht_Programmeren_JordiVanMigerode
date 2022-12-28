using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EindOpdrachtGentseFeesten.Domain.Model;
using EindOpdrachtGentseFeesten.Domain.Repository;
using Microsoft.Data.SqlClient;

namespace EindOpdrachtGentseFeesten.Persistence
{
    public class EvenementMapper : IEvenementRepository
    {
        
        private SqlConnection _connection;
        public EvenementMapper()
        {
            _connection = new SqlConnection(DBInfo.ConnectionString);
        }
        public List<string> GetParents(string originalParentId)
        {
            try
            {
                _connection.Open();
                List<string> ParentEvenementen = new List<string>();
                while (originalParentId != null)
                {
                    string query = $"SELECT * FROM {DBInfo.EvenementGroepTableName} Where Id = (SELECT ParentId FROM {DBInfo.EvenementTableName} Where Id = {originalParentId});";
                    SqlCommand sqlCommand = new SqlCommand(query, _connection);
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        string title = (string)reader["Title"];
                        string description = (string)reader["Description"];
                        string newParentId = (string)reader["ParentId"];
                        ParentEvenementen.Add(originalParentId);
                        originalParentId = newParentId;
                    }
                }
                return ParentEvenementen;
            }
            finally
            {
                _connection.Close();
            }
        }


    }

}

