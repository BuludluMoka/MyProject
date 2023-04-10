using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyProject.Application.DTOs
{
    public class CreateAnnouncementDto
    {
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required()]
        [Range(1903, 2023, ErrorMessage = "Year must be between {1} and {2}.")]
        public int? Year { get; set; }
        [Required]
        public long ModelId { get; set; }
        [Required]
        public long MakeId { get; set; }
        [Required]
        public byte BanTypeId { get; set; }
        [Required]
        public byte CurrencyId { get; set; }
        [Required]
        public List<IFormFile> images { get; set; }

    }
}
