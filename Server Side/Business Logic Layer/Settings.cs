using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer
{
    public class Settings
    {
        //private readonly Data_Access_Layer.Settings _repository;

        public Settings(string connectionString)
        {
            //_repository = new Data_Access_Layer.Settings(connectionString); // send to DAL
            Data_Access_Layer.Settings.SetConnectionString(connectionString); // send to DAL
        }

    }
}
