﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Model
{
    public class MainPageModel
    {
        public List<ProductionData> ProductionDatas { get; set; }=new List<ProductionData>();
        public List<ProductData> ProductDatas { get; set; } = new List<ProductData>();

    }
}