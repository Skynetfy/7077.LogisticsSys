using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Entities
{
    public class DeliveryMessage
    {
        public string message { get; set; }

        public int ischeck { get; set; }

        public string condition { get; set; }

        public string com { get; set; }

        public string comcontact { get; set; }

        public int status { get; set; }

        public int state { get; set; }

        public string comurl { get; set; }

        public List<DeliveryData> data { get; set; }


    }

    public class DeliveryData
    {
        public DateTime time { get; set; }

        public string context { get; set; }
    }
}
