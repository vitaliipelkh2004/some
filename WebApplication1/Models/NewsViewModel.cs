using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
   
    public class NewsViewModel
    { 
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string Date { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public string Time { get; set; }
      }



}