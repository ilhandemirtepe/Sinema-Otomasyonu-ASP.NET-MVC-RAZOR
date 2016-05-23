using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSinemaOdev.ViewModel
{
    public class FilmViewModel
    {


        public int FilmIDNumber { get; set; }
        public string FilmAdName { get; set; }
        public bool FilmUcBoyutlumuCheck { get; set; }
        public Nullable<int> FilmTurIDNumber { get; set; }
        public Nullable<int> FilmSureTime { get; set; }
        public bool FilmYerlimiCheck { get; set; }


    }
}