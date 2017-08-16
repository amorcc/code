﻿using cc.basemodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cc.model
{
    /// <summary>
    /// 买家我的供货商
    /// </summary>
    public class VMyRetailerInfo : BaseModel
    {
        public string UserSN_R { get; set; }
        public string CompanyName { get; set; }
        public string BusinessScope { get; set; }
    }
}