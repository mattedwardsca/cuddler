﻿using CuddlerDev.Modules;

namespace CuddlerDev.Configuration.Internal;

internal class AppService : IAppService
{
    private readonly ICuddlerModule _module;

    public AppService(ICuddlerModule module)
    {
        _module = module;
    }

    public List<IApp> ListShowInBottom()
    {
        return _module.BottomApps.ToList();
    }

    public List<IApp> ListShowInMiddle()
    {
        return _module.MiddleApps.Where(w => !w.Hidden)
                      .ToList();
    }

    public List<IApp> ListShowInTop()
    {
        return _module.TopApps.ToList();
    }
}