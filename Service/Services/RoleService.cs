using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;
        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public List<Role> GetAll()
        {
            List<Role> roles = _roleRepository.GetAll().ToList();

            return roles;
        }

        public Role Add(Role role)
        {
            Role newRole = _roleRepository.AddAndSaveChanges(role);

            return newRole;
        }
    }
}
