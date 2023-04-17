using Generator.Server.Enums;
using Generator.Server.Helpers;
using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using ProtoBuf.Grpc;

namespace Generator.Server.Services;

//public class PagesDService : GenericServiceBase<GeneratorContext, PAGES_D>, IPagesDService
//{
//    public PagesDService(IServiceProvider provider) : base(provider)
//    {
//    }

//    public new Task<RESPONSE_RESULT<PAGES_D>> CreateAsync(RESPONSE_REQUEST<PAGES_D> request, CallContext context = default)
//    {
//        return TaskHandler.ExecuteModelAsync(async () =>
//        {
//            var page = request.RR_DATA;

//            //Note:CRUD icin button eklemeye gerek yok.
//            if (page.PM_CREATE)
//            {
//                var newPermission = new PERMISSIONS(nameof(CRUD), CRUD.Create.ToString(), page.CB_CODE);
//                page.PERMISSIONS.Add(newPermission);
//            };

//            if (page.PM_READ)
//            {
//                var newPermission = new PERMISSIONS(nameof(CRUD), CRUD.Read.ToString(), page.CB_CODE);
//                page.PERMISSIONS.Add(newPermission);
//            };

//            if (page.PM_UPDATE)
//            {
//                var newPermission = new PERMISSIONS(nameof(CRUD), CRUD.Update.ToString(), page.CB_CODE);
//                page.PERMISSIONS.Add(newPermission);
//            };

//            if (page.PM_DELETE)
//            {
//                var newPermission = new PERMISSIONS(nameof(CRUD), CRUD.Delete.ToString(), page.CB_CODE);
//                page.PERMISSIONS.Add(newPermission);
//            };


//            Db.PAGES_D.Add(page);

//            await Db.SaveChangesAsync();

//            return page;
//        });
//    }

//    public new Task<RESPONSE_RESULT<PAGES_D>> UpdateAsync(RESPONSE_REQUEST<PAGES_D> request, CallContext context = default)
//    {
//        return TaskHandler.ExecuteModelAsync(async () =>
//        {
//            var page = request.RR_DATA;

//            var existingPermissions = page.PERMISSIONS;

//            PERMISSIONS existingCreatePermission = existingPermissions.FirstOrDefault(x => x.PER_DESCRIPTION.Equals(CRUD.Create.ToString()));
//            PERMISSIONS existingUpdatePermission = existingPermissions.FirstOrDefault(x => x.PER_DESCRIPTION.Equals(CRUD.Update.ToString()));
//            PERMISSIONS existingDeletePermission = existingPermissions.FirstOrDefault(x => x.PER_DESCRIPTION.Equals(CRUD.Delete.ToString()));

//            if (!page.PM_CREATE)
//            {
//                page.PERMISSIONS.Remove(existingCreatePermission);
//            }
             
//            if (!page.PM_UPDATE)
//            {
//                page.PERMISSIONS.Remove(existingUpdatePermission);
//            }
            
//            if (!page.PM_DELETE)
//            {
//                page.PERMISSIONS.Remove(existingDeletePermission);
//            }

//            foreach (var existingPermission in existingPermissions)
//            {
//                if (existingPermission.PER_AUTH_CODE != page.CB_CODE)
//                    existingPermission.ChangeAuthCode(page.CB_CODE);
//            }

//            await Db.SaveChangesAsync();

//            return page;
//        });
//    }

//}