using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eDoc.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        public String Doc_Name { get; set; }
        public String Date { get; set; }
        public String Illness { get; set; }
        public String Medicines { get; set; }

    }
}
