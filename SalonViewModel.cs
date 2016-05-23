using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSinemaOdev.ViewModel
{
    public class SalonViewModel
    {
        public int SalonIDNumber { get; set; }
        public string SalonAdNumber { get; set; }
        public Nullable<int> SalonKapasiteNumber { get; set; }
        public Nullable<int> SalonSiraSayisiNumber { get; set; }
        public Nullable<int> SeansIDNumber { get; set; }
    }
}