﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Automatica.Core.Driver
{
    public abstract class DriverNotReadableBase : DriverBase
    {
        protected DriverNotReadableBase(IDriverContext driverContext) : base(driverContext)
        {
        }

        protected override Task Write(object value, IWriteContext writeContext, CancellationToken token = default)
        {
            DriverContext.Logger.LogWarning($"Write not implemented for {FullName}");
            return Task.CompletedTask;
        }
    }
}
