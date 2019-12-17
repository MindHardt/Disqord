﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Disqord.Rest.AuditLogs;

namespace Disqord.Rest
{
    internal sealed class RestAuditLogsRequestEnumerator<T> : RestRequestEnumerator<T>
        where T : RestAuditLog
    {
        private readonly Snowflake _guildId;
        private readonly RetrievalDirection _direction;
        private readonly Snowflake? _userId;
        private readonly Snowflake? _startFromId;

        public RestAuditLogsRequestEnumerator(RestDiscordClient client,
            Snowflake guildId, int limit, Snowflake? userId, Snowflake? startFromId) : base(client, 100, limit)
        {
            _guildId = guildId;
            _userId = userId;
            _startFromId = startFromId;
        }

        protected override Task<IReadOnlyList<T>> NextPageAsync(
            IReadOnlyList<T> previous, RestRequestOptions options = null)
        {
            var amount = Remaining > 100
                ? 100
                : Remaining;
            var startFromId = _startFromId;
            if (previous != null && previous.Count > 0)
            {
                startFromId = _direction switch
                {
                    RetrievalDirection.Before => previous[previous.Count - 1].Id,
                    RetrievalDirection.After => previous[0].Id,
                    RetrievalDirection.Around => throw new NotImplementedException(),
                    _ => throw new ArgumentOutOfRangeException("direction"),
                };
            }

            return Client.InternalGetAuditLogsAsync<T>(_guildId, amount, _userId, startFromId, options);
        }
    }
}