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
        public Tuple<List<RecordPosModel>, List<RecordPosModel>> GetPeriodRecord(int id);
        UserModel GetUserById(int id);
        UserModel GetUserByEmailAndPassword(string email, string password);
        Dictionary<DateTime, List<DishModel>> GetPeriodDish(DateTime datefirst,DateTime seconddate);
        List<TransactionModel> GetUserTransactions(int User);
        decimal GetUserBalance(int userId);

        void DeleteDinnnerMenu(DateTime date);
        void DeletePeriodDinnnerMenu(DateTime dateFirst, DateTime dateSecond);
        void DeleteDishMenu(int id);
        void DeleteDish(int id);
        void DeleteRecord(int id);
        void CreateUser(SignUpModel upm);
        void CreateDishAndDinnerMenu(DateTime date, List<int> dishesList);
        void CreateDish(string name, decimal price, int position);
        void CreateOrUpdateRecord(DateTime date, int userId, int position);
        Tuple<List<MenuModel>, List<MenuModel>> GetPeriodMenu();
        void UpdateDishMenu(DateTime date, List<int> dishesList);
        void ApproveUser(int userId);
        void ChangeUserBalance(int adminId, int userId, decimal price, DateTime date);
        bool CheckUserByEmail(string email);
        Task ResetPasswordOfUser(string email);
        bool ChangePasswordOfUser(ChangePasswordModel changePasswordModel);
    }
}
