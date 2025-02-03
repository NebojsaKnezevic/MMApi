using Dapper;
using MajaMayo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using static MajaMayo.API.Models.CommonResponseObject;

namespace MajaMayo.API.Repository
{
    public class QueryRepository : IQueryRepository
    {
        private readonly IDbConnection _context;
        private readonly IHttpContextAccessor _httpContext;

        private List<CommonResponseObject> _response;
        private IEnumerable<QuestionGroupResponse> _questionGroups;
        private IEnumerable<QuestionResponse> _questions;
        private IEnumerable<AnswerResponse> _answers;

        public QueryRepository(IDbConnection context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<ICollection<QuestionResponse>> GetQuestions()
        {
            var response = await _context.QueryAsync<QuestionResponse>
                ("dbo.spGetQuestions", null, commandType: CommandType.StoredProcedure);
            return response.ToList();
        }

        public async Task<ICollection<QuestionGroupResponse>> GetQuestionGroups()
        {
            var response = await _context.QueryAsync<QuestionGroupResponse>
                ("dbo.spGetQuestionGroups2", null, commandType:CommandType.StoredProcedure);
            return response.ToList();
        }

        public async Task<ICollection<AnswerResponse>> GetAnswers(int userId, int healthAssesmentId)
        {
            var pars = new DynamicParameters();
            pars.Add("@UserId", userId, DbType.Int32);
            pars.Add("@HealthAssessmentId", healthAssesmentId, DbType.Int32);
            var response = await _context.QueryAsync<AnswerResponse>("dbo.spGetAnswers", pars, commandType: CommandType.StoredProcedure);
            var x = response.Where(x => x.Id == 615 || x.Id == 611).ToList();
            return response.ToList();
        }

        public async Task<ICollection<HealthAssesmentResponse>> GetHealthAssesment(int userId, int healthAssesmentId = 0)
        {
            var pars = new DynamicParameters();
            pars.Add("@UserId", userId, DbType.Int32);
            pars.Add("@HealthAssesmentId", healthAssesmentId, DbType.Int32);

            var response = await _context.QueryAsync<HealthAssesmentResponse>("spGetHealthAssessment", pars, commandType: CommandType.StoredProcedure);
            return response.ToList();
        }

        //public async Task<FamilyHistoryModel> GetFamilyHistory(int id)
        //{
        //    var pars = new DynamicParameters();

        //    pars.Add("@Id",id, DbType.Int32);

        //    var response = await _context.QueryFirstAsync<FamilyHistoryModel>("spGetFamilyHistory", pars, commandType: CommandType.StoredProcedure);
        //    return response;
        //}

        public async Task<ICollection<HealthExaminationResponse>> GetHealthExaminations()
        {
            var response = await _context.QueryAsync<HealthExaminationResponse>("spGetHealthExaminations", null,commandType: CommandType.StoredProcedure);
            return response.ToList();
        }

        public async Task<ICollection<HealthAssessmentScoresResponse>> GetHealthAssessmentScores(int healthAssessmentId)
        {
            var pars = new DynamicParameters();

            pars.Add("@HealthAssessmentId", healthAssessmentId, DbType.Int32);

            var response = await _context.QueryAsync<HealthAssessmentScoresResponse>("dbo.spGetHealthAssessmentScores", pars, commandType: CommandType.StoredProcedure);
            return response.ToList();
        }
    }
}
