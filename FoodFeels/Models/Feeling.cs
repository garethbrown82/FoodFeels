using System;
using System.ComponentModel.DataAnnotations;

namespace FoodFeels.Models
{
    public class Feeling
    {
        public int ID { get; set; }

        [Range(0, 3)]
        public int FeelLevel { get; set; }
        public DateTime Time { get; set; }
    }
}