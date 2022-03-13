using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class VisitSiteViewModel
    {
        public int VisitToday { get; set; }
        public int VisitYesterday { get; set; }
        public int online { get; set; }
        public int VisitSum { get; set; }
    }
}
