using Entities;
using Helper.Methods;
using K205Oleev.Areas.admin.ViewModel;
using K205Oleev.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K205Oleev.Areas.admin.Controllers
{
    [Area("admin")]
    public class AboutController : Controller
    {
        private readonly OleevDbContext _context;
        private IWebHostEnvironment _environment;

        public AboutController(OleevDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var about = _context.AboutLanguages.Include(x=>x.About).Where(x=>x.LangCode == "Az").ToList();
            return View(about);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<string>Title, List<string>Description, List<string> LangCode, List<string> SEO ,string PhotoURL)
        {
            About about = new()
            {
                PhotoURL = PhotoURL,
                CreatedDate = DateTime.Now
            };

            _context.Abouts.Add(about);
            _context.SaveChanges();

            for (int i = 0; i < Title.Count; i++)
            {
                AboutLanguage aboutLanguage = new()
                {
                    Title = Title[i],
                    Description = Description[i],
                    LangCode = LangCode[i],
                    SEO = SEO[i],
                    AboutID = about.ID
                };
                _context.AboutLanguages.Add(aboutLanguage);
            }


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            EditVM editVM = new()
            {
                AboutLanguages = _context.AboutLanguages.Include(x => x.About).Where(x => x.AboutID == id).ToList(),
                About = _context.Abouts.FirstOrDefault(x => x.ID == id.Value)
            };
            
            return View(editVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int AboutID,List<int> LangID,List<string> Title,List<string> Description,List<string> LangCode,string PhotoURL)
        {
            for (int i = 0; i < Title.Count; i++)
            {
                SEO seo = new();

                AboutLanguage aboutLanguage = new()
                {
                    ID = LangID[i],
                    Title = Title[i],
                    Description = Description[i],
                    SEO = seo.SeoURL(Title[i]),
                    LangCode = LangCode[i],
                    AboutID = AboutID
                };
                var updatedEntity = _context.Entry(aboutLanguage);
                updatedEntity.State = EntityState.Modified;

            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
