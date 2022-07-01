using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
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
         public List<RecordDishModel> GetAllRecordDishes() {
            return dataBase.RecordDishRepository.GetAll().Select(i => new RecordDishModel(i)).ToList();
        }

        public void DeleteDinnnerMenu(DateTime date)
        {
            DinnerMenu dinnerMenu = dataBase.DinnerMenuRepository.GetAll().Where(i => i.Date == date).FirstOrDefault();
            List<DishMenu> dishMenus = dataBase.DishMenuRepository.GetAll().Where(i => i.Menu == dinnerMenu.Id).ToList();
            if (dinnerMenu != null)
            {
                foreach (DishMenu dm in dishMenus)
                {
                    dataBase.DishMenuRepository.Delete(dm.Id);
                }
                dataBase.DinnerMenuRepository.Delete(dinnerMenu.Id);
                Save();
            }

        }
        public void DeletePeriodDinnnerMenu(DateTime dateFirst, DateTime dateSecond)
        {
            List<DinnerMenu> dinnerMenu = dataBase.DinnerMenuRepository.GetAll().Where(i => i.Date >= dateFirst).Where(i => i.Date <= dateSecond).ToList();
            foreach (DinnerMenu dim in dinnerMenu)
            {
                List<DishMenu> dishMenus = dataBase.DishMenuRepository.GetAll().Where(i => i.Menu == dim.Id).ToList();
                if (dim != null)
                {
                    foreach (DishMenu dm in dishMenus)
                    {
                        dataBase.DishMenuRepository.Delete(dm.Id);
                    }
                    dataBase.DinnerMenuRepository.Delete(dim.Id);
                    Save();
                }
            }  
        }
        public void DeleteDishMenu(int id)
        {
            DishMenu dishMenu = dataBase.DishMenuRepository.Get(id);
            if (dishMenu != null)
            {
                dataBase.DishMenuRepository.Delete(dishMenu.Id);
                Save();
            }
        }
        public void DeleteDish(int id)
        {
            Dish dish = dataBase.DishRepository.Get(id);
            DishMenu dishMenu = dataBase.DishMenuRepository.GetAll().Where(i => i.Dish == dish.Id).FirstOrDefault();
            RecordDish recordDish = dataBase.RecordDishRepository.GetAll().Where(i => i.Dish == dish.Id).FirstOrDefault();
            if (dish != null)
            {
                dataBase.RecordDishRepository.Delete(recordDish.Id);
                dataBase.DishMenuRepository.Delete(dishMenu.Id);
                dataBase.DishRepository.Delete(dish.Id);
                Save();
            }          
        }
        public void DeleteRecord(int id)
        {
            Record record = dataBase.RecordRepository.Get(id);
            RecordDish recordDish = dataBase.RecordDishRepository.GetAll().Where(i => i.Record == record.Id).FirstOrDefault();
            if (record != null)
            {
                dataBase.RecordDishRepository.Delete(recordDish.Id);
                dataBase.RecordRepository.Delete(record.Id);
                Save();
            }

        }
        public void DeleteRecordDish(int id)
        {
            RecordDish recordDish = dataBase.RecordDishRepository.Get(id);
            if (recordDish != null)
            {
                dataBase.RecordDishRepository.Delete(recordDish.Id);
                Save();
            }

        }
        public DinnerMenuModel GetDinnerMenu(int id)
        {
            return dataBase.DinnerMenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public DishMenuModel GetDishMenu(int id)
        {
            return dataBase.DishMenuRepository.GetAll().Select(i => new DishMenuModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public DishModel GetDish(int id)
        {
            return dataBase.DishRepository.GetAll().Select(i => new DishModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public RecordDishModel GetRecordDish(int id)
        {
            return dataBase.RecordDishRepository.GetAll().Select(i => new RecordDishModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public RecordModel GetRecord(int id)
        {
            return dataBase.RecordRepository.GetAll().Select(i => new RecordModel(i)).Where(i => i.Id == id).FirstOrDefault();
        }
        public List<DishModel> GetDishesByDate(DateTime date)
        {
            DinnerMenuModel dinnerMenu = dataBase.DinnerMenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).Where(i => i.Date == date).FirstOrDefault();
            List<DishMenuModel> dishMenus = dataBase.DishMenuRepository.GetAll().Select(i => new DishMenuModel(i)).Where(i => i.Menu == dinnerMenu.Id).ToList();
            List<DishModel> dishes = new List<DishModel>(); 
            foreach (DishMenuModel di in dishMenus) 
            {
                dishes.Add(dataBase.DishRepository.GetAll().Select(i => new DishModel(i)).Where(i => i.Id == di.Dish).FirstOrDefault());
            }
            return dishes;
        }
        public Dictionary<DateTime, List<DishModel>> GetPeriodDish(DateTime dateFirst,DateTime dateSecond)
        {
            List<DinnerMenuModel> dinnerMenu = dataBase.DinnerMenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).Where(i => i.Date >= dateFirst).Where(i => i.Date <= dateSecond).ToList();
            Dictionary<DateTime, List<DishModel>> datesAndDishesDict = new Dictionary<DateTime, List<DishModel>>();
            for (int j = 0; j < dinnerMenu.Count(); j++)
            { 
                List<DishMenuModel> dishMenus = dataBase.DishMenuRepository.GetAll().Select(i => new DishMenuModel(i)).Where(i => i.Menu == dinnerMenu[j].Id).ToList();
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
            DinnerMenu dinnerMenuItems = new DinnerMenu 
            {
                Date = date,
            };

            dataBase.DinnerMenuRepository.Create(dinnerMenuItems);

            Save();

            List<DinnerMenu> dinners = dataBase.DinnerMenuRepository.GetAll();
            int dinnerKey = dinners[dinners.Count - 1].Id;

            List<DishMenu> dishMenuItems = new List<DishMenu>();

            foreach (int d in dishesList) 
            {
                dishMenuItems.Add(new DishMenu 
                {
                    Menu = dinnerKey,
                    Dish = d
                });
            }              
            foreach (DishMenu i in dishMenuItems)
            {
                dataBase.DishMenuRepository.Create(i);
            }
            Save();
        }
        public void UpdateDishMenu(DateTime date, List<int> dishesList) 
        {
            DinnerMenuModel dinnerMenu = dataBase.DinnerMenuRepository.GetAll().Select(i => new DinnerMenuModel(i)).Where(i => i.Date == date).FirstOrDefault();
           // List<DishMenuModel> dishMenus = dataBase.DishMenuRepository.GetAll().Select(i => new DishMenuModel(i)).Where(i => i.Menu == dinnerMenu.Id).ToList();
            List<DishMenu> dishMenuItems = dataBase.DishMenuRepository.GetAll().Where(i => i.Menu == dinnerMenu.Id).ToList();
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
        public void CreateRecordDish()
        {

        }
        public bool Save()
        {
            if (dataBase.Save() > 0) return true;
            return false;
        }
    }
}
