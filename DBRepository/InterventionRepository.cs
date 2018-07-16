using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using DBRepository.Models;

namespace DBRepository
{
    public class InterventionRepository : IInterventionRepository
    {
        private string _connectionString;

        public InterventionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Intervention> Get()
        {
            throw new NotImplementedException();
        }

        public Intervention Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Intervention> GetElectricalInterventions()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT 
    BuildingSite.CutomerEmail, 
    BuildingSite.CustomerName, 
    BuildingSite.BuildingSiteLocation, 
    Intervention.Title, 
    Intervention.Notes, 
    Intervention.Cost
FROM BuildingSite JOIN Intervention
ON BuildingSite.Id = Intervention.BuidingSiteId
WHERE Intervention.Type = 1;";

                return connection.Query<Intervention>(query).ToList();
            }
        }

        public void Insert(Intervention value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [dbo].[Intervention]
           ([Title]
           ,[Type]
           ,[Notes]
           ,[Cost]
           ,[PhotoFolderUri]
           ,[BuidingSiteId])
     VALUES
           (@Title
           ,@Type
           ,@Notes
           ,@Cost
           ,@PhotoFolderUri
           ,@BuidingSiteId)";

                connection.Query(query, value);
            }
        }

        public void Update(Intervention value)
        {
            throw new NotImplementedException();
        }
    }
}
