﻿using System;
using System.Collections.Generic;
using System.Threading;
using Disqord.Rest.Api;

namespace Disqord.Rest.Default
{
    public class DefaultRestRequestOptions : IRestRequestOptions
    {
        /// <summary>
        ///     Gets or sets the cancellation token for the request.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <inheritdoc/>
        public string Reason
        {
            get
            {
                if (_headers != null && _headers.TryGetValue("X-Audit-Log-Reason", out var value))
                    return value;

                return null;
            }
            set => Headers["X-Audit-Log-Reason"] = value;
        }

        /// <summary>
        ///     Gets or sets the custom headers for the request.
        /// </summary>
        public IDictionary<string, string> Headers
        {
            get => _headers ??= new Dictionary<string, string>();
            set => _headers = value;
        }
        private IDictionary<string, string> _headers;

        public Action<IRestRequest> RequestAction { get; set; }
    }
}