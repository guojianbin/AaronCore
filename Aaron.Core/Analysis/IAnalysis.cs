using Aaron.Core.Analysis.Config;

namespace Aaron.Core.Analysis
{
    public interface IAnalysis
    {
        string AnalysisCode { get;}
        string AnalysisId { get; set; }
        void SetAnalysisCode();
        void SetAnalysisCode(string code);
        void Save();
    }
}
