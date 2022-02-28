using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CaseStudyLanguage : Base
    {
        public string Title { get; set; }
        public string PhotoTitle { get; set; }
        public string LangCode { get; set; }
        public string SEO { get; set; }
        public int CaseStudyID { get; set; }
        public virtual CaseStudy CaseStudy { get; set; }

    }
}
