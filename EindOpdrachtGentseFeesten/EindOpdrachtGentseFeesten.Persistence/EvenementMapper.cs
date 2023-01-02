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
                string query = $"SELECT TOP 30 * FROM {DBInfo.EvenementTableName} Where ParentId is NULL ORDER BY Title;";
                SqlCommand cmd = new SqlCommand(query, _connection);
                return MapToHigherLevelEvents(cmd);
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<BaseLevelEvenement> GetBaseLevelChildEvenementen(string id)
        {
            try
            {
                _connection.Open();
                string baseLevelChildrenQuery = $"SELECT * FROM {DBInfo.EvenementTableName} WHERE ParentId = '{id}' and ChildId IS NULL";
                SqlCommand baseLevelChildrenCmd = new SqlCommand(baseLevelChildrenQuery, _connection);
                List<BaseLevelEvenement> baseLevelChildEvenementen = MapToBaseLevelEvents(baseLevelChildrenCmd);

                return baseLevelChildEvenementen;
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<HigherLevelEvenement> GetHigherLevelChildEvenementen(string id)
        {
            try
            {
                _connection.Open();
                string higherLevelChildrenQuery = $"SELECT * FROM {DBInfo.EvenementTableName} WHERE ParentId = '{id}' and ChildId IS NOT NULL";
                SqlCommand higherLevelChildrenCmd = new SqlCommand(higherLevelChildrenQuery, _connection);
                List<HigherLevelEvenement> higherLevelChildEvenementen = MapToHigherLevelEvents(higherLevelChildrenCmd);
                return higherLevelChildEvenementen;
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
                string baseLevelDescendantsQuery = $"With descendants as (SELECT id, title, description, parentid, start, [end], price, ChildId FROM {DBInfo.EvenementTableName} WHERE id = '{id}'" +
                    $" UNION ALL SELECT child.id, child.title, child.description, child.parentid, child.start, child.[end], child.price, child.ChildId FROM {DBInfo.EvenementTableName}" +
                    $" child INNER JOIN descendants d ON d.id = child.ParentId)" +
                    $"SELECT * from descendants WHERE ChildId IS NULL;";
                _connection.Open();
                SqlCommand cmd = new SqlCommand(baseLevelDescendantsQuery, _connection);
                return MapToBaseLevelEvents(cmd);
            }
            finally
            {
                _connection.Close();
            }

        }
        private List<BaseLevelEvenement> MapToBaseLevelEvents(SqlCommand cmd)
        {
            List<BaseLevelEvenement> baseLevelEvents = new();
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

                    BaseLevelEvenement evenement = new(id, title, description, parentEvenementId, childIds, start, end, price);
                    baseLevelEvents.Add(evenement);
                }
            }
            reader.Close();
            return baseLevelEvents;
        }
        private List<HigherLevelEvenement> MapToHigherLevelEvents(SqlCommand cmd)
        {
            List<HigherLevelEvenement> higherLevelEvents = new();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    string id = reader.GetValue<string>("Id");
                    string title = reader.GetValue<string>("Title");
                    string description = reader.GetValue<string>("Description", descriptionUnavailableString);
                    List<string> childIds = reader.GetValue<string>("ChildId", "").Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                    string parentEvenementId = reader.GetValue<string>("ParentId");
                    higherLevelEvents.Add(new(id, title, description, parentEvenementId,
                         childIds));
                }
            }
            reader.Close();
            return higherLevelEvents;
        }
    }
}


