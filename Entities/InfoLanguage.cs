namespace Entities
{
    public class InfoLanguage : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
        public string SEO { get; set; }
        public int InfoID { get; set; }
        public virtual Info Info { get; set; }
    }
}
