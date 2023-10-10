using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
