using Data.Entities;
using Data.Repositories;

namespace Service.Services
{
    public class QuestionService
    {
        private readonly QuestionRepository _questionRepository;
        public QuestionService(QuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public List<Question> GetAll()
        {
            List<Question> questions = _questionRepository.GetAll().ToList();

            return questions;
        }

        public Question Add(Question question)
        {
            Question newQuestion = _questionRepository.AddAndSaveChanges(question);

            return newQuestion;
        }
    }
}
