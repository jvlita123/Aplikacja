using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
