using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrzepisyWeb.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; }

        public Recipe Recipe { get; set; }

        public Image() { }

        
    }
}
