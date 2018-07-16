using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DBRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAppElettricisti.Models.InterventionModels;

namespace WebAppElettricisti.Controllers
{
    public class InterventionController : Controller
    {
       // private readonly IInterventionRepository _interventionRepository;
        private readonly IConfiguration _configuration;

        public InterventionController(/*IInterventionRepository interventionRepository,*/ IConfiguration configuration)
        {
            //_interventionRepository = interventionRepository;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var sql = @"SELECT Intervention.Id AS InterventionId, BuildingSite.CustomerName, Intervention.Title, Intervention.Cost 
                        FROM BuildingSite JOIN Intervention 
                        ON BuildingSite.Id = Intervention.BuidingSiteId 
                        WHERE Intervention.Type = 1; ";

            using (var conn = new SqlConnection(_configuration["CloudPreventivoDBConnectionString"]))
            {
                conn.Open();

                var result = conn.Query<InterventionModel>(sql);

                conn.Close();

                return View(result);
            }
        }

        public IActionResult Details(int inetrventionId)
        {
            var sql = @"SELECT BuildingSite.BuildingSiteLocation, BuildingSite.CutomerEmail,
                        FROM BuildingSite JOIN Intervention 
                        ON BuildingSite.Id = Intervention.BuidingSiteId 
                        WHERE Intervention.Type = 1 AND Intervention.Id = @id ";

            using (var conn = new SqlConnection(_configuration["CloudPreventivoDBConnectionString"]))
            {
                conn.Open();

                var result = conn.Query<InterventionModel>(sql, new { id = inetrventionId });

                conn.Close();

                return View(result);
            }
        }
    }
}