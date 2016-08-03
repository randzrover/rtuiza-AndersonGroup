using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandyTuiza_CodingExam.Models
{
    public class UsedValues
    {
        [Key]
        public int ID { get; set; }

        public int ParentID { get; set; }
        public string Values { get; set; }
    }
}