using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Data;
using BLL.Models;
using System.Collections.ObjectModel;

namespace BLL.Services
{
    public class DBDataOperations : IDbCrud
    {
        IUnitOfWork dataBase;

        public DBDataOperations(IUnitOfWork repos)
        {
            dataBase = repos;
        }

        public List<DinnerMenuModel> GetAllDinnerMenus() {
            return dataBase.MenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).ToList();
        }
        public List<DishMenuModel> GetAllDishMenus() {
            return dataBase.MenuDishRepository.GetAll().Select(i => new DishMenuModel(i)).ToList();
        }
        public List<RecordModel> GetAllRecords() {
            return dataBase.RecordRepository.GetAll().Select(i => new RecordModel(i)).ToList();
        }
        public List<DishModel> GetAllDishes() {
            return dataBase.DishRepository.GetAll().Select(i => new DishModel(i)).ToList();
        }
        public List<UserModel> GetAllUsers() {
            return dataBase.UserRepository.GetAll().Select(i => new UserModel(i)).ToList();
        }

        public void DeleteDinnnerMenu(DateTime date)
        {
            Menu dinnerMenu = dataBase.MenuRepository.GetAll().Where(i => i.Date == date).FirstOrDefault();
            List<MenuDish> dishMenus = dataBase.MenuDishRepository.GetAll().Where(i => i.Menu == dinnerMenu.Id).ToList();
            if (dinnerMenu != null)
            {
                foreach (MenuDish dm in dishMenus)
                {
                    dataBase.MenuDishRepository.Delete(dm.Id);
                }
                dataBase.MenuRepository.Delete(dinnerMenu.Id);
                Save();
            }

        }
        public void DeletePeriodDinnnerMenu(DateTime dateFirst, DateTime dateSecond)
        {
            List<Menu> dinnerMenu = dataBase.MenuRepository.GetAll().Where(i => i.Date >= dateFirst).Where(i => i.Date <= dateSecond).ToList();
            foreach (Menu dim in dinnerMenu)
            {
                List<MenuDish> dishMenus = dataBase.MenuDishRepository.GetAll().Where(i => i.Menu == dim.Id).ToList();
                if (dim != null)
                {
                    foreach (MenuDish dm in dishMenus)
                    {
                        dataBase.MenuDishRepository.Delete(dm.Id);
                    }
                    dataBase.MenuRepository.Delete(dim.Id);
                    Save();
                }
            }  
        }
        public void DeleteDishMenu(int id)
        {
            MenuDish dishMenu = dataBase.MenuDishRepository.Get(id);
            if (dishMenu != null)
            {
                dataBase.MenuDishRepository.Delete(dishMenu.Id);
                Save();
            }
        }
        public void DeleteDish(int id)
        {
            Dish dish = dataBase.DishRepository.Get(id);
            MenuDish dishMenu = dataBase.MenuDishRepository.GetAll().Where(i => i.Dish == dish.Id).FirstOrDefault();
            if (dish != null)
            {
                dataBase.MenuDishRepository.Delete(dishMenu.Id);
                dataBase.DishRepository.Delete(dish.Id);
                Save();
            }          
        }
        public void DeleteRecord(int id)
        {
            Record record = dataBase.RecordRepository.Get(id);
            if (record != null)
            {
                dataBase.RecordRepository.Delete(record.Id);
                Save();
            }

        }
        public DinnerMenuModel GetDinnerMenu(int id)
        {
            return dataBase.MenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public DishMenuModel GetDishMenu(int id)
        {
            return dataBase.MenuDishRepository.GetAll().Select(i => new DishMenuModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public DishModel GetDish(int id)
        {
            return dataBase.DishRepository.GetAll().Select(i => new DishModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public RecordModel GetRecord(int id)
        {
            return dataBase.RecordRepository.GetAll().Select(i => new RecordModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public UserModel GetUser(int id)
        {
            return new UserModel(dataBase.UserRepository.Get(id));
        }
        public List<DishModel> GetDishesByDate(DateTime date)
        {
            DinnerMenuModel dinnerMenu = dataBase.MenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).Where(i => i.Date == date).FirstOrDefault();
            List<DishMenuModel> dishMenus = dataBase.MenuDishRepository.GetAll().Select(i => new DishMenuModel(i)).Where(i => i.Menu == dinnerMenu.Id).ToList();
            List<DishModel> dishes = new List<DishModel>(); 
            foreach (DishMenuModel di in dishMenus) 
            {
                dishes.Add(dataBase.DishRepository.GetAll().Select(i => new DishModel(i)).Where(i => i.Id == di.Dish).FirstOrDefault());
            }
            return dishes;
        }
        public Dictionary<DateTime, List<DishModel>> GetPeriodDish(DateTime dateFirst,DateTime dateSecond)
        {
            List<DinnerMenuModel> dinnerMenu = dataBase.MenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).Where(i => i.Date >= dateFirst).Where(i => i.Date <= dateSecond).ToList();
            Dictionary<DateTime, List<DishModel>> datesAndDishesDict = new Dictionary<DateTime, List<DishModel>>();
            for (int j = 0; j < dinnerMenu.Count(); j++)
            { 
                List<DishMenuModel> dishMenus = dataBase.MenuDishRepository.GetAll().Select(i => new DishMenuModel(i)).Where(i => i.Menu == dinnerMenu[j].Id).ToList();
                List<DishModel> dishes = new List<DishModel>();
                foreach (DishMenuModel di in dishMenus) 
                {
                    dishes.Add(dataBase.DishRepository.GetAll().Select(i => new DishModel(i)).Where(i => i.Id == di.Dish).FirstOrDefault());
                }
                datesAndDishesDict.Add(dinnerMenu[j].Date, dishes);
            }
            return datesAndDishesDict;
        }
        public void CreateDinnerMenu()
        {

        }
        public void CreateDishAndDinnerMenu(DateTime date, List<int> dishesList)
        {
            Menu dinnerMenuItems = new Menu 
            {
                Date = date,
            };

            dataBase.MenuRepository.Create(dinnerMenuItems);

            Save();

            List<Menu> dinners = dataBase.MenuRepository.GetAll();
            int dinnerKey = dinners[dinners.Count - 1].Id;

            List<MenuDish> dishMenuItems = new List<MenuDish>();

            foreach (int d in dishesList) 
            {
                dishMenuItems.Add(new MenuDish 
                {
                    Menu = dinnerKey,
                    Dish = d
                });
            }              
            foreach (MenuDish i in dishMenuItems)
            {
                dataBase.MenuDishRepository.Create(i);
            }
            Save();
        }
        public void UpdateDishMenu(DateTime date, List<int> dishesList) 
        {
            DinnerMenuModel dinnerMenu = dataBase.MenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).Where(i => i.Date == date).FirstOrDefault();
           // List<DishMenuModel> dishMenus = dataBase.DishMenuRepository.GetAll().Select(i => new DishMenuModel(i)).Where(i => i.Menu == dinnerMenu.Id).ToList();
            List<MenuDish> dishMenuItems = dataBase.MenuDishRepository.GetAll().Where(i => i.Menu == dinnerMenu.Id).ToList();
            int i = 0;
            foreach (int di in dishesList)
            {
                dishMenuItems[i].Dish = di;
                i++;
            }
            Save(); 
        }
        public void CreateDish()
        {

        }
        public void CreateRecord()
        {

        }
        public bool Save()
        {
            dataBase.Save();
            return true;
        }
    }
}
