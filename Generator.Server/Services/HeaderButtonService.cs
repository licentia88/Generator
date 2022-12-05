﻿using Generator.Shared.Models;
using Generator.Shared.Services;

namespace Generator.Server.Services;

public class HeaderButtonService : GenericServiceBase<GeneratorContext, HEADER_BUTTON>, IHeaderButtonService
{
    public HeaderButtonService(IServiceProvider provider) : base(provider)
    {
    }
}
