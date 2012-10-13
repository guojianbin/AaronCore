using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aaron.Core.SysConfiguration
{
    public interface ISysConfigurationProvider<TSettings> where TSettings : ISettings, new()
    {
        TSettings Settings { get; }
        void SaveSettings(TSettings settings);
    }
}
