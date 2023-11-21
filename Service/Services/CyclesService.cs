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

        public Cycle Add(Cycle cycle)
        {
            Cycle newCycle = _cyclesRepository.AddAndSaveChanges(cycle);

            return newCycle;
        }
    }
}
