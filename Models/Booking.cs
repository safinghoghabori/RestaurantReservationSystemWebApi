using System;
using System.ComponentModel.DataAnnotations;


namespace RestaurantReservationSystem.Api.Models;
public class Booking
{
    [Key]
    public int Bid { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Name can't be longer than 30 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Age is required")]
    [Range(1, 100, ErrorMessage = "Age must be between 1 and 100")]
    public int Age { get; set; }

    public string Gender { get; set; }

    [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "Please enter a valid 10-digit phone number starting with 6, 7, 8, or 9.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email should be provided")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Date and Time can't be empty.")]
    public DateTime? DateTime { get; set; }

    [Required(ErrorMessage = "Capacity can't be empty!")]
    [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
    public int Capacity { get; set; }

    public string? UserId { get; set; } 
}

