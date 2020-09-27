using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace YourSoul4Christ.App.API.Helpers
{
    /// <summary>
    /// Swagger Options
    /// </summary>
    public class SwaggerOptions
    {
        /// <summary>
        /// Gets or sets the json route.
        /// </summary>
        /// <value>
        /// The json route.
        /// </value>
        public string JsonRoute { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the UI endpoint.
        /// </summary>
        /// <value>
        /// The UI endpoint.
        /// </value>
        public string UiEndpoint { get; set; }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        /// <value>
        /// The document path.
        /// </value>
        public string DocPath { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the name of the document.
        /// </summary>
        /// <value>
        /// The name of the document.
        /// </value>
        public string DocName { get; set; }

        public SwaggerOptions(IConfiguration configuration)
        {
            configuration.GetSection(nameof(SwaggerOptions)).Bind(this);
        }
    }
}
