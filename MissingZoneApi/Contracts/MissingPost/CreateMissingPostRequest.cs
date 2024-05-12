using System.ComponentModel.DataAnnotations;

namespace MissingZoneApi.Contracts.MissingPost
{
    public class CreateMissingPostRequest
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Contents is required")]
        [MinLength(1, ErrorMessage = "Contents must have at least 1 item")]
        public List<string> Contents { get; set; }

        [Required(ErrorMessage = "Coordinates is required")]
        [MinLength(2, ErrorMessage = "Coordinates must have at least 2 items")]
        public List<double> Coordinates { get; set; }

        [Required(ErrorMessage = "Contact info is required")]
        public string ContactInfo { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        public string FatherName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
    }
}
