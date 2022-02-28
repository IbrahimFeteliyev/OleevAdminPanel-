using Entities;
using Helper.Methods;
using K205Oleev.Areas.admin.ViewModel;
using K205Oleev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace K205Oleev.Areas.admin.Controllers
{

    [Area("admin")]
    public class CaseStudyController : Controller
    {

        private readonly OleevDbContext _context;
        private IWebHostEnvironment _environment;

        public CaseStudyController(OleevDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var caseStudy = _context.CaseStudyLanguages.Include(x => x.CaseStudy).Where(x => x.LangCode == "Az").ToList();
            return View(caseStudy);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<string> Title, List<string> LangCode, List<string> SEO, string PhotoURL)
        {
            CaseStudy caseStudy = new()
            {
                PhotoURL = PhotoURL,
                CreatedDate = DateTime.Now
            };

            _context.CaseStudies.Add(caseStudy);
            _context.SaveChanges();

            for (int i = 0; i < Title.Count; i++)
            {
                CaseStudyLanguage caseStudyLanguage = new()
                {
                    Title = Title[i],
                    LangCode = LangCode[i],
                    SEO = SEO[i],
                    CaseStudyID = caseStudy.ID
                };
                _context.CaseStudyLanguages.Add(caseStudyLanguage);
            }


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            EditVM editVM = new()
            {
                CaseStudyLanguages = _context.CaseStudyLanguages.Include(x => x.CaseStudy).Where(x => x.CaseStudyID == id).ToList(),
                CaseStudy = _context.CaseStudies.FirstOrDefault(x => x.ID == id.Value)
        };

            return View(editVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int CaseStudyID, List<int> LangID, List<string> Title,  List<string> LangCode, string PhotoURL)
        {
            for (int i = 0; i < Title.Count; i++)
            {
                SEO seo = new();

                CaseStudyLanguage caseStudyLanguage = new()
                {
                    ID = LangID[i],
                    Title = Title[i],
                    SEO = seo.SeoURL(Title[i]),
                    LangCode = LangCode[i],
                    CaseStudyID = CaseStudyID
                };
                var updatedEntity = _context.Entry(caseStudyLanguage);
                updatedEntity.State = EntityState.Modified;

            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
