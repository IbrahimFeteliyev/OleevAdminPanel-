namespace Entities
{
    public class OurServiceLanguage : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
        public string SEO { get; set; }
        public int OurServiceID { get; set; }
        public virtual OurService OurService { get; set; }
    }
}
