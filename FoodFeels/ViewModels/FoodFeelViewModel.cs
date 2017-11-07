using System.ComponentModel.DataAnnotations;

namespace FoodFeels.ViewModels
{
    public class FoodFeelViewModel
    {
        public string FoodName { get; set; }

        [Range(0, 3)]
        public int Feeling { get; set; }
    }
}