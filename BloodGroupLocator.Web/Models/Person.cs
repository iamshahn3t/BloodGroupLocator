using System.ComponentModel.DataAnnotations;

namespace BloodGroupLocator.Web.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Blood group is required")]
        public string BloodGroup { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
