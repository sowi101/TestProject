using TestProject.Models;

namespace TestProject.ViewModels
{
    public class ProjectViewModel
    {
        public IEnumerable<Project>? Projects { get; set; }
        public IEnumerable<Competence>? Competences { get; set; }
    }
}
