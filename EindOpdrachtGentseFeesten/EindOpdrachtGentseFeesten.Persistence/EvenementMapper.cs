using Microsoft.Data.SqlClient;
using EindOpdrachtGentseFeesten.Persistence.ExtensionMethods;
using EindOpdrachtGentseFeesten.Domain.Repository;
using EindOpdrachtGentseFeesten.Domain.Model;
using System.Data;

namespace EindOpdrachtGentseFeesten.Persistence

{
    public class EvenementMapper : IEvenementRepository
    {
        private readonly SqlConnection _connection;
        private const string descriptionUnavailableString = "Geen beschrijving beschikbaar voor dit evenement";
        public EvenementMapper()
        {
            _connection = new SqlConnection(DBInfo.ConnectionString);
        }

        public List<HigherLevelEvenement> GetAllTopLevelEvenementen()
        {
            try
            {
                _connection.Open();
                string query = $"SELECT top 30 * FROM {DBInfo.EvenementTableName} Where ParentId is NULL ORDER BY Title;";
                SqlCommand cmd = new SqlCommand(query, _connection);
                List<HigherLevelEvenement> topLevelEvenementen = MapToEvenementen(cmd).Cast<HigherLevelEvenement>().ToList();
                return topLevelEvenementen;
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<Evenement> GetChildEvenementen(string id)
        {
            try
            {
                _connection.Open();
                string childrenQuery = $"SELECT * FROM {DBInfo.EvenementTableName} WHERE ParentId = @Id";
                SqlCommand childrenCommand = new SqlCommand(childrenQuery, _connection);
                childrenCommand.Parameters.Add(new SqlParameter("@Id", id));
                List<Evenement> childEvenementen = MapToEvenementen(childrenCommand);

                return childEvenementen;
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<BaseLevelEvenement> GetAllBaseLevelDescendants(string id)
        {
            try
            {
                string baseLevelDescendantsQuery = $"With descendants as (SELECT id, title, description, parentid, start, [end], price, ChildId FROM {DBInfo.EvenementTableName} WHERE id = @Id" +
                    $" UNION ALL SELECT child.id, child.title, child.description, child.parentid, child.start, child.[end], child.price, child.ChildId FROM {DBInfo.EvenementTableName}" +
                    $" child INNER JOIN descendants d ON d.id = child.ParentId)" +
                    $"SELECT * from descendants WHERE ChildId IS NULL;";
                _connection.Open();
                SqlCommand cmd = new SqlCommand(baseLevelDescendantsQuery, _connection);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                List<BaseLevelEvenement> baseLevelDescendants = MapToEvenementen(cmd).Cast<BaseLevelEvenement>().ToList();
                return baseLevelDescendants;
            }
            finally
            {
                _connection.Close();
            }

        }
        private static List<Evenement> MapToEvenementen(SqlCommand cmd)
        {
            List<Evenement> evenementen = new();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string id = reader.GetValue<string>("Id");
                    string title = (string)reader["Title"];
                    string parentEvenementId = reader.GetValue<string>("ParentId");
                    string description = reader.GetValue<string>("Description", descriptionUnavailableString);
                    List<string> childIds = reader.GetValue<string>("ChildId", "").Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                    DateTime start = reader.GetValue<DateTime>("Start");
                    DateTime end = reader.GetValue<DateTime>("End");
                    int price = reader.GetValue<int>("Price");
                    
                    if (childIds.Any())
                    {
                        HigherLevelEvenement higherLevelEvenement = new(id, title, description, parentEvenementId, childIds, start, end, price);
                        evenementen.Add(higherLevelEvenement);
                    }
                    else
                    {
                        BaseLevelEvenement baseLevelEvenement = new(id, title, description, parentEvenementId, childIds, start, end, price);
                        evenementen.Add(baseLevelEvenement);
                    }
                }
            }
            reader.Close();
            return evenementen;
        }

        public void SaveToPlanner(string id)
        {
            try
            {
                _connection.Open();
                string updateSetPlannerToTrueQuery = $"UPDATE {DBInfo.EvenementTableName} SET OnPlanner = 1 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(updateSetPlannerToTrueQuery, _connection);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.ExecuteNonQuery();
            }
            finally
            {

               _connection.Close();
            }
            
        }
        public void RemoveFromPlanner(string id)
        {
            try
            {
                _connection.Open();
                string updateSetPlannerToFalseQuery = $"UPDATE {DBInfo.EvenementTableName} SET OnPlanner = 0 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(updateSetPlannerToFalseQuery, _connection);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.ExecuteNonQuery();
            }
            finally 
            {

                _connection.Close();
            }
        }

        public List<Evenement> GetEvenementenOnPlanner()
        {
            try
            {
                _connection.Open();
                string eventsOnPlannerQuery = "SELECT * FROM GentseFeestenInt WHERE OnPlanner = 1";
                SqlCommand cmd = new SqlCommand(eventsOnPlannerQuery, _connection);
                return MapToEvenementen(cmd);
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}


