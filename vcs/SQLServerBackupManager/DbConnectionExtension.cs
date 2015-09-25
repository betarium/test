using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Configuration;

namespace SQLServerBackupManager
{
    class DbConnectionExtension : DbConnection
    {
        public DbConnection InnerConnection { get; set; }

        public DbConnectionExtension()
        {
            DbProviderFactory dataFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["Default"].ProviderName);
            InnerConnection = dataFactory.CreateConnection();
            InnerConnection.ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            InnerConnection.Open();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override string ConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        protected override DbCommand CreateDbCommand()
        {
            return InnerConnection.CreateCommand();
        }

        public override string DataSource
        {
            get { throw new NotImplementedException(); }
        }

        public override string Database
        {
            get { return InnerConnection.Database; }
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        public override string ServerVersion
        {
            get { throw new NotImplementedException(); }
        }

        public override ConnectionState State
        {
            get { throw new NotImplementedException(); }
        }

        public new void Dispose()
        {
            if (InnerConnection != null)
            {
                try
                {
                    InnerConnection.Close();
                    InnerConnection = null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        public int ExecuteNonQuery(string sql)
        {
            using (var command = CreateDbCommand())
            {
                command.CommandText = sql;
                return command.ExecuteNonQuery();
            }
        }

    }
}
