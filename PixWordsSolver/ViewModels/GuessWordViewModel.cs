namespace PixWordsSolver.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class GuessWordViewModel
    {
        [DisplayName("Дума")]
        [Required(ErrorMessage = "Полето ДУМА е задължително")]
        public string Word { get; set; }

        [DisplayName("Букви")]
        //[Required(ErrorMessage = "Полето БУКВИ е задължително")]
        public string Letters { get; set; }
    }
}