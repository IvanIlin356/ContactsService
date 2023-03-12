using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Service.Models
{
    //[Index(nameof(Email), IsUnique = true)]
    public class Contact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        [StringLength(50, MinimumLength = 3)]
        public string Salution { get; set; }

        //[Required]
        [StringLength(150, MinimumLength = 3)]
        public string Firstname { get; set; }
        
        //[Required]
        [StringLength(150, MinimumLength = 3)]
        public string Lastname { get; set; }

        public string? Displayname { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? CreationTimestamp { get; set; }
        public DateTime? LastChangeTimestamp { get; set; }

        [NotMapped]
        public bool? NotifyHasBirthdaySoon { get { return Birthdate.HasValue ? DateTime.Today <= Birthdate.Value && DateTime.Today >= Birthdate.Value.AddDays(-14) : false; } }

        //[Required]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
