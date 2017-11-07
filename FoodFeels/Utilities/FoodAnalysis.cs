using System;

namespace FoodFeels.Utilities
{
    public class FoodAnalysis
    {
        public string FoodName { get; set; }
        public int FoodCountRed { get; set; }
        public double PercentageRed { get; set; }
        public int FoodCountGreen { get; set; }
        public double PercentageGreen { get; set; }

        public string GetPercentageRed()
        {
            return String.Format("{0:0.00}%", PercentageRed);
        }

        public string GetPercentageGreen()
        {
            return String.Format("{0:0.00}%", PercentageGreen);
        }
    }
}