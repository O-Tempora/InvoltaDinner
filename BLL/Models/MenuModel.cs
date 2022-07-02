using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Data;

namespace BLL.Models
{
    public class MenuModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public DayOfWeek dayOfWeek { get; set; }
        public bool is_dayOff { get; set; }
        public class DishInfoModel
        {
            public string label { get; set; }
            public decimal price { get; set; }
        }
        public DishInfoModel first_course { get; set; }
        public DishInfoModel second_course { get; set; }
        public MenuModel() { }
        public MenuModel(DateTime d) 
        {
            date = d;
            dayOfWeek = d.DayOfWeek;
            first_course = new DishInfoModel
            {
                label = "",
                price = 0
            };
            second_course = new DishInfoModel
            {
                label = "",
                price = 0
            };
        }
        public MenuModel(DinnerMenuModel m, List<DishModel> dishModels)
        {
            id = m.Id;
            date = m.Date;
            dayOfWeek = date.DayOfWeek;
            is_dayOff = m.isActive;

            first_course = new DishInfoModel
            {
                label = "",
                price = 0
            };

            second_course = new DishInfoModel
            {
                label = "",
                price = 0
            };

            foreach (DishModel d in dishModels)
            {
                if (d.Position == 1)
                {
                    first_course.label = d.Name;
                    first_course.price = d.Price;
                }
                else if (d.Position == 2)
                {
                    second_course.label = d.Name;
                    second_course.price = d.Price;
                }
            }

        }
    }
}