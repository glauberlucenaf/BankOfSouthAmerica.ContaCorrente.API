using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOfSouthAmerica.ContaCorrente.Infra.Settings
{
    public class KafkaSettings
    {
        public string TopicName { get; set; }
        public string BootstrapServer { get; set; }
    }
}
