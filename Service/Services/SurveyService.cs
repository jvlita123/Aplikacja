using Data.Entities;
using Data.Repositories;

namespace Service.Services
{
    public class SurveyService
    {
        private readonly SurveyRepository _surveyRepository;
        public SurveyService(SurveyRepository surveyRepository)
        {
            _surveyRepository = surveyRepository;
        }

        public List<Survey> GetAll()
        {
            List<Survey> surveys = _surveyRepository.GetAll().ToList();

            return surveys;
        }

        public Survey Add(Survey survey)
        {
            Survey newSurvey = _surveyRepository.AddAndSaveChanges(survey);

            return newSurvey;
        }
    }
}
