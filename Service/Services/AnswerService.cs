using Data.Entities;
using Data.Repositories;

namespace Service.Services
{
    public class AnswerService
    {
        private readonly AnswerRepository _answerRepository;
        public AnswerService(AnswerRepository answerRepository)
        {
            _answerRepository = answerRepository;
        }

        public List<Answer> GetAll()
        {
            List<Answer> answers = _answerRepository.GetAll().ToList();

            return answers;
        }

        public Answer Add(Answer answer)
        {
            Answer newAnswer = _answerRepository.AddAndSaveChanges(answer);

            return newAnswer;
        }
    }
}
