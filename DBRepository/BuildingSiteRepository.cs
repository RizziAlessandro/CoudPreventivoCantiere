using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using DBRepository.Models;

namespace DBRepository
{
    public class BuildingSiteRepository : IBuildingSiteRepository
    {
        private string _connectionString;

        public BuildingSiteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BuildingSite> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
SELECT [Id]
      ,[CustomerName]
      ,[CutomerEmail]
      ,[BuildingSiteLocation]
      ,[PhotoFolderUri]
  FROM [dbo].[BuildingSite]";

                return connection.Query<BuildingSite>(query).ToList();
            }
        }

        public BuildingSite Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                var query = @"
SELECT [Id]
      ,[CustomerName]
      ,[CutomerEmail]
      ,[BuildingSiteLocation]
      ,[PhotoFolderUri]
  FROM [dbo].[BuildingSite]
  WHERE Id = @Id";

                return connection.QueryFirstOrDefault<BuildingSite>(query, new { Id = id });
            }
        }

        public void Insert(BuildingSite value)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
INSERT INTO [dbo].[BuildingSite]
           ([CustomerName]
           ,[CutomerEmail]
           ,[BuildingSiteLocation]
           ,[PhotoFolderUri])
     VALUES
           (@CustomerName
           ,@CutomerEmail
           ,@BuildingSiteLocation
           ,@PhotoFolderUri)";

                connection.Query(query, value);
            }
        }

        public void Update(BuildingSite value)
        {
            throw new NotImplementedException();
        }
    }
}
