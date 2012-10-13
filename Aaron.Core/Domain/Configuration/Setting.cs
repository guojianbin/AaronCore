using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aaron.Core.Utility;

namespace Aaron.Core.Domain.Configuration
{
    public class Setting : BaseEntity<int>
    {
        public Setting() { }

        public Setting(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Returns the setting value as the specified type
        /// </summary>
        public virtual T As<T>()
        {
            return CommonHelper.To<T>(this.Value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
