using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SPA
{
    public class spaResponse
    {
        public spaResponse()
        {
        }

        [DataMember]
        public bool result { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public object data { get; set; }

    }
}
