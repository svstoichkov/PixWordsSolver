namespace PixWordsSolver.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Hosting;
    using System.Web.Mvc;

    using ViewModels;

    public class HomeController : Controller
    {
        private static readonly string path = HostingEnvironment.ApplicationPhysicalPath;
        private static readonly string[] dic = System.IO.File.ReadAllLines(Path.Combine(path, "dic.txt")); 
        private static readonly string[] files = System.IO.File.ReadAllLines(Path.Combine(path, "files.txt")); 

        public ActionResult Index()
        {
            return View(new IndexViewModel());
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel model)
        {
            model.GuessWordViewModel.Letters = model.GuessWordViewModel.Letters?.ToLower();
            model.GuessWordViewModel.Word = model.GuessWordViewModel.Word?.ToLower();

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var result = this.GetResult(files, model);

            if (result.Any())
            {
                model.ResultWithPics = result;
            }
            else
            {
                model.Result = string.Join(", ", this.GetResult(dic, model));
            }
            
            return View(model);
        }

        private IEnumerable<string> GetResult(string[] dictionary, IndexViewModel model)
        {
            var result = dictionary.Where(x => x.Length == model.GuessWordViewModel.Word.Length);

            for (int i = 0; i < model.GuessWordViewModel.Word.Length; i++)
            {
                if (model.GuessWordViewModel.Word[i] != '*')
                {
                    var index = i;
                    result = result.Where(x => x[index] == model.GuessWordViewModel.Word[index]);
                }
            }

            if (model.GuessWordViewModel.Letters != null)
            {
                result = result.Where(x => x.All(y => model.GuessWordViewModel.Letters.IndexOf(y) != -1));
            }

            return result;
        }
    }
}