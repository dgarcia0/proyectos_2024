using PreguntasYRespuestas.Data.Models;

namespace PreguntasYRespuestas.Data
{
    public interface IDataRepository
    {
        IEnumerable<QuestionGetManyResponse> GetQuestions();
        IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search);
        IEnumerable<QuestionGetManyResponse> GetUnanwseredQuestions();
        QuestionGetSingleResponse GetQuestion(int questionId);
        bool QuestionExists(int questionId);
        AnswerGetResponse GetAnswer(int answerId);
        QuestionGetSingleResponse PostQuestion(QuestionPostFullRequest question);
        void DeleteQuestion(int questionId);
        QuestionGetSingleResponse PutQuestion(int questionId, QuestionPutRequest question);
        AnswerGetResponse PostAnswer(AnswerPostFullRequest answer);
    }
}
