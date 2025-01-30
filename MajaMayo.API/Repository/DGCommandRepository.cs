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
        //public Task<bool> HandleDGRequests(int healthExaminationId)
        //{
        //    //insert into db
        //    //send to DG api
        //    //update db with response msg
        //    throw new NotImplementedException();
        //}

        //public Task<bool> HandleDGExaminationPDF(int healthExaminationId)
        //{
        //    //insert into db
        //    //send to DG api
        //    //update db with response msg
        //    throw new NotImplementedException();
        //}

    }
}
