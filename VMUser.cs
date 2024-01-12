using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.ViewModel
{
    public class VMUser
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [Display(Name = "Phone")]
        public string? Phone { get; set; }
        public string? Address { get; set; }

        [Required(ErrorMessage = "StateName is required")]
        [Display(Name = "StateName")]
        public string? StateName { get; set; }

        [Required(ErrorMessage = "CityName is required")]
        [Display(Name = "CityName")]
        public string? CityName { get; set; }
        public List<State> DDLState { get; set; }
        public State State { get; set; }
        public List<City> DDLCity { get; set; }
        public City City { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}
