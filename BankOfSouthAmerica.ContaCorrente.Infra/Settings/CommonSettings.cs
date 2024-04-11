using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSouthAmerica.ContaCorrente.Infra.Settings
{
    public class CommonSettings
    {
        public KafkaSettings KafkaSettings { get; set; }
        public MongoDbSettings MongoDbSettings { get; set; }
    }
}
