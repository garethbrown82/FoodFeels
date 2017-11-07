using FoodFeels.Models;
using FoodFeels.Utilities;
using FoodFeels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FoodFeels.Controllers
{
    public class HomeController : Controller
    {
        FoodFeelsDBContext context = new FoodFeelsDBContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult FoodFeel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FoodFeel(FoodFeelViewModel foodModel)
        {
            ViewBag.SaveMessage = "";

            if (ModelState.IsValid)
            {               
                // Adds food if a food name is entered
                if (foodModel.FoodName != null)
                {
                    var foodItem = new Food()
                    {
                        FoodName = foodModel.FoodName,
                        Time = DateTime.Now
                    };
                    context.Foods.Add(foodItem);
                    ViewBag.SaveMessage = "Saved!";
                }
               
                // Adds a feeling if a feeling score greater than 0 is entered
                if(foodModel.Feeling != 0)
                {
                    var feeling = new Feeling()
                    {
                        FeelLevel = foodModel.Feeling,
                        Time = DateTime.Now
                    };
                    context.Feelings.Add(feeling);
                    ViewBag.SaveMessage = "Saved!";
                }
                
                context.SaveChanges();             

                ModelState.Clear();

                return View();
            }

            return View();

        }

        public ActionResult Results()
        {
            ResultViewModel resultView = new ResultViewModel();

            // Gets distinct list of foods, Creates a List of Type FoodAnalysis and then adds each distinct food name to a new FoodAnalysis Object.
            resultView.FoodsList = context.Foods.Select(f => f.FoodName).Distinct();

            List<FoodAnalysis> distFoodsAnalyse = new List<FoodAnalysis>();

            foreach (string food in resultView.FoodsList)
            {
                FoodAnalysis foodToAdd = new FoodAnalysis()
                {
                    FoodName = food,
                    FoodCountRed = 0
                };

                distFoodsAnalyse.Add(foodToAdd);
            }

            
            // Get the date the is one month ago from todays date, then gets a list of Red Feels since that date
            DateTime oneMonthAgo = DateTime.UtcNow.AddMonths(-1);


            List<Feeling> feelingsList = context.Feelings.Where(f => f.Time > oneMonthAgo).ToList();

            
            // For each feeling Count the number of times each food was eaten six hours before
            foreach (Feeling feeling in feelingsList)
            {
                DateTime sixHoursAgo = feeling.Time.AddHours(-6);
                var foodsFromSixHours = context.Foods.Where(fd => fd.Time > sixHoursAgo && fd.Time < feeling.Time).ToList();

                // Counts the number of times each food appeared 6 hours before each feeling
                foreach (var food in foodsFromSixHours)
                {
                    foreach (var distFood in distFoodsAnalyse)
                    {
                        if (food.FoodName == distFood.FoodName && feeling.FeelLevel == 3)
                        {
                            distFood.FoodCountRed++;
                        }

                        if (food.FoodName == distFood.FoodName && feeling.FeelLevel == 1)
                        {
                            distFood.FoodCountGreen++;
                        }
                    }
                }
            }

            // Calculate the percentage food was eaten before each feel

            foreach(var food in distFoodsAnalyse)
            {
                food.PercentageRed = (((double)food.FoodCountRed / (double)feelingsList.Where(fl => fl.FeelLevel == 3).ToList().Count) * 100);
                food.PercentageGreen = (((double)food.FoodCountGreen / (double)feelingsList.Where(fl => fl.FeelLevel == 1).ToList().Count) * 100);
            }

            // Order red feelings

            for (int i = distFoodsAnalyse.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (distFoodsAnalyse[j].PercentageRed > distFoodsAnalyse[j + 1].PercentageRed)
                    {
                        var temp = distFoodsAnalyse[j];
                        distFoodsAnalyse[j] = distFoodsAnalyse[j + 1];
                        distFoodsAnalyse[j + 1] = temp;
                    }
                }
            }

            // Add the Food with the highest red percentage to the list

            resultView.Result = $"You ate {distFoodsAnalyse[distFoodsAnalyse.Count - 1].FoodName} {distFoodsAnalyse[distFoodsAnalyse.Count - 1].GetPercentageRed()} of the time withing 6 hours before feeling bad, and only {distFoodsAnalyse[distFoodsAnalyse.Count - 1].GetPercentageGreen()} of the time within 6 hours before feeling good!";

            return View(resultView);
        }
    }
}