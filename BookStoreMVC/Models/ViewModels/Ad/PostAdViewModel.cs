using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace BookStoreMVC.Models.ViewModels.Ad
{
    public class PostAdViewModel
    {
        [DataType(DataType.ImageUrl)]
        [Required]
        public IFormFileCollection Images { get; set; }
    }
}