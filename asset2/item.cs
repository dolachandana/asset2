using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asset2
{
    internal class item
    {
        internal int d;
        internal int value;

        public int Id{get;set;}
        public string type{ get; set; }
        public string brand { get; set; }
        public DateTime purchase_date { get; set; }
        public int price { get; set; }
        public string modelname { get; set; }
        public string office { get; set; }
        public string currency { get; set; }
        public double todayprice { get; set; }
    }
}
