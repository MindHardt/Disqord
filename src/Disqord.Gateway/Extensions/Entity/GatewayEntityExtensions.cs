﻿using System.ComponentModel;
using Qommon;

namespace Disqord.Gateway
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static partial class GatewayEntityExtensions
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IGatewayClient GetGatewayClient(this IClientEntity entity)
        {
            Guard.IsNotNull(entity);
            Guard.IsAssignableToType<IGatewayClient>(entity.Client);

            return entity.Client as IGatewayClient;
        }
    }
}
