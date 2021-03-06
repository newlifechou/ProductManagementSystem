﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 热压过程当中的记录
    /// </summary>
    public class RecordVHP
    {
        public Guid ID { get; set; }
        public string CreateTime { get; set; }
        public string Creator { get; set; }
        public string VHPID { get; set; }//161210-M
        public string DeivceCode { get; set; }
        //预压力和预压温度
        public double PreTemperature { get; set; }
        public double PrePressure { get; set; }

        //实际压力，温度，真空度，保温时间
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Vaccum { get; set; }
        public double KeepTempTime { get; set; }

        public string Remark { get; set; }
    }
}
