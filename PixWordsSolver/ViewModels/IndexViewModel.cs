using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PixWordsSolver.ViewModels
{
    public class IndexViewModel
    {
        public GuessWordViewModel GuessWordViewModel { get; set; }

        public IEnumerable<string> ResultWithPics { get; set; }

        public string Result { get; set; }
    }
}