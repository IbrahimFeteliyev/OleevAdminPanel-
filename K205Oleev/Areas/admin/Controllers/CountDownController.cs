using Entities;
using K205Oleev.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using K205Oleev.Areas.admin.ViewModel;
using Helper.Methods;

namespace K205Oleev.Areas.admin.Controllers
{
    [Area("admin")]
    public class CountDownController : Controller
    {
        private readonly OleevDbContext _context;


        public CountDownController(OleevDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var countdown = _context.CountDownLanguages.Include(x => x.CountDown).Where(x => x.LangCode == "AZ").ToList();
            return View(countdown);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(List<string> Title, List<string> LangCode, List<string> SEO, int Count)
        {
            CountDown countDown = new()
            {
                Count = Count,
                CreatedDate = DateTime.Now
            };

            _context.CountDowns.Add(countDown);
            _context.SaveChanges();

            for (int i = 0; i < Title.Count; i++)
            {
                CountDownLanguage countDownLanguage = new()
                {
                    Title = Title[i],
                    LangCode = LangCode[i],
                    SEO = SEO[i],
                    CountDownID = countDown.ID
                };
                _context.CountDownLanguages.Add(countDownLanguage);
            }


            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            EditVM editVM = new()
            {
                CountDownLanguages = _context.CountDownLanguages.Include(x => x.CountDown).Where(x => x.CountDownID == id).ToList(),
                CountDown = _context.CountDowns.FirstOrDefault(x => x.ID == id.Value)
            };

            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int CountDownID, List<int> LangID, List<string> Title,  List<string> LangCode, int Count)
        {
            for (int i = 0; i < Title.Count; i++)
            {
                SEO seo = new();

                CountDownLanguage countDownLanguage = new()
                {
                    ID = LangID[i],
                    Title = Title[i],
                    
                    SEO = seo.SeoURL(Title[i]),
                    LangCode = LangCode[i],
                    CountDownID = CountDownID
                };
                var updatedEntity = _context.Entry(countDownLanguage);
                updatedEntity.State = EntityState.Modified;

            }
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
