﻿using System.ComponentModel.DataAnnotations;
namespace RestaurantsAPI.Models
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public bool? HasDelivery { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }

        [Required]
        [MaxLength(25)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        [Required]
        [MaxLength(50)]
        public string PostalCode { get; set; }
    }
}
/*
ValidationAttributes buildin:
[CreditCard]
[Compare("otherProperty")]
[EmailAddress]
[Phone]
[Range(minVal,maxVal)]
RegularExpression(pattern)]
*/