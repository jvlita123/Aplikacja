using Data.Entities;
using Data.Repositories;

namespace Service.Services
{

    public class CyclesService
    {
        private readonly CyclesRepository _cyclesRepository;
        public CyclesService(CyclesRepository cyclesRepository)
        {
            _cyclesRepository = cyclesRepository;
        }

        public List<Cycle> GetAll()
        {
            List<Cycle> cycle = _cyclesRepository.GetAll().ToList();

            return cycle;
        }

        public Cycle GetById(int id)
        {
            Cycle cycle = _cyclesRepository.GetAll().Where(x=>x.Id == id).FirstOrDefault();

            return cycle;
        }

        public Cycle UploadFile(string filePath, int id)
        {
            Cycle cycle = _cyclesRepository.GetAll().Where(x=>x.Id==id).FirstOrDefault() ;
            cycle.SourcePath = filePath;

            _cyclesRepository.Update(cycle);
            _cyclesRepository.SaveChanges();
            return cycle;
        }

        public Cycle NewCycle(Cycle cycle)
        {
            Cycle newCycle = new Cycle();
            newCycle.Id = cycle.Id;
            newCycle.CourseId = cycle.CourseId;
            newCycle.Description = cycle.Description;
            newCycle.StartDate = cycle.StartDate;
            newCycle.EndDate = cycle.EndDate;

            _cyclesRepository.AddAndSaveChanges(newCycle);
            return newCycle;
        }

        public void RemoveCycleById(int id)
        {
            _cyclesRepository.RemoveById(id);
            _cyclesRepository.SaveChanges();
        }
    }
}
