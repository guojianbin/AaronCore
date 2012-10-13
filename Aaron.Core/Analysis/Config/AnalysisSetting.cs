using System.Configuration;

namespace Aaron.Core.Analysis.Config
{
    public class AnalysisSetting : ConfigurationSection, IAnalysisSetting
    {
        [ConfigurationProperty("id", IsRequired = true)]
        public string Id
        {
            get { return this["id"].ToString(); }
            set { this["id"] = value; }
        }
    }
}
