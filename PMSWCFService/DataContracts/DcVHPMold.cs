﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PMSWCFService.DataContracts
{
    [DataContract]
    public class DcVHPMold
    {
        [DataMember]
        public Guid ID { get; set; }
        [DataMember]
        public string ModelType { get; set; }
        [DataMember]
        public string MoldDetails { get; set; }
        [DataMember]
        public double InnerDiameter { get; set; }
        [DataMember]
        public double ModelHeight { get; set; }
        [DataMember]
        public int State { get; set; }
        [DataMember]
        public string Creator { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }

    }

}
