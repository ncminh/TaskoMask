﻿using System.Threading.Tasks;
using TaskoMask.Domain.Core.Data;
using TaskoMask.Domain.Membership.Entities;
using System.Collections.Generic;

namespace TaskoMask.Domain.Membership.Data
{
    public interface IRoleRepository : IBaseAggregateRepository<Role>
    {
        Task<bool> ExistByNameAsync(string id, string name);
        Task<IEnumerable<Role>> GetListByIdsAsync( string[] selectedRolesId);
        Task<long> CountByPermissionIdAsync(string permissionId);
        Task<IEnumerable<Role>> GetListByPermissionIdAsync(string permissionId);

    }
}