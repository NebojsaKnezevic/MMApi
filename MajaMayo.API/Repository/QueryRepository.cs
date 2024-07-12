using Dapper;
using MajaMayo.API.Models;
using System.Data;

namespace MajaMayo.API.Repository
{
    public class QueryRepository : IQueryRepository
    {
        private readonly IDbConnection _context;

        public QueryRepository(IDbConnection context)
        {
            _context = context;
        }

        public async Task<ICollection<QuestionResponse>> GetQuestions()
        {
            var response = await _context.QueryAsync<QuestionResponse>
                ("dbo.spGetQuestions", null, commandType: CommandType.StoredProcedure);
            return response.ToList();
        }

        public async Task<ICollection<QuestionGroupResponse>> GetQuestionsQueryOld(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            string sql = "EXEC dbo.spGetQuestionsQuery @Email";

            var result = await _context.QueryMultipleAsync(
                sql,
                parameters,
                commandType: CommandType.Text
            );

            var questionGroups = await result.ReadAsync<QuestionGroupResponse>();
            var questions = await result.ReadAsync<QuestionResponse>();
            var answers = await result.ReadAsync<AnswerResponse>();

            //var groupedQuestions = questions.GroupBy(q => q.QuestionGroupId);
            foreach (var questionGroup in questionGroups)
            {
                var questionFromSpecificGroup = questions.Where(q => q.QuestionGroupId == questionGroup.Id).ToList();
                foreach (var question in questionFromSpecificGroup)
                {
                    var answersForSpecificQuestion = answers.Where(x => x.QuestionId == question.Id).ToList();
                    question.Answers.AddRange(answersForSpecificQuestion);
                }
                questionGroup.Questions.AddRange(questionFromSpecificGroup);
            }
            return GroupQuestionGroupResponse(questionGroups.ToList());
        }

        public async Task<ICollection<CommonResponseObject>> GetQuestionsQuery(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);

            string sql = "EXEC dbo.spGetQuestionsQuery @Email";

            var result = await _context.QueryMultipleAsync(
                sql,
                parameters,
                commandType: CommandType.Text
            );

            var questionGroups = await result.ReadAsync<QuestionGroupResponse>();
            var questions = await result.ReadAsync<QuestionResponse>();
            var answers = await result.ReadAsync<AnswerResponse>();

            var response = new List<CommonResponseObject>();

            foreach (var group in questionGroups)
            {
                response.Add(new CommonResponseObject 
                { 
                    Id = group.Id,
                    Type = CommonResponseObject.StepType.Group.ToString(),
                    Name = group.Name,
                    Question = "",
                    
                });

                foreach (var question in questions.Where(x => x.QuestionGroupId == group.Id))
                {
                    response.Add(new CommonResponseObject
                    {
                        Id = question.Id,
                        Type = CommonResponseObject.StepType.Question.ToString(),
                        Name = group.Name,
                        Question = question.Text,
                        Answers = answers.Where(x => x.QuestionId == question.Id ).Select(x => new CommonResponseObject.Answer { 
                            Id = x.Id,
                            Val = x.Text
                        }).ToList(),
                        UserAnswered = answers.Where(x => x.QuestionId == question.Id && x.HealthAssesmentId > 0).Select(x => x.Id).ToList()
                    });
                }
            }

            return response;
        }

        private List<QuestionGroupResponse> GroupQuestionGroupResponse(List<QuestionGroupResponse> subgroups) 
        {
            var subgroupDictionary = subgroups.ToDictionary(x => x.Id, x => x);
            var roots = new List<QuestionGroupResponse>();
            foreach (var subgroup in subgroups)
            {
                
                if (subgroupDictionary.ContainsKey(subgroup.ParentId))
                {
                    var parentSubgroup = subgroupDictionary[subgroup.ParentId];
                    parentSubgroup.Subgroups.Add(subgroup);
                }
                else 
                {
                    roots.Add(subgroup);
                }
            }
            return roots;
        }
    }
}
