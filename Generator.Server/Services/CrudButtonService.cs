using Generator.Server.Enums;
using Generator.Server.Helpers;
using Generator.Shared.Models;
using Generator.Shared.Models.ComponentModels;
using Generator.Shared.Services;
using Humanizer;
using ProtoBuf.Grpc;

namespace Generator.Server.Services;

public class CrudButtonService : GenericServiceBase<GeneratorContext, HEADER_BUTTONS>, IHeaderButtonsService
{
    public CrudButtonService(IServiceProvider provider) : base(provider)
    {
    }

    public new Task<RESPONSE_RESULT<HEADER_BUTTONS>> CreateAsync(RESPONSE_REQUEST<HEADER_BUTTONS> request, CallContext context = default)
    {
        return TaskHandler.ExecuteModelAsync(async () =>
        {
            var button = request.RR_DATA;

            var newPermission = new PERMISSIONS(nameof(HEADER_BUTTONS),button.CB_TITLE.Dehumanize(), button.CB_CODE );

            button.PERMISSIONS.Add(newPermission);

            Db.HEADER_BUTTONS.Add(button);

            await Db.SaveChangesAsync();

            return button;
        });
    }

    
}