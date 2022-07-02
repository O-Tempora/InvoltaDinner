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
        List<UserModel> GetAllUsers();

        DinnerMenuModel GetDinnerMenu(int id);
        DishMenuModel GetDishMenu(int id);
        DishModel GetDish(int id);
        RecordModel GetRecord(int id);
        UserModel GetUserById(int id);
        UserModel GetUserByEmailAndPassword(string email, string password);
        List<DishModel> GetDishesByDate(DateTime date);
        Dictionary<DateTime, List<DishModel>> GetPeriodDish(DateTime datefirst,DateTime seconddate);

        void DeleteDinnnerMenu(DateTime date);
        void DeletePeriodDinnnerMenu(DateTime dateFirst, DateTime dateSecond);
        void DeleteDishMenu(int id);
        void DeleteDish(int id);
        void DeleteRecord(int id);

        void CreateDishAndDinnerMenu(DateTime date, List<int> dishesList);

        void UpdateDishMenu(DateTime date, List<int> dishesList);
    }
}