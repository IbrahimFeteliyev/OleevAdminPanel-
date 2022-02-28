using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Methods
{
    public class SEO
    {
        public string SeoURL (string link)
        {
            //Bu gün hər
            //bu-gun-her-shey-yaxsidi
            link = link.ToLower()
                .Replace("ə", "e")
                .Replace(" ", "-")
                .Replace("ğ", "g")
                .Replace("ç", "c")
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u");
            return link;
        }
    }
}
