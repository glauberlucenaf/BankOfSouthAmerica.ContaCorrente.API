using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSouthAmerica.ContaCorrente.Infra.Settings
{
    public class MongoDbSettings
    {
        public string Server { get; set; }
        public string DataBase { get; set; }
        public string CollectionContas { get; set; }
    }
}
