using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.Services.Interfaces;

namespace BEWebtoon.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task CreateRole(CreateRoleDto user)
        {
           await _roleRepository.CreateRole(user);
        }

        public async Task DeleteRole(int id)
        {
           await _roleRepository.DeleteRole(id);
        }

        public async Task<List<RoleDto>> GetAll()
        {
           return await _roleRepository.GetAll();
        }

        public async Task<RoleDto> GetById(int id)
        {
           return await _roleRepository.GetById(id);
        }

        public async Task<PagedResult<RoleDto>> GetRolePagination(SeacrhPagingRequest request)
        {
            return await _roleRepository.GetRolePagination(request);
        }

        public async Task UpdateRole(UpdateRoleDto user)
        {
            await _roleRepository.UpdateRole(user);
        }
    }
}
