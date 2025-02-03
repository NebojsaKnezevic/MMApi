using Dapper;
using MajaMayo.API.DTOs;
using MajaMayo.API.Mapper;
using MajaMayo.API.Models;
using System.Data;

namespace MajaMayo.API.Repository
{
    public class DGCommandRepository : IDGCommandRepository
    {
        private readonly IDbConnection _connection;
        private readonly HttpContext _httpContext;

        public DGCommandRepository(IDbConnection connection, IHttpContextAccessor httpContextAccessor)
        {
            _connection = connection;
            _httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<string> InsertDeltaGeneraliApprovedUsers(ICollection<DGApprovedUserResponse> dGApproveds)
        {
            if (dGApproveds == null || !dGApproveds.Any())
                throw new ArgumentException("The list of approved users cannot be null or empty.");

            var tvp = dGApproveds.ToDTO().ToDataTable();
            var pars = new DynamicParameters();
            pars.Add("@Users", tvp.AsTableValuedParameter("dbo.DGApprovedUserType"));

            var result = await _connection.ExecuteAsync("dbo.spInsertDGApprovedUsers", pars, commandType: CommandType.StoredProcedure);

            if (result > 0)
            {
                return $"{result} users successfully inserted into the database.";
            }
            else
            {
                return "No users were inserted into the database. Please check the input data.";
            }
        }

        public async Task<bool> HandleDGRequests(int healthAssessmentId)
        {
            //insert into db
            var pars = new DynamicParameters();
            pars.Add("@HealthAssessmentId", healthAssessmentId);
            var result = await _connection.ExecuteScalarAsync<int>("dbo.spInsertDGRequest", pars, commandType: CommandType.StoredProcedure);

            if (result != -1)
            {
                //send to dg 
                var statusCode = 200;
                var responseMsg = "To be implemented after we receive API from DG.";

                //update db with response
                var pars2 = new DynamicParameters();
                pars2.Add("@RequestId", result, DbType.Int32);
                pars2.Add("@StatusCode", statusCode);
                pars2.Add("@ResponseMessage", responseMsg);

                var result2 = await _connection.ExecuteScalarAsync<int>("dbo.spUpdateDGRequest", pars2, commandType: CommandType.StoredProcedure);
                if (result2 > 1)
                {
                    throw new Exception("Error, affected more than 1 row in DG_request table, this should not happen!");
                }
            }
            else 
            {
                return false;
            }

            return true;
        }

        public Task<bool> HandleHealthExaminationPDF(int healthAssessmentId)
        {
            throw new NotImplementedException();
        }
    }
}
