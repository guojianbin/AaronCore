using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aaron.Core
{
    public class SEOEntity<TKey> : BaseEntity<TKey> where TKey : struct
    {
        #region Properties

        /// <summary>
        /// Gets or sets the SEO url name.
        /// </summary>
        /// <value>
        /// The SEO url name.
        /// </value>
        public string SEOUrlName { get; set; }

        /// <summary>
        /// Gets or sets the meta title.
        /// </summary>
        /// <value>
        /// The meta title.
        /// </value>
        public string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords.
        /// </summary>
        /// <value>
        /// The meta keywords.
        /// </value>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description.
        /// </summary>
        /// <value>
        /// The meta description.
        /// </value>
        public string MetaDescription { get; set; }

        #endregion
    }
}
