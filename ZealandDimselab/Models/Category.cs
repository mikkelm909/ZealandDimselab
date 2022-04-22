using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZealandDimselab.Models
{
    public class Category
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int CategoryId { get; set; }
        [Required] [MaxLength(50)] public string CategoryName { get; set; }
        [MaxLength(100)] public string ImageName { get; set; }

        public Category()
        {

        }
    }
}
