﻿using System;
using Qommon;

namespace Disqord
{
    public sealed class CreateTextChannelActionProperties : CreateNestedChannelActionProperties
    {
        public Optional<string> Topic { internal get; set; }

        public Optional<TimeSpan> Slowmode { internal get; set; }

        public Optional<bool> IsNsfw { internal get; set; }

        public Optional<TimeSpan> DefaultAutomaticArchiveDuration { internal get; set; }

        internal CreateTextChannelActionProperties()
        { }
    }
}
