using System;
using System.ComponentModel.DataAnnotations;

namespace FoodFeels.Models
{
    public class Food
    {
        public int ID { get; set; }
        public string FoodName { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }
    }
}