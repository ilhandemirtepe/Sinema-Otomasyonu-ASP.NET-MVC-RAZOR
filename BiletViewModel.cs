using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSinemaOdev.ViewModel
{
    public class BiletViewModel
    {

        public int BiletIDNumber { get; set; }
        public int FilmIDNumber { get; set; }
        public Nullable<System.DateTime> BiletTarihNumber { get; set; }
        public string BiletKoltukNumaraNumber { get; set; }
        public Nullable<int> SatisIDNumber { get; set; }
    }
}