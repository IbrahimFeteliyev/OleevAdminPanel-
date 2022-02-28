using Entities;
using K205Oleev.Areas.admin.ViewModel;
using K205Oleev.Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Helper.Methods;

namespace K205Oleev.Areas.admin.Controllers
{
    [Area("admin")]
    public class OurServiceController : Controller
    {
        private readonly OleevDbContext _context;
        private IWebHostEnvironment _environment;

        public OurServiceController(OleevDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var ourservice = _context.OurServiceLanguages.Include(x => x.OurService).Where(x => x.LangCode == "Az").ToList();
            return View(ourservice);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<string> Title, List<string> Description, List<string> LangCode, List<string> SEO, string PhotoURL, string IconURL)
        {
            OurService ourService = new()
            {
                PhotoURL = PhotoURL,
                CreatedDate = DateTime.Now,
                IconURL = IconURL,
            };

            _context.OurServices.Add(ourService);
            _context.SaveChanges();

            for (int i = 0; i < Title.Count; i++)
            {
                OurServiceLanguage OurServiceLanguage = new()
                {
                    Title = Title[i],
                    Description = Description[i],
                    LangCode = LangCode[i],
                    SEO = SEO[i],
                    OurServiceID = ourService.ID
                };
                _context.OurServiceLanguages.Add(OurServiceLanguage);
            }


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            EditVM editVM = new()
            {
                OurServiceLanguages = _context.OurServiceLanguages.Include(x => x.OurService).Where(x => x.OurServiceID == id).ToList(),
                OurService = _context.OurServices.FirstOrDefault(x => x.ID == id.Value)
            };

            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int OurServiceID, List<int> LangID, List<string> Title, List<string> Description, List<string> LangCode, string PhotoURL)
        {
            for (int i = 0; i < Title.Count; i++)
            {
                SEO seo = new();

                OurServiceLanguage ourServiceLanguage = new()
                {
                    ID = LangID[i],
                    Title = Title[i],
                    Description = Description[i],
                    SEO = seo.SeoURL(Title[i]),
                    LangCode = LangCode[i],
                    OurServiceID = OurServiceID
                };
                var updatedEntity = _context.Entry(ourServiceLanguage);
                updatedEntity.State = EntityState.Modified;

            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
