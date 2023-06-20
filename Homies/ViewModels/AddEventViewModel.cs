﻿using Homies.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Homies.ViewModels
{
    public class AddEventViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(15,MinimumLength =5)]
        public string Name { get; set; } = null!;
        [Required]
        [StringLength(150,MinimumLength =15)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }

        public ICollection<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}

/*[Key]
public int Id { get; set; }

[Required]
[MaxLength(NameMaxLength)]
[MinLength(NameMinLength)]
public string Name { get; set; } = null!;
[Required]
[MaxLength(DescriptionMaxLength)]
[MinLength(DescriptionMinLength)]
public string Description { get; set; } = null!;

[Required]
public string OrganiserId { get; set; } = null!;

[Required]
[ForeignKey(nameof(OrganiserId))]
public IdentityUser Organiser { get; set; } = null!;

[Required]
public DateTime CreatedOn { get; set; }

[Required]
public DateTime Start { get; set; }

[Required]
public DateTime End { get; set; }

[Required]
public int TypeId { get; set; }

[Required]
public Type Type { get; set; } = null!;*/
