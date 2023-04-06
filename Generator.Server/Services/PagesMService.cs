using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Common;
using Generator.Server.Enums;
using Generator.Server.Helpers;
using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProtoBuf.Grpc;

namespace Generator.Server.Services;
public class PagesMService : GenericServiceBase<GeneratorContext,PAGES_M>, IPagesMService
{
    public PagesMService(IServiceProvider provider) : base(provider)
    {
    }

    public new Task<RESPONSE_RESULT<PAGES_M>> CreateAsync(RESPONSE_REQUEST<PAGES_M> request, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            var page = request.RR_DATA;

            //Note:CRUD icin button eklemeye gerek yok.
            if (page.PM_CREATE)
            {
                var newPermission = new PERMISSIONS(nameof(CRUD), CRUD.Create.ToString(), page.CB_CODE);
                page.PERMISSIONS.Add(newPermission);
            };

            if (page.PM_READ)
            {
                var newPermission = new PERMISSIONS(nameof(CRUD), CRUD.Read.ToString(), page.CB_CODE);
                page.PERMISSIONS.Add(newPermission);
            };

            if (page.PM_UPDATE)
            {
                var newPermission = new PERMISSIONS(nameof(CRUD), CRUD.Update.ToString(), page.CB_CODE);
                page.PERMISSIONS.Add(newPermission);
            };

            if (page.PM_DELETE)
            {
                var newPermission = new PERMISSIONS(nameof(CRUD), CRUD.Delete.ToString(), page.CB_CODE);
                page.PERMISSIONS.Add(newPermission);
            };


            Db.PAGES_M.Add(page);

            await Db.SaveChangesAsync();

            return page;
        });
    }

   
 

    public new Task<RESPONSE_RESULT<PAGES_M>> UpdateAsync(RESPONSE_REQUEST<PAGES_M> request, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            var page = request.RR_DATA;

            var existingPermissions = page.PERMISSIONS;

            PERMISSIONS existingCreatePermission = existingPermissions.FirstOrDefault(x => x.PER_DESCRIPTION.Equals(CRUD.Create.ToString()));
            PERMISSIONS existingUpdatePermission = existingPermissions.FirstOrDefault(x => x.PER_DESCRIPTION.Equals(CRUD.Update.ToString()));
            PERMISSIONS existingDeletePermission = existingPermissions.FirstOrDefault(x => x.PER_DESCRIPTION.Equals(CRUD.Delete.ToString()));

            if (!page.PM_CREATE)
            {
                page.PERMISSIONS.Remove(existingCreatePermission);
            }

            if (!page.PM_UPDATE)
            {
                page.PERMISSIONS.Remove(existingUpdatePermission);
            }
           
            if (!page.PM_DELETE)
            {
                page.PERMISSIONS.Remove(existingDeletePermission);
            }

            foreach (var existingPermission in existingPermissions)
            {
                existingPermission.ChangeAuthCode(page.CB_CODE);
            }

            await Db.SaveChangesAsync();

            return page;
        });
    }

}
