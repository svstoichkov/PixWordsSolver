namespace PixWordsSolver.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;
    using System.Web.Mvc;

    using ViewModels;

    public class HomeController : Controller
    {
        private static string path = HostingEnvironment.ApplicationPhysicalPath;
        private static readonly string[] dic = System.IO.File.ReadAllLines(Path.Combine(path, "dic.txt")); 

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(GuessWordViewModel model)
        {
            model.Letters = model.Letters?.ToLower();
            model.Word = model.Word?.ToLower();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = dic.Where(x => x.Length == model.Word.Length);
            
            for (int i = 0; i < model.Word.Length; i++)
            {
                if (model.Word[i] != '*')
                {
                    var index = i;
                    result = result.Where(x => x[index] == model.Word[index]);
                }
            }
            
            result = result.Where(x => x.All(y => model.Letters.IndexOf(y) != -1));

            if (result.Any())
            {
                this.ViewBag.Result = string.Join(", ", result);
            }
            else
            {
                this.ViewBag.Result = "Думата не може да бъде открита.";
            }
            
            return View(new GuessWordViewModel());
        }
    }
}