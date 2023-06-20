using System.ComponentModel.DataAnnotations;

namespace Homies.ViewModels
{
    public class EditEventViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 5)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(150, MinimumLength = 15)]
        public string Description { get; set; } = null!;

        [Required]
        public string Start { get; set; } = null!;

        [Required]
        public string End { get; set; } = null!;

        [Required]
        public int TypeId { get; set; }

        public int MyProperty { get; set; }

        public ICollection<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}
