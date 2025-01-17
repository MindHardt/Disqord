using Disqord.Serialization.Json;
using Qommon;

namespace Disqord.Rest.Api
{
    public class CreateFollowupMessageJsonRestRequestContent : ExecuteWebhookJsonRestRequestContent
    {
        [JsonProperty("flags")]
        public Optional<MessageFlag> Flags;
    }
}
