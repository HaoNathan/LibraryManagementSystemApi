using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.DTO.AddTypeDto;
using LibraryManagementSystem.DTO.QueryTypeDto;
using LibraryManagementSystem.DTO.UpdateTypeDto;
using LibraryManagementSystem.IBLL;
using LibraryManagementSystem.IDAL;
using LibraryManagementSystem.MODEL;
using LibraryManagementSystemCommon;

namespace LibraryManagementSystem.BLL
{
    /*===============================================
    * CLR 版本  :4.0.30319.42000
    * 命名空间  :LibraryManagementSystem.BLL
    * 文件名称  :AdminManager.cs
    * 机器名称  :ADMINISTRATOR-5
    * ----------------------------------------
    * 创 建 者  :向豪
    * 创建日期  :2021-01-17 17:50:23 
    * 邮    箱  :2224613103@qq.com
    * 功能描述  :
    * 使用说明  :
    * ----------------------------------------
    * 修改者    :
    * 修改日期  :
    * 修改内容  :
    =================================================*/
    public class AdminManager : IAdminManager
    {
        public AdminManager(IAdminService service, IMapper mapping, IPermissionService permission)
        {
            _service = service;

            _mapping = mapping;

            _permissionService = permission;
        }

        private readonly IAdminService _service;

        private readonly IMapper _mapping;

        private readonly IPermissionService _permissionService;

        public async Task<bool> AddAdmin(AddAdminDto model)
        {
            return await _service.Add(_mapping.Map<Admin>(model));
        }

        public async Task<AdminDto> QueryAdmin(Guid id)
        {
            return _mapping.Map<AdminDto>(await _service.Query(id, false));
        }

        public async Task<AdminDto> QueryAdmin(Dictionary<string, string> queryParameters)
        {
            var data = await _service.QueryAll(queryParameters);
            return _mapping.Map<AdminDto>(data.FirstOrDefault());
        }

        public async Task<List<AdminDto>> QueryAllAdmin(bool includeRemove)
        {
            var data = await _service.QueryAll(includeRemove);
            return _mapping.Map<List<AdminDto>>(data);
        }

        public async Task<List<AdminDto>> QueryAllAdminByPara(Dictionary<string, string> dic) => _mapping.Map<List<AdminDto>>(await _service.QueryAll(dic));

        public async Task<bool> IsExist(Dictionary<string, object> dic) => await _service.IsExist(dic) > 0;

        public async Task<bool> RemoveAdmin(Guid id) => await _service.Delete(id);

        public async Task<bool> UpdateAdmin(Guid id, UpdateAdminDto model, string field)
        {
            var admin = await _service.Query(id, false);
            admin.UpdateTime = DateTime.Now;
            return await _service.Update(CommonClass.SetModelValue(model, admin, field));
        }

        public async Task<bool> IsExist(Guid id) => await _service.IsExist(id);

        public async Task<List<AuthorityDto>> QueryAllAuthority(bool includeRemove)
        {
            var data = await _permissionService.QueryAll(includeRemove);
            return _mapping.Map<List<AuthorityDto>>(data);
        }
    }
}
