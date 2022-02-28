using Entities;
using Helper.Methods;
using K205Oleev.Areas.admin.ViewModel;
using K205Oleev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K205Oleev.Areas.admin.Controllers
{
    [Area("admin")]
    public class OurTestimonialController : Controller
    {
        private readonly OleevDbContext _context;
        private IWebHostEnvironment _environment;

        public OurTestimonialController(OleevDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var ourtestimonial = _context.OurTestimonialLanguages.Include(x => x.OurTestimonial).Where(x => x.LangCode == "Az").ToList();
            return View(ourtestimonial);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<string> Title, List<string> Description, List<string> Name, List<string> Profession, List<string> LangCode, List<string> SEO, string PhotoURL)
        {
            OurTestimonial ourTestimonial = new()
            {
                PhotoURL = PhotoURL,
                CreatedDate = DateTime.Now,
            };

            _context.OurTestimonials.Add(ourTestimonial);
            _context.SaveChanges();

            for (int i = 0; i < Title.Count; i++)
            {
                OurTestimonialLanguage OurTestimonialLanguage = new()
                {
                    Title = Title[i],
                    Description = Description[i],
                    Name = Name[i],
                    Profession = Profession[i],
                    LangCode = LangCode[i],
                    SEO = SEO[i],
                    OurTestimonialID = ourTestimonial.ID
                };
                _context.OurTestimonialLanguages.Add(OurTestimonialLanguage);
            }


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            EditVM editVM = new()
            {
                OurTestimonialLanguages = _context.OurTestimonialLanguages.Include(x => x.OurTestimonial).Where(x => x.OurTestimonialID == id).ToList(),
                OurTestimonial = _context.OurTestimonials.FirstOrDefault(x => x.ID == id.Value)
            };

            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int OurTestimonialID, List<int> LangID, List<string> Title, List<string> Description, List<string> Name, List<string> Profession, List<string> LangCode, string PhotoURL)
        {
            for (int i = 0; i < Title.Count; i++)
            {
                SEO seo = new();

                OurTestimonialLanguage ourTestimonialLanguage = new()
                {
                    ID = LangID[i],
                    Title = Title[i],
                    Description = Description[i],
                    SEO = seo.SeoURL(Title[i]),
                    LangCode = LangCode[i],
                    OurTestimonialID = OurTestimonialID
                };
                var updatedEntity = _context.Entry(ourTestimonialLanguage);
                updatedEntity.State = EntityState.Modified;

            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
