using Microsoft.Data.SqlClient;
using EindOpdrachtGentseFeesten.Domain.Repository;
using EindOpdrachtGentseFeesten.Domain.Model;
using System.Data;

namespace EindOpdrachtGentseFeesten.Persistence

{
    public class EvenementVerzamelingMapper : EvenementMapper, IEvenementVerzamelingRepository
    {
        private SqlConnection _connection;
        public EvenementVerzamelingMapper()
        {
            _connection = new SqlConnection(DBInfo.ConnectionString);
        }

        public List<EvenementVerzameling> GetAllTopLevelEvenementVerzamelingen()
        {
            try
            {
                _connection.Open();
                string query = $"SELECT TOP 30 * FROM {DBInfo.EvenementGroepTableName} Where ParentId is NULL;";
                List<EvenementVerzameling> topLevelEvenementVerzamelingen = new List<EvenementVerzameling>();
                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = (string)reader["Id"];
                        string title = (string)reader["Title"];
                        string description;
                        if (reader["Description"] != System.DBNull.Value)
                        {
                            description = (string)reader["Description"];
                        }
                        else
                        {
                            description = "Geen beschrijving beschikbaar voor dit evenement";
                        }
                        string childEventIds = (string)reader["ChildId"];
                        topLevelEvenementVerzamelingen.Add(new(id, title, description, parentEvenement: null,
                             childEventIds.Split(',').ToList()));
                    }
                }
                return topLevelEvenementVerzamelingen;
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<Evenement> GetChildren(string parentId)
        {
            try
            {
                _connection.Open();
                string query = $"SELECT * FROM {DBInfo.EvenementGroepTableName} WHERE ParentId = '{parentId}'";
                List<Evenement> childEvents = new List<Evenement>();
                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = (string)reader["Id"];
                        string title = (string)reader["Title"];
                        string description = (string)reader["Description"];
                        string parentEvenementId = (string)reader["parentId"];
                        string childEventIds = (string)reader["ChildId"];
                        EvenementVerzameling evenementverzameling = new(id, title, description, parentEvenementId,
                             childEventIds.Split(',').ToList());
                        childEvents.Add(evenementverzameling);
                    }
                }
                reader.Close();

                string query2 = $"SELECT * FROM {DBInfo.EvenementTableName} WHERE ParentId = '{parentId}'";
                SqlCommand sqlCommand2 = new SqlCommand(query2, _connection);
                reader = sqlCommand2.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string id = (string)reader["Id"];
                        string title = (string)reader["Title"];
                        string description = (string)reader["Description"];
                        string parentEvenementId = (string)reader["parentId"];
                        DateTime start = (DateTime)reader["Start"];
                        DateTime end = (DateTime)reader["End"];
                        int price = reader.GetInt32("Price");

                        EvenementInstantie evenement = new(id, title, description, parentEvenementId, start, end, price);
                        childEvents.Add(evenement);
                    }
                }
                return childEvents;


            }
            finally
            {
                _connection.Close();
            }
        }

        public string GetTableLocationById(string id)
        {
            try
            {
                string query = $"SELECT CASE WHEN EXISTS(" +
                    $" SELECT * FROM {DBInfo.EvenementTableName} WHERE Id = '{id}') then '{DBInfo.EvenementTableName}'" +
                    $"WHEN EXISTS(SELECT * FROM {DBInfo.EvenementGroepTableName} WHERE Id = {id}) then '{DBInfo.EvenementGroepTableName}'" +
                    $"else null END;";
                _connection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, _connection);
                string tableName = (string)sqlCommand.ExecuteScalar();
                if (tableName != null)
                {
                    return tableName;
                }
                else
                {
                    throw new ArgumentException("Id not found in any tables");
                }
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}