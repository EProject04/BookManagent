using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.Pagination;
using BEWebtoon.Requests;

namespace BEWebtoon.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAll();
        Task<RoleDto> GetById(int id);
        Task CreateRole(CreateRoleDto user);
        Task UpdateRole(UpdateRoleDto user);
        Task DeleteRole(int id);
        Task<PagedResult<RoleDto>> GetRolePagination(SeacrhPagingRequest request);
    }
}
