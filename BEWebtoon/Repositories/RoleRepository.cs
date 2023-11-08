using AutoMapper;
using BEWebtoon.DataTransferObject.RolesDto;
using BEWebtoon.Helpers;
using BEWebtoon.Models;
using BEWebtoon.Pagination;
using BEWebtoon.Repositories.Interfaces;
using BEWebtoon.Requests;
using BEWebtoon.WebtoonDBContext;
using Microsoft.EntityFrameworkCore;
using IOCBEWebtoon.Utilities;

namespace BEWebtoon.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly WebtoonDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly SessionManager _sessionManager;
        public RoleRepository(WebtoonDbContext dbContext, IMapper mapper, SessionManager sessionManager)
        {
            _dBContext = dbContext;
            _mapper = mapper;
            _sessionManager = sessionManager;
        }

        public async Task CreateRole(CreateRoleDto roleDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var data = _mapper.Map<Role>(roleDto);
                try
                {
                    await _dBContext.Roles.AddAsync(data);
                    await _dBContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new CustomException($"Danh muc da ton tai" + ex);
                }
            }
            
        }

        public async Task DeleteRole(int id)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var role = await _dBContext.Roles.FindAsync(id);
                if (role != null)
                {
                    _dBContext.Roles.Remove(role);
                    await _dBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
        }

        public async Task<List<RoleDto>> GetAll()
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                List<RoleDto> rolesDto = new List<RoleDto>();
                var roles = await _dBContext.Roles.ToListAsync();
                if (roles != null)
                {
                    rolesDto = _mapper.Map<List<Role>, List<RoleDto>>(roles);
                }
                return rolesDto;
            }
            return null;
         
        }

        public async Task<RoleDto> GetById(int id)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var role = await _dBContext.Roles.FindAsync(id);
                if (role != null)
                {

                    RoleDto roleDto = _mapper.Map<Role, RoleDto>(role);
                    return roleDto;

                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
            return null;
            
        }

        public async Task<PagedResult<RoleDto>> GetRolePagination(SeacrhPagingRequest request)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var query = await _dBContext.Roles.ToListAsync();
                if (!string.IsNullOrEmpty(request.keyword))
                    query = query.Where(x => x.RoleName.ToLower().Contains(request.keyword.ToLower())
                                            || SearchHelper.ConvertToUnSign(x.RoleName).ToLower().Contains(request.keyword.ToLower())).ToList();
                var items = _mapper.Map<IEnumerable<RoleDto>>(query);
                return PagedResult<RoleDto>.ToPagedList(items, request.PageIndex, request.PageSize);
            }
            return null;
        }

        public async Task UpdateRole(UpdateRoleDto roleDto)
        {
            if (_sessionManager.CheckRole(ROLE_CONSTANTS.Admin))
            {
                var role = await _dBContext.Roles.Where(x => x.Id == roleDto.Id).FirstOrDefaultAsync();
                if (role != null)
                {
                    role.RoleName = roleDto.RoleName;
                    role.Description = roleDto.Description;
                    await _dBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Khong tim thay nguoi dung");
                }
            }
        }
    }
}
