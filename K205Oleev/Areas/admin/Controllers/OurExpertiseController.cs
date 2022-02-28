using Entities;
using Helper.Methods;
using K205Oleev.Areas.admin.ViewModel;
using K205Oleev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K205Oleev.Areas.admin.Controllers
{

    [Area("admin")]
    public class OurExpertiseController : Controller
    {
        private readonly OleevDbContext _context;
        private IWebHostEnvironment _environment;

        public OurExpertiseController(OleevDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var ourexpertise = _context.OurExpertiseLanguages.Include(x => x.OurExpertise).Where(x => x.LangCode == "Az").ToList();
            return View(ourexpertise);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<string> Title, List<string> Description, List<string> SubTitle, List<string> SubDescription, List<string> LangCode, List<string> SEO, string PhotoURL, string Icon)
        {
            OurExpertise ourExpertise = new()
            {
                PhotoURL = PhotoURL,
                Icon = Icon,
                CreatedDate = DateTime.Now,
            };

            _context.OurExpertises.Add(ourExpertise);
            _context.SaveChanges();

            for (int i = 0; i < Title.Count; i++)
            {
                OurExpertiseLanguage OurExpertiseLanguage = new()
                {
                    Title = Title[i],
                    Description = Description[i],
                    SubTitle = SubTitle[i],
                    SubDescription = SubDescription[i],
                    LangCode = LangCode[i],
                    SEO = SEO[i],
                    OurExpertiseID = ourExpertise.ID,
                };
                _context.OurExpertiseLanguages.Add(OurExpertiseLanguage);
            }


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            EditVM editVM = new()
            {
                OurExpertiseLanguages = _context.OurExpertiseLanguages.Include(x => x.OurExpertise).Where(x => x.OurExpertiseID == id).ToList(),
                OurExpertise = _context.OurExpertises.FirstOrDefault(x => x.ID == id.Value)
            };

            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int OurExpertiseID, List<int> LangID, List<string> Title, List<string> Description, List<string> SubTitle, List<string> SubDescription, List<string> LangCode, string PhotoURL, string Icon)
        {
            for (int i = 0; i < Title.Count; i++)
            {
                SEO seo = new();

                OurExpertiseLanguage ourExpertiseLanguage = new()
                {
                    ID = LangID[i],
                    Title = Title[i],
                    Description = Description[i],
                    SubTitle = SubTitle[i],
                    SubDescription = SubDescription[i],
                    SEO = seo.SeoURL(Title[i]),
                    LangCode = LangCode[i],
                    OurExpertiseID = OurExpertiseID
                };
                var updatedEntity = _context.Entry(ourExpertiseLanguage);
                updatedEntity.State = EntityState.Modified;

            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }

}
