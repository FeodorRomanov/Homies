using System.ComponentModel.DataAnnotations;
using static Homies.Common.Validations.Type;

namespace Homies.Data.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; } = null!;

        public ICollection<Event> Events { get; set; }=new List<Event>();
    }
}
