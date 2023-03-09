using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Published { get; set; }

        public virtual ICollection<Competence>? Competences { get; set; }
    }
}
