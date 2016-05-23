using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSinemaOdev.ViewModel
{
    public class SeansViewModel
    {

        public int SeansIDNumber { get; set; }
        public Nullable<int> SeansSaatNumber { get; set; }
        public Nullable<int> FilmIDNumber { get; set; }
    
    }
}