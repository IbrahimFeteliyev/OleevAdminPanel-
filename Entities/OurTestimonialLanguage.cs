using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OurTestimonialLanguage : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string LangCode { get; set; }
        public string SEO { get; set; }
        public int OurTestimonialID { get; set; }
        public virtual OurTestimonial OurTestimonial { get; set; }
    }
}
