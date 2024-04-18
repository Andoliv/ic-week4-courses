using System.ComponentModel.DataAnnotations;

namespace ServiceBasedApplication.ViewModels;

public class CreateCourseRequest
{
    [Required(ErrorMessage = "You must input a title")]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    public required string Title { get; set; }

    public int Credits { get; set; }
}

