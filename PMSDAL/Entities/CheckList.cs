﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    /// <summary>
    /// 检查单
    /// </summary>
    public class CheckList
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string State { get; set; }

        public DateTime UpdateTime { get; set; }
        public string Updator { get; set; }

        public string Title { get; set; }
        public string Introduction { get; set; }
        public string Content { get; set; }
    }
}
