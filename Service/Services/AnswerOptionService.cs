using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AnswerOptionService
    {
        private readonly AnswerOptionRepository _answerOptionRepository;
        public AnswerOptionService(AnswerOptionRepository answerOptionRepository)
        {
            _answerOptionRepository = answerOptionRepository;
        }

        public List<AnswerOption> GetAll()
        {
            List<AnswerOption> answerOptions = _answerOptionRepository.GetAll().ToList();

            return answerOptions;
        }

        public AnswerOption Add(AnswerOption answerOption)
        {
            AnswerOption newAnswerOption = _answerOptionRepository.AddAndSaveChanges(answerOption);

            return newAnswerOption;
        }
    }
}
