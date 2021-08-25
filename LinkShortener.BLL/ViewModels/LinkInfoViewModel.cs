using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.BLL.ViewModels
{
    public class LinkInfoViewModel
    {
        public string OriginalLink { get; set; }
        public string ShortenedLink { get; set; }
        public int NumberOfLinkRequests { get; set; }
    }
}
