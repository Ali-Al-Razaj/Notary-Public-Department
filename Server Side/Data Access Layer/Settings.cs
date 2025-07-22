using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public static class Settings
    {
        private static string _connectionString = "";

        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }


        public static string GetConnectionString()
        {
            //var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            //var connectionString = config.GetSection("constr").Value;
            //return connectionString!;

            return _connectionString;
        }
    }
}
