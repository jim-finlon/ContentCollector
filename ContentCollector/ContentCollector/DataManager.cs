using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentCollector.Model;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace ContentCollector
{
    public class DataManager
    {
        private readonly MobileServiceClient _client;

        private readonly IMobileServiceTable<Location> _locationTable;

        public DataManager()
        {
            try
            {
                _client = new MobileServiceClient(Constants.ApplicationUrl);

                var store = new MobileServiceSQLiteStore("localstore.db");
                store.DefineTable<Location>();
                _client.SyncContext.InitializeAsync(store);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
