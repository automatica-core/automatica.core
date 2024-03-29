﻿using System;
using Automatica.Core.Base.Cache;
using Automatica.Core.EF.Models;
using Automatica.Core.EF.Models.Areas;

namespace Automatica.Core.Internals.Cache.Common
{
    public interface IAreaCache : IStore<AreaInstance>
    {
        bool IsAreaExisting(Guid id);
        public AreaInstance GetSingle(AutomaticaContext context, Guid guid);
    }
}
