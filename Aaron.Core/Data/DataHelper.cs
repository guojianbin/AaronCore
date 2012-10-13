using System;
using System.Data.SqlClient;
using Aaron.Core.Infrastructure;
using System.Threading;

namespace Aaron.Core.Data
{
    public partial class DataHelper
    {
        private static bool? _hasSettingsFileOrNotNull;
        private static bool? _databaseIsExisted;
        private static string connectionString;

        public static string ConnectionString { get{return connectionString;} }

        public static bool HasSettingsFileOrNotNull()
        {
            if (!_hasSettingsFileOrNotNull.HasValue)
            {
                var settings = IoC.Resolve<DataSettings>();
                _hasSettingsFileOrNotNull = settings != null && !String.IsNullOrEmpty(settings.DataConnectionString);
            }
            return _hasSettingsFileOrNotNull.Value;
        }

        private static string _createConnectionString(bool trustedConnection, string serverName, string databaseName, string userName, string password, int timeOut = 0)
        {
            var builder = new SqlConnectionStringBuilder();
            builder.IntegratedSecurity = trustedConnection;
            builder.DataSource = serverName;
            builder.InitialCatalog = databaseName;
            if (!trustedConnection)
            {
                builder.UserID = userName;
                builder.Password = password;
            }
            builder.PersistSecurityInfo = false;
            builder.MultipleActiveResultSets = true;
            if (timeOut > 0)
            {
                builder.ConnectTimeout = timeOut;
            }

            connectionString = builder.ConnectionString;

            return connectionString;
        }

        public static void SaveSettings(string provider, bool trustedConnection = true, string serverName = ".\\SQLExpress", string databaseName = "", string userName = "", string password = "", int timeOut = 0)
        {
            var _connectionString = connectionString ?? _createConnectionString(trustedConnection, serverName, databaseName, userName, password, timeOut);

            var settings = new DataSettings() 
            { 
                DataConnectionString = _connectionString,
                DataProvider = provider
            };

            var settingsManager = IoC.Resolve<DataSettingsManager>();
            settingsManager.SaveSettings(settings);
        }

        public static void RemoveSettingsFile()
        {
            IoC.Resolve<DataSettingsManager>().RemoveSettingsFile();
        }

        public static bool CreateDatabase()
        {
            if (DatabaseIsExisted()) return false;

            var settings = IoC.Resolve<DataSettings>();

            var builder = new SqlConnectionStringBuilder(connectionString ?? settings.DataConnectionString);
            var dbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            var masterCatalogConnectionString = builder.ToString();
            string query = string.Format("CREATE DATABASE [{0}] COLLATE SQL_Latin1_General_CP1_CI_AS", dbName);

            using (var conn = new SqlConnection(masterCatalogConnectionString))
            {
                conn.Open();
                using (var sqlCmd = new SqlCommand(query, conn))
                {
                    sqlCmd.ExecuteNonQuery();
                }
            }

            Thread.Sleep(3000);

            return true;
        }

        public static bool DatabaseIsExisted()
        {
            _hasSettingsFileOrNotNull = null;
            if (!HasSettingsFileOrNotNull()) return false;
            if (!_databaseIsExisted.HasValue)
            {
                var settings = IoC.Resolve<DataSettings>();
                using (var conn = new SqlConnection(settings.DataConnectionString))
                {
                    try
                    {
                        conn.Open();
                        _databaseIsExisted = true;
                    }
                    catch
                    {
                        _databaseIsExisted = false;
                    }
                }
            }
            return _databaseIsExisted.Value;
        }

        public static void ResetCache()
        {
            _hasSettingsFileOrNotNull = null;
            _databaseIsExisted = null;
            connectionString = null;
        }
    }
}
