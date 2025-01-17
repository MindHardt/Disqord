using System.Collections.Generic;
using Qommon;

namespace Disqord
{
    public static class LocalSlashCommandExtensions
    {
        public static TSlashCommand WithDescription<TSlashCommand>(this TSlashCommand @this, string description)
            where TSlashCommand : LocalSlashCommand
        {
            @this.Description = description;
            return @this;
        }

        public static TSlashCommand AddOption<TSlashCommand>(this TSlashCommand @this, LocalSlashCommandOption option)
            where TSlashCommand : LocalSlashCommand
        {
            Guard.IsNotNull(option);

            if (!@this.Options.Add(option, out var list))
                @this.Options = new(list);

            return @this;
        }

        public static TSlashCommand WithOptions<TSlashCommand>(this TSlashCommand @this, IEnumerable<LocalSlashCommandOption> options)
            where TSlashCommand : LocalSlashCommand
        {
            Guard.IsNotNull(options);

            if (!@this.Options.With(options, out var list))
                @this.Options = new(list);

            return @this;
        }

        public static TSlashCommand WithOptions<TSlashCommand>(this TSlashCommand @this, params LocalSlashCommandOption[] options)
            where TSlashCommand : LocalSlashCommand
            => @this.WithOptions(options as IEnumerable<LocalSlashCommandOption>);
    }
}
