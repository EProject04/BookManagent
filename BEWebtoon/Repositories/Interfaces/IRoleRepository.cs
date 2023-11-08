using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.DataTransferObject.UsersDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;

namespace BEWebtoon.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<RoleDto>> GetAll();
        Task<RoleDto> GetById(int id);
        Task CreateRole(CreateRoleDto user);
        Task UpdateRole(UpdateRoleDto user);
        Task DeleteRole(int id);
        Task<PagedResult<RoleDto>> GetRolePagination(SeacrhPagingRequest request);
    }
}
