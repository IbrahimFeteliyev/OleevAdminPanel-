using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OurExpertiseLanguage : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string SubTitle { get; set; }
        public string SubDescription { get; set; }
        public string LangCode { get; set; }
        public string SEO { get; set; }
        public int OurExpertiseID { get; set; }
        public virtual OurExpertise OurExpertise { get; set; }
    }
}
