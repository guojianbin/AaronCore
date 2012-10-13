using System;

namespace Aaron.Core.Data
{
    public abstract class DataProviderManager
    {
        public DataProviderManager(DataSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            this.Settings = settings;
        }
        protected DataSettings Settings { get; private set; }
        public abstract IDataProvider LoadDataProvider();
    }
}
