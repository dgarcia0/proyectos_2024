using PreguntasYRespuestas.Data.Models;

namespace PreguntasYRespuestas.Data
{
    public interface IDataRepository
    {
        IEnumerable<QuestionGetManyResponse> GetQuestions();
        IEnumerable<QuestionGetManyResponse> GetQuestionsWithAnswers();
        IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search);
        IEnumerable<QuestionGetManyResponse> GetQuestionsBySearchWithPaging(string search, int pageNumber, int pageSize);
        IEnumerable<QuestionGetManyResponse> GetUnanwseredQuestions();
        Task<IEnumerable<QuestionGetManyResponse>> GetUnanwseredQuestionsAsync();
        QuestionGetSingleResponse GetQuestion(int questionId);
        bool QuestionExists(int questionId);
        AnswerGetResponse GetAnswer(int answerId);
        QuestionGetSingleResponse PostQuestion(QuestionPostFullRequest question);
        void DeleteQuestion(int questionId);
        QuestionGetSingleResponse PutQuestion(int questionId, QuestionPutRequest question);
        AnswerGetResponse PostAnswer(AnswerPostFullRequest answer);
    }
}
