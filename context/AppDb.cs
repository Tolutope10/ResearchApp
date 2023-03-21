using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;

namespace SchoolProjectAPI.context
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection { get; }
        private IConfiguration _config;
        public AppDb(IConfiguration config)
        {
            _config = config;
            var connString = _config.GetSection("ConnectionStrings").GetSection("Default").Value;
            Connection = new MySqlConnection(connString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
