using Service.Entities;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlockService
    {
        private readonly BlockRepository _blockRepository;
        public BlockService(BlockRepository blockRepository)
        {
            _blockRepository = blockRepository;
        }

        public List<Block> GetAll()
        {
            List<Block> blocks = _blockRepository.GetAll().ToList();

            return blocks;
        }
    }
}
