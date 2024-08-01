using PreguntasYRespuestas.Data.Models;

namespace PreguntasYRespuestas.Data
{
    public interface IQuestionCache
    {
        QuestionGetSingleResponse Get(int questionId);
        void Remove(int questionId);
        void Set(QuestionGetSingleResponse question);
    }
}
