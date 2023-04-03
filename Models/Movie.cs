using System.ComponentModel.DataAnnotations;

namespace RestAPI.Models
{
	public class Movie
	{
		public int Id { get; set; }

		[Required]
		[StringLength(60, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 60 characters")]
		public string? Title { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "Genre must be between 3 and 30 characters")]
		[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Genre must start with a capital letter and contain only letters")]
		public string? Genre { get; set; }

		[Required]
		[Range(1700, 2023, ErrorMessage = "Year must be between 1700 and 2023")]
		public int Year { get; set; }
	}
}