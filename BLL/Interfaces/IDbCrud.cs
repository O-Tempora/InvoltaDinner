using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Interfaces
{
    public interface IDbCrud
    {
        List<DinnerMenuModel> GetAllDinnerMenus();
        List<DishMenuModel> GetAllDishMenus();
        List<RecordModel> GetAllRecords();
        List<DishModel> GetAllDishes();
        void DeleteDinnnerMenu(int id);
        void DeleteDishMenu(int id);
        void DeleteDish(int id);
        void DeleteRecord(int id);
        DinnerMenuModel GetDinnerMenu(int id);
        DishMenuModel GetDishMenu(int id);
        DishModel GetDish(int id);
        RecordModel GetRecord(int id);
        void CreateDinnerMenu();
        void CreateDishMenu();
        void CreateDish();
        void CreateRecord();
    }
}