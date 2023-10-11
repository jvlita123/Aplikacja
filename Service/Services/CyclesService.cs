using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
