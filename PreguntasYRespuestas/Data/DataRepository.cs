﻿using Dapper;
using Microsoft.Data.SqlClient;
using PreguntasYRespuestas.Data.Models;
using static Dapper.SqlMapper;

namespace PreguntasYRespuestas.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly string _connectionString;

        public DataRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public AnswerGetResponse GetAnswer(int answerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<AnswerGetResponse>(@"EXEC dbo.Answer_Get_ByAnswerId @AnswerId = @AnswerId", new { AnswerId = answerId });
            }
            throw new NotImplementedException();
        }

        public QuestionGetSingleResponse GetQuestion(int questionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (GridReader results = connection.QueryMultiple(@"EXEC dbo.Question_GetSingle @QuestionId = @QuestionId;
                EXEC dbo.Answer_Get_ByQuestionId @QuestionId = @QuestionId", new { QuestionId = questionId }))
                {
                    var question = results.Read<QuestionGetSingleResponse>().FirstOrDefault();
                    if (question != null)
                    {
                        question.Answers = results.Read<AnswerGetResponse>().ToList();
                    }
                    return question;
                }
                /*var question =  connection.QueryFirstOrDefault<QuestionGetSingleResponse>(@"EXEC dbo.Question_GetSingle @QuestionId = @QuestionId", new { QuestionId = questionId});
                if (question != null) {
                    question.Answers = connection.Query<AnswerGetResponse>(@"EXEC dbo.Answer_Get_ByQuestionId @QuestionId = @QuestionId", new { QuestionId = questionId });
                }
                return question;*/
            }
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionGetManyResponse> GetQuestions()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetMany");
            }
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionGetManyResponse> GetQuestionsWithAnswers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var questionDictionary = new Dictionary<int, QuestionGetManyResponse>();
                return connection.Query<QuestionGetManyResponse, AnswerGetResponse, QuestionGetManyResponse>
                    ("EXEC dbo.Question_GetMany_WithAnswers", map: (q,a) =>
                    {
                        QuestionGetManyResponse question;
                        if(!questionDictionary.TryGetValue(q.QuestionId, out question))
                        {
                            question = q;
                            question.Answers = new List<AnswerGetResponse>();
                            questionDictionary.Add(question.QuestionId, question);
                        }
                        question.Answers.Add(a);
                        return question;
                    }, splitOn: "QuestionId").Distinct().ToList();
                //return connection.Query<QuestionGetManyResponse>("EXEC dbo.Question_GetMany_WithAnswers");
                /*var questions = connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetMany");
                foreach (var item in questions)
                {
                    item.Answers = connection.Query<AnswerGetResponse>(@"EXEC dbo.Answer_Get_ByQuestionId @QuestionId = @QuestionId",
                        new{ QuestionId = item.QuestionId } ).ToList();
                }
                return questions;*/
            }
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetMany_BySearch @Search = @Search", new { Search = search});
            }
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionGetManyResponse> GetQuestionsBySearchWithPaging(string search, int pageNumber, int pageSize)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var parameters = new { Search = search, PageNumber =pageNumber, PageSize = pageSize };
                return connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetMany_BySearch_WithPaging @Search = @Search
                @PageNumber = @PageNumber, PageSize = @PageSize", parameters);
            }
        }

        public IEnumerable<QuestionGetManyResponse> GetUnanwseredQuestions()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<QuestionGetManyResponse>(@"EXEC dbo.Question_GetUnanswered");
            }
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<QuestionGetManyResponse>> GetUnanwseredQuestionsAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<QuestionGetManyResponse>("EXEC dbo.Question_GetUnanswered");
            }
        }

        public bool QuestionExists(int questionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirst<bool>(@"EXEC dbo.Question_Exists @QuestionId = @QuestionId", new { QuestionId = questionId });
            }
            throw new NotImplementedException();
        }

        public void DeleteQuestion(int questionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"EXEC dbo.Question_Delete @QuestionId = @QuestionId", new { QuestionId = questionId });
            }
            throw new NotImplementedException();
        }

        public AnswerGetResponse PostAnswer(AnswerPostFullRequest answer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirst<AnswerGetResponse>(@"EXEC dbo.Answer_Post @QuestionId = @QuestionId
                @Content = @Content, @UserId = @UserId, UserName = @UserName, @Created = @Created", answer);
            }
            throw new NotImplementedException();
        }

        public QuestionGetSingleResponse PostQuestion(QuestionPostFullRequest question)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var questionId = connection.QueryFirst<int>(@"EXEC dbo.Question_Post @Title = @Title
                @Content = @Content, @UserId = @UserId, UserName = @UserName, @Created = @Created", question);
                return GetQuestion(questionId);
            }
            throw new NotImplementedException();
        }

        public QuestionGetSingleResponse PutQuestion(int questionId, QuestionPutRequest question)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"EXEC dbo.Question_Put @QuestionId = @QuestionId, @Title = @Title, @Content = @Content",
                new { QuestionId = questionId, question.Title, question.Content });
                return GetQuestion(questionId);
            }
            throw new NotImplementedException();
        }
    }
}
