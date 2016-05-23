using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSinemaOdev.ViewModel
{
    public class SatisViewModel
    {
        public int SatisIDNumber { get; set; }
        public Nullable<int> CalisanIDNumber { get; set; }
        public Nullable<System.DateTime> SatisTarihNumber { get; set; }
        public Nullable<int> MusteriIDNumber { get; set; }
        public Nullable<decimal> SatisFiyatNumber { get; set; }
    
    }
}