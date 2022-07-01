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


        public void DeleteDinnnerMenu(int id)
        {
            DinnerMenu dinnerMenu = dataBase.DinnerMenuRepository.Get(id);
            DishMenu dishMenu = dataBase.DishMenuRepository.GetAll().Where(i => i.Menu == dinnerMenu.Id).FirstOrDefault();
            if (dinnerMenu != null)
            {
                dataBase.DishMenuRepository.Delete(dishMenu.Id);
                dataBase.DinnerMenuRepository.Delete(dinnerMenu.Id);
                Save();
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
        public void CreateDinnerMenu()
        {

        }
        public void CreateDishMenu()
        {

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
