
namespace Aaron.Core.Utility.Setting
{
    public interface ISettingHelper<T> where T : class
    {
        T Setting();
        void Save();
    }
}
