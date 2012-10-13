using Aaron.Core.Utility.Setting;
using Aaron.Core.Analysis.Config;

namespace Aaron.Core.Analysis
{
    public class Google : AnalysisFactory
    {
        public Google() : this("GoogleAnalysis") { }

        public Google(string analyticName) : base(new SettingHelper<IAnalysisSetting>(analyticName))
        {
            
        }
    }
}