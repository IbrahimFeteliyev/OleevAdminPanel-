namespace Entities
{
    public class AboutLanguage : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
        public string SEO { get; set; }
        public int AboutID { get; set; }
        public virtual About About { get; set; }
    }
}
