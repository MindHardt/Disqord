﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disqord.Collections;
using Disqord.Models;
using Qommon.Collections;

namespace Disqord.Rest
{
    public sealed class RestGroupDmChannel : RestPrivateChannel, IGroupDmChannel
    {
        public IReadOnlyDictionary<Snowflake, RestUser> Recipients { get; private set; }

        public string IconHash { get; private set; }

        public Snowflake OwnerId { get; private set; }

        public RestUser Owner => Recipients.TryGetValue(OwnerId, out var owner) ? owner : null;

        IReadOnlyDictionary<Snowflake, IUser> IGroupDmChannel.Recipients => new ReadOnlyUpcastingDictionary<Snowflake, RestUser, IUser>(Recipients);

        IUser IGroupDmChannel.Owner => Owner;

        internal RestGroupDmChannel(RestDiscordClient client, ChannelModel model) : base(client, model)
        {
            Update(model);
        }

        internal override void Update(ChannelModel model)
        {
            Recipients = new ReadOnlyDictionary<Snowflake, RestUser>(model.Recipients.Value.ToDictionary(x => new Snowflake(x.Id), x => new RestUser(Client, x)));
            IconHash = model.Icon.Value;
            OwnerId = model.OwnerId.Value;

            if (!model.Name.HasValue || string.IsNullOrWhiteSpace(model.Name.Value))
                model.Name = string.Join(", ", Recipients.Values);

            base.Update(model);
        }

        public Task LeaveAsync(RestRequestOptions options = null)
            => Client.DeleteOrCloseChannelAsync(Id, options);
    }
}