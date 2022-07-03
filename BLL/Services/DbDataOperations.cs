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
        public UserModel GetUserById(int id)
        {
            return new UserModel(dataBase.UserRepository.Get(id));
        }
        public UserModel GetUserByEmailAndPassword(string email, string password) 
        {
            return dataBase.UserRepository.GetAll().Select(i => new UserModel(i)).Where(i => i.Password == password).Where(i => i.Email == email).FirstOrDefault();
        }

        public List<DishModel> GetDishesByDate(DateTime date)
        {
            List<DishModel> dishes = new List<DishModel>();
            DinnerMenuModel dinnerMenu = dataBase.MenuRepository.GetAll()
                                        .Select(i => new DinnerMenuModel(i))
                                        .Where(i => i.Date == date).FirstOrDefault();
            if (dinnerMenu == null) 
                return dishes;
            List<DishMenuModel> dishMenus = dataBase.MenuDishRepository.GetAll()
                                            .Select(i => new DishMenuModel(i))
                                            .Where(i => i.Menu == dinnerMenu.Id).ToList();
            foreach (DishMenuModel di in dishMenus) 
            {
                dishes.Add(dataBase.DishRepository.GetAll()
                        .Select(i => new DishModel(i))
                        .Where(i => i.Id == di.Dish).FirstOrDefault());
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

        public void DeleteDinnnerMenu(DateTime date)
        {
            Menu dinnerMenu = dataBase.MenuRepository.GetAll().Where(i => i.Date == date).FirstOrDefault();
            List<MenuDish> dishMenus = dataBase.MenuDishRepository.GetAll()
                                        .Where(i => i.Menu == dinnerMenu.Id).ToList();
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
            List<Menu> dinnerMenu = dataBase.MenuRepository.GetAll().
                                    Where(i => i.Date >= dateFirst).Where(i => i.Date <= dateSecond).ToList();
            foreach (Menu dim in dinnerMenu)
            {
                List<MenuDish> dishMenus = dataBase.MenuDishRepository.GetAll()
                                            .Where(i => i.Menu == dim.Id).ToList();
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
            MenuDish dishMenu = dataBase.MenuDishRepository.GetAll()
                                .Where(i => i.Dish == dish.Id).FirstOrDefault();
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
            DinnerMenuModel dinnerMenu = dataBase.MenuRepository.GetAll()
                                        .Select(i => new DinnerMenuModel(i))
                                        .Where(i => i.Date == date).FirstOrDefault();
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
        public Tuple<List<MenuModel>, List<MenuModel>> GetPeriodMenu()
        {
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            List<MenuModel> current = new List<MenuModel>();
            List<MenuModel> next = new List<MenuModel>();
            List<DinnerMenuModel> dinnerMenus = dataBase.MenuRepository.GetAll()
                                                .Select(i => new DinnerMenuModel(i))
                                                .Where(i => i.Date >= date).Take(61).ToList();
            List<MenuModel> cycle (List<MenuModel> menuList)
            {
                for (DateTime counter = date; counter.Month == date.Month; counter = counter.AddDays(1))
                {
                    DinnerMenuModel dinnerMenu = dinnerMenus.Where(i => i.Date == counter).FirstOrDefault();
                    List<DishModel> dishes = GetDishesByDate(counter);
                    if (dinnerMenu == null || dishes == null)
                    {
                        menuList.Add(new MenuModel(counter));
                    }
                    else
                    {
                        menuList.Add(new MenuModel(dinnerMenu, dishes));
                    }
                }
                return menuList;
            }
            current = cycle(current);
            date = date.AddMonths(1);
            next = cycle(next);
            return Tuple.Create(current, next);
        }
        public void CreateDish(string name, decimal price, int position)
        {
            Dish dish = new Dish
            {
                Name = name,
                Price = price,
                Position = position
            };
            dataBase.DishRepository.Create(dish);

            Save();
        }
        public void CreateRecord(DateTime date, int userId, int position)
        {
            RecordModel record = dataBase.RecordRepository.GetAll()
                                                            .Select(i => new RecordModel(i))
                                                            .Where(i => i.Date == date && i.UserId == userId)
                                                            .FirstOrDefault();

            //если запись на дату уже сделана и стоит позиция "не ест", то удаляем запись
            if (position == 4 && record != default(RecordModel))
            {
                dataBase.RecordRepository.Delete(record.Id);
                return;
            }

            void createRecordDish (int? dishId, int recordId)
            {
                RecordDish recordDishItems = new RecordDish
                    {
                        Record = recordId,
                        Dish = dishId
                    };
                dataBase.RecordDishRepository.Create(recordDishItems);
                Save();
            }
            void createRecordsOnPos (int recordId, int? dishFirst, int? dishSecond)
            {
                //если стоит позиция "комплекс", то создаем записи на оба блюда
                if (position == 3)
                {
                    createRecordDish(dishFirst, recordId);
                    createRecordDish(dishSecond, recordId);
                    return;
                }
                //если стоит позиция "второе блюдо"
                if (position == 2)
                {
                    createRecordDish(dishSecond, recordId);
                    return;
                }
                //если стоит позиция "первое блюдо"
                if (position == 1)
                {
                    createRecordDish(dishFirst, recordId);
                    return;
                }
            }

            List<DishModel> dishes = GetDishesByDate(date);
            //запись на еще не установленное блюдо создается на блюдо с id 0
            int? dishFirstId = null; 
            int? DishSecondId = null;
            DishModel dishFirst = dishes.Select(i => new DishModel(i)).Where(i => i.Position == 1).LastOrDefault();
            if (dishFirst != default(DishModel))
            {
                dishFirstId = dishFirst.Id;
            }
            DishModel DishSecond = dishes.Select(i => new DishModel(i)).Where(i => i.Position == 2).LastOrDefault();
            if (DishSecond != default(DishModel))
            {
                DishSecondId = DishSecond.Id;
            }

            //если запись на дату уже сделана
            if (record != default(RecordModel))
            {
                //составляем список перектрестной таблицы по записям на блюда
                List<RecordDishModel> recordDishes = dataBase.RecordDishRepository.GetAll()
                                                .Select(i => new RecordDishModel(i))
                                                .Where(i => i.Record == record.Id).ToList();

                //если список пустой, то добавляем пустой объект для использования Count()
                recordDishes = (recordDishes == null) ? new List<RecordDishModel>() : recordDishes;

                //для более простой обработки запроса удалим записи на блюда, 
                //а впоследствии добавим необходимые
                if (recordDishes.Count() != 0)
                {
                    foreach (RecordDishModel recordDish in recordDishes)
                    {
                        dataBase.RecordDishRepository.Delete(recordDish.Id);
                        Save();
                    }
                }

                //если стоит позиция "комплекс" и в бд уже есть запись на оба блюда,
                //то изменять нечего, выходим из метода
                if (recordDishes.Count() > 1 && position == 3)
                    return;
                //если стоит позиция "комплекс", то создаем записи на оба блюда
                createRecordsOnPos(record.Id, dishFirstId, DishSecondId);
            }
            //если записи на дату еще нет
            else
            {
                Record recordItems = new Record
                {
                    Date = date,
                    UserId = userId,
                    Price = 0,
                    IsReady = 0
                };
                dataBase.RecordRepository.Create(recordItems);
                Save();

                List<Record> records = dataBase.RecordRepository.GetAll();
                records = (records == null) ? new List<Record>() : records;
                int recordKey = 1;
                if (records.Count() != 0)
                {
                    recordKey = records[records.Count - 1].Id;
                }
                createRecordsOnPos(recordKey, dishFirstId, DishSecondId);
            }
        }
        public bool Save()
        {
            dataBase.Save();
            return true;
        }
    }
}
