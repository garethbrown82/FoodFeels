using FoodFeels.Models;
using System.Collections.Generic;
using System.Linq;

namespace FoodFeels.ViewModels
{
    public class ResultViewModel
    {
        public string Result;
        public List<Feeling> FeelingsList { get; set; }

        public IQueryable<string> FoodsList { get; set; }

    }
}