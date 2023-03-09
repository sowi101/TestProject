using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TestProject.Models
{
    public class Competence
    {
        public int CompetenceId { get; set; }

        [Required]
        [Display(Name = "Kompetens")]
        public string? Name { get; set; }
        public virtual ICollection<Project>? Projects { get; set; }
    }
}
