using System;
using Aaron.Core.Analysis.Config;
using Aaron.Core.Utility.Setting;

namespace Aaron.Core.Analysis
{
    public abstract class AnalysisFactory : IAnalysis
    {
        //private AnalysisSetting _setting;

        private ISettingHelper<IAnalysisSetting> _setting;

        protected string analysisCode;

        public AnalysisFactory(ISettingHelper<IAnalysisSetting> setting)
        {
            _setting = setting;
            SetAnalysisCode();
        }

        public virtual void SetAnalysisCode()
        {
            var code = "<script type='text/javascript'>" +
                              "var _gaq = _gaq || [];" +
                              "_gaq.push(['_setAccount', '{0}']);" +
                              "_gaq.push(['_trackPageview']);" +

                              "(function() {" +
                                "var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;" +
                                "ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';" +
                                "var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);" +
                              "})();" +
                            "</script>";
            SetAnalysisCode(code);
        }

        public void SetAnalysisCode(string code)
        {
            analysisCode = code;
        }
       
        public string AnalysisCode
        {
            get
            {
                return String.Format(analysisCode, AnalysisId);
            }
        }

        public string AnalysisId
        {
            get { return _setting.Setting().Id; }
            set { _setting.Setting().Id = value; }
        }

        public void Save()
        {
            _setting.Save();
        }
    }
}
