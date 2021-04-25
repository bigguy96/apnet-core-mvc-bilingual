using System.ComponentModel.DataAnnotations;

namespace AspNetCoreBilingual.ViewModels
{
    public class ServiceViewModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}