using Aaron.Core.Analysis;
using Aaron.Core.Utility.Setting;
using Aaron.Core.Analysis.Config;

namespace Aaron.Core.SocialNetwork
{
    public class Facebook : AnalysisFactory
    {
        public Facebook() : this("FacebookAPI") { }

        public Facebook(string analyticName) : base(new SettingHelper<IAnalysisSetting>(analyticName))
        {
            
        }

        public override void SetAnalysisCode()
        {
            var code = "<div id='fb-root'></div>" +
                        "<script>(function(d, s, id) {" +
                        "var js, fjs = d.getElementsByTagName(s)[0];" +
                        "if (d.getElementById(id)) return;" +
                        "js = d.createElement(s); js.id = id;" +
                    "js.src = '//connect.facebook.net/vi_VN/all.js#xfbml=1&appId={0}';" +
                        "fjs.parentNode.insertBefore(js, fjs);" +
                        "}(document, 'script', 'facebook-jssdk'));" +
                        "</script>";
            base.SetAnalysisCode(code);
        }
    }
}
