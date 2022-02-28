using Entities;
using K205Oleev.Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using K205Oleev.Areas.admin.ViewModel;
using Helper.Methods;

namespace K205Oleev.Areas.admin.Controllers
{
    [Area("admin")]
    public class InfoController : Controller
    {
        private readonly OleevDbContext _context;

        public InfoController(OleevDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var info = _context.InfoLanguages.Include(x => x.Info).Where(x => x.LangCode == "Az").ToList();
            return View(info);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<string> Title, List<string> Description, List<string> LangCode, List<string> SEO, string PhotoURL)
        {
            Info info = new()
            {
                PhotoURL = PhotoURL,
                CreatedDate = DateTime.Now
            };

            _context.Infos.Add(info);
            _context.SaveChanges();

            for (int i = 0; i < Title.Count; i++)
            {
                InfoLanguage infoLanguage = new()
                {
                    Title = Title[i],
                    Description = Description[i],
                    LangCode = LangCode[i],
                    SEO = SEO[i],
                    InfoID = info.ID
                };
                _context.InfoLanguages.Add(infoLanguage);
            }


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            EditVM editVM = new()
            {
                InfoLanguages = _context.InfoLanguages.Include(x => x.Info).Where(x => x.InfoID == id).ToList(),
                Info = _context.Infos.FirstOrDefault(x => x.ID == id.Value)
            };

            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int InfoID, List<int> LangID, List<string> Title, List<string> Description, List<string> LangCode, string PhotoURL)
        {
            for (int i = 0; i < Title.Count; i++)
            {
                SEO seo = new();

                InfoLanguage infoLanguage = new()
                {
                    ID = LangID[i],
                    Title = Title[i],
                    Description = Description[i],
                    SEO = seo.SeoURL(Title[i]),
                    LangCode = LangCode[i],
                    InfoID = InfoID
                };
                var updatedEntity = _context.Entry(infoLanguage);
                updatedEntity.State = EntityState.Modified;

            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
