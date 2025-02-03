using Microsoft.AspNetCore.Mvc.Rendering;

namespace muzikaletleristok.Models
{
    public class Cascade
    {
        public IEnumerable<SelectListItem> KategorilerList { get; set; }
        public IEnumerable<SelectListItem> MuzikAletleriList { get; set; }
    }
}
