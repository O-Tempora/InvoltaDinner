using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Data;
using BLL.Models;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using MimeKit;
using MailKit.Net.Smtp;
using Newtonsoft.Json;
using System.Security.Claims;

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
            return dataBase.UserRepository.GetAll().Select(i => new UserModel(i))
                                                    .OrderBy(i => i.IsApproved)
                                                    .ThenBy(i => i.Role)
                                                    .ThenBy(i => i.Name)
                                                    .ToList();
        }
        public List<TransactionModel> GetAllTransactions() {
            List<TransactionModel> transactionName = new List<TransactionModel>();
            List<TransactionModel> transactions = dataBase.TransactionRepository.GetAll()
                                                                                .Select(i => new TransactionModel(i))
                                                                                .OrderByDescending(i => i.Id)
                                                                                .ToList();
            List<UserModel> users = GetAllUsers();
            foreach (TransactionModel t in transactions)
            {
                t.Username = users.Where(i => i.Id == t.User).FirstOrDefault().Name;
                transactionName.Add(t);
            }
            return transactionName;
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
        public List<RecordNameModel> GetDayRecords(DateTime date)
        {
            List<RecordNameModel> dayRecordsList = new List<RecordNameModel>();
            List<RecordModel> dayrecords = dataBase.RecordRepository.GetAll()
                                        .Select(i => new RecordModel(i))
                                        .Where (i => i.Date == date).ToList();
            foreach (RecordModel d in dayrecords)
            {
                UserModel user = dataBase.UserRepository.GetAll()
                                .Select(i => new UserModel(i))
                                .Where (i => i.Id == d.UserId).FirstOrDefault();
                string username = user.Name;
                List<RecordDishModel> recordDishes = dataBase.RecordDishRepository.GetAll()
                                                .Select(i => new RecordDishModel(i))
                                                .Where(i => i.Record == d.Id).ToList();
                recordDishes = (recordDishes == null) ? new List<RecordDishModel>() : recordDishes;
                List<DishWoPriceModel> dishesList = new List<DishWoPriceModel>();
                foreach (RecordDishModel rd in recordDishes)
                {
                    DishModel currDish = dataBase.DishRepository.GetAll()
                                        .Select(i => new DishModel(i))
                                        .Where(i => i.Id == rd.Dish).FirstOrDefault();
                    DishWoPriceModel newDish = new DishWoPriceModel
                    {
                        Id = currDish.Id,
                        Position = currDish.Position,
                        Name = currDish.Name
                    };
                    dishesList.Add(newDish);
                }

                dayRecordsList.Add(new RecordNameModel(d.Id, username, d.isReady, dishesList));
            }
            return dayRecordsList;
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
            return dataBase.UserRepository.GetAll()
                .Select(i => new UserModel(i))
                .Where(i => i.Email == email)
                .Where(i => HashPassword.VerifyUserPassword(password, i.Password) == true)
                .FirstOrDefault();
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
        public List<TransactionModel> GetUserTransactions(int User)
        {
            List<TransactionModel> userTransactions = dataBase.TransactionRepository.GetAll()
                                                                                    .Select(i => new TransactionModel(i))
                                                                                    .Where(i => i.User == User)
                                                                                    .OrderByDescending(i => i.Id)
                                                                                    .ToList();
            User user = dataBase.UserRepository.Get(User);
            foreach (TransactionModel t in userTransactions)
            {
                t.Username = user.Name;
            }
            return userTransactions;
        }

        public decimal GetUserBalance(int userId)
        {
            User user = dataBase.UserRepository.GetAll().Where(i => i.Id.Equals(userId)).FirstOrDefault();
            if(user != null)
            {
                List<TransactionModel> userTransactions = GetUserTransactions(userId);
                decimal balance = 0;
                foreach (TransactionModel tr in userTransactions)
                {
                    balance += tr.Price;
                }
                return balance;
            }
            return 0;
        }

        public void SwitchMenuStatus(DateTime date)
        {
            Menu dinnerMenu = dataBase.MenuRepository.GetAll().Where(i => i.Date == date).FirstOrDefault();
            List<Record> records = dataBase.RecordRepository.GetAll().Where(i => i.Date == date).ToList();

            if (dinnerMenu != default(Menu))
            {
                if (dinnerMenu.IsActive == 0)
                    dinnerMenu.IsActive = 1;
                else {
                    dinnerMenu.IsActive = 0;
                    foreach (Record record in records)
                    {
                        dataBase.RecordRepository.Delete(record.Id);
                    }
                } 
                dataBase.MenuRepository.Update(dinnerMenu);
                Save();
            }
        }
        
        public void DeleteDinnnerMenu(DateTime date)
        {
            Menu dinnerMenu = dataBase.MenuRepository.GetAll().Where(i => i.Date == date).FirstOrDefault();
            if (dinnerMenu != null)
            {
                List<MenuDish> dishMenus = dataBase.MenuDishRepository.GetAll()
                                        .Where(i => i.Menu == dinnerMenu.Id).ToList();
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
            if (dish != null)
            {
                MenuDish dishMenu = dataBase.MenuDishRepository.GetAll()
                                                                .Where(i => i.Dish == dish.Id)
                                                                .FirstOrDefault();
                if (dishMenu != null)
                {
                    dataBase.MenuDishRepository.Delete(dishMenu.Id);
                }
                List<RecordDish> recDishes = dataBase.RecordDishRepository.GetAll().Where(i => i.Dish == dish.Id).ToList();
                foreach (RecordDish recsDishes in recDishes)
                {
                    Record rec = dataBase.RecordRepository.Get(recsDishes.Record);
                    if (rec != null)
                    {
                        rec.Price -= dish.Price;
                        dataBase.RecordRepository.Update(rec);
                    }
                    if (dish.Position == 1)
                    {
                        recsDishes.Dish = 1;
                    }
                    if (dish.Position == 2)
                    {
                        recsDishes.Dish = 2;
                    }
                    dataBase.RecordDishRepository.Update(recsDishes);
                    Save();
                }
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
            if (dinnerMenu != null)
            {                            
                List<MenuDish> dishMenuItems = dataBase.MenuDishRepository.GetAll().Where(i => i.Menu == dinnerMenu.Id).ToList();
                if (dishMenuItems.Count != 0)
                {
                    foreach (MenuDish md in dishMenuItems)
                    {
                        dataBase.MenuDishRepository.Delete(md.Id);
                        Save();
                    }
                }
                foreach (int di in dishesList)
                {   
                    if (di != 1 && di != 2)
                    {
                        MenuDish dishMenuItem = new MenuDish 
                        {
                            Menu = dinnerMenu.Id, 
                            Dish = di
                        };   
                        dataBase.MenuDishRepository.Create(dishMenuItem);
                        Save();
                    }
                }
                
                List<Record> records = dataBase.RecordRepository.GetAll().Where(i => i.Date == date).ToList();
                List<MenuDish> dishMenus = dataBase.MenuDishRepository.GetAll().Where(i => i.Menu == dinnerMenu.Id).ToList();
                List<Dish> dishes = new List<Dish>();
                foreach (MenuDish di in dishMenus) 
                {
                    dishes.Add(dataBase.DishRepository.GetAll().Where(i => i.Id == di.Dish).FirstOrDefault());
                }
                if (records.Count != 0)
                {
                    foreach (Record rec in records)
                    {
                        rec.Price = 0;
                        dataBase.RecordRepository.Update(rec);
                        Save();
                        List<RecordDish> recDishes = dataBase.RecordDishRepository.GetAll().Where(i => i.Record == rec.Id).ToList();
                        if (recDishes.Count == 2)
                        {
                            foreach (RecordDish rd in recDishes)
                            {
                                Dish prevDish = dataBase.DishRepository.Get(rd.Dish);
                                if (prevDish != null)
                                {
                                    Dish newDish = dishes.Where(i => i.Position == prevDish.Position).FirstOrDefault();
                                    if (newDish != null)
                                    {
                                        rd.Dish = newDish.Id;
                                        dataBase.RecordDishRepository.Update(rd);
                                        Save();
                                    }
                                    else 
                                    {
                                        if (prevDish.Position == 1)
                                        {
                                            rd.Dish = 1;
                                        }
                                        if (prevDish.Position == 2)
                                        {
                                            rd.Dish = 2;
                                        }
                                        dataBase.RecordDishRepository.Update(rd);
                                        Save();
                                    }
                                }
                                Dish dishForPrice = dataBase.DishRepository.Get(rd.Dish);
                                rec.Price += dishForPrice.Price;
                                dataBase.RecordRepository.Update(rec);
                                Save();
                            }
                        }
                        if (recDishes.Count == 1)
                        {
                            Dish prevDish = dataBase.DishRepository.Get(recDishes[0].Dish);
                            if (prevDish != null)
                            {
                                Dish newDish = dishes.Where(i => i.Position == prevDish.Position).FirstOrDefault();
                                if (newDish != null)
                                {
                                    recDishes[0].Dish = newDish.Id;
                                    dataBase.RecordDishRepository.Update(recDishes[0]);
                                    Save();
                                }
                                else 
                                {
                                    if (prevDish.Position == 1)
                                    {
                                        recDishes[0].Dish = 1;
                                    }
                                    if (prevDish.Position == 2)
                                    {
                                        recDishes[0].Dish = 2;
                                    }
                                    dataBase.RecordDishRepository.Update(recDishes[0]);
                                    Save();   
                                }
                            }
                            Dish dishForPrice = dataBase.DishRepository.Get(recDishes[0].Dish);
                            rec.Price += dishForPrice.Price;
                            dataBase.RecordRepository.Update(rec);
                            Save();
                        }
                    }
                }
            }
        }

        public void DeleteUser(int userId) 
        {
            User user = dataBase.UserRepository.GetAll().Where(i => i.Id == userId).FirstOrDefault();
            if (user != null)
            {
                dataBase.UserRepository.Delete(user.Id);
                Save();
            }
        }

        public void ApproveUser(int userId)
        {
            User user = dataBase.UserRepository.GetAll().Where(i => i.Id.Equals(userId)).FirstOrDefault();
            if (user != null)
            {
                user.IsApproved = 1;
                dataBase.UserRepository.Update(user);
                Save();
            }
        }

        public void ChangeUserBalance(int adminId, int userId, decimal price, DateTime date)
        {
            User user = dataBase.UserRepository.GetAll().Where(i => i.Id.Equals(userId)).FirstOrDefault();
            if (user != null)
            {
                CreateTransaction(adminId, userId, price, date);
            }
        }
        public void UpdateRecordStatus(int id, sbyte status)
        {
            RecordModel recordmodel = dataBase.RecordRepository.GetAll()
                                .Select(i => new RecordModel(i))
                                .Where(i => i.Id == id).FirstOrDefault();

            Record record = new Record
            {
                Id = recordmodel.Id,
                UserId = recordmodel.UserId,
                Price = recordmodel.Price,
                Date = recordmodel.Date,
                IsReady = status
            };
            dataBase.RecordRepository.Update(record);
            Save();
        }
        public Tuple<List<MenuModel>, List<MenuModel>> GetPeriodMenu()
        {
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            List<MenuModel> current = new List<MenuModel>();
            List<MenuModel> next = new List<MenuModel>();
            List<DinnerMenuModel> dinnerMenus = dataBase.MenuRepository.GetAll()
                                                .Select(i => new DinnerMenuModel(i))
                                                .Where(i => i.Date >= date).Take(62).ToList();
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

        public Tuple<List<RecordPosModel>, List<RecordPosModel>> GetPeriodRecord(int id)
        {
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            List<RecordPosModel> current = new List<RecordPosModel>();
            List<RecordPosModel> next = new List<RecordPosModel>();

            List<RecordModel> allUserRecords = dataBase.RecordRepository.GetAll()
                                                .Select(i => new RecordModel(i))
                                                .Where(i => i.UserId == id && i.Date >= date).Take(62).ToList();  
            allUserRecords = (allUserRecords == null) ? new List<RecordModel>() : allUserRecords;

            List<RecordPosModel> cycle (List<RecordPosModel> recordPosList)
            {
                for (DateTime counter = date; counter.Month == date.Month; counter = counter.AddDays(1))
                {
                    RecordModel record = allUserRecords.Where(i => i.Date == counter).FirstOrDefault();
  //                  int positionCounter = allUserRecords.Where(i => i.Date == counter).Count();
                    int positionCounter = 0;
                    if (record != null)
                        positionCounter = dataBase.RecordDishRepository.GetAll().Where(i => i.Record == record.Id).Count();
                    
                    if (positionCounter == 0)
                    {
                        recordPosList.Add(new RecordPosModel(counter, 4));  //не ест
                    }
                    if (positionCounter == 2)
                    {
                        recordPosList.Add(new RecordPosModel(counter, 3));  //комплекс
                    }
                    if (positionCounter == 1)
                    {
                       // RecordModel currDayRecords = allUserRecords.Where(i => i.Date == counter).FirstOrDefault();
                        RecordDishModel currRecordDish = dataBase.RecordDishRepository.GetAll()
                                                .Select(i => new RecordDishModel(i))
                                                .Where(i => i.Record == record.Id).FirstOrDefault();
                        DishModel currDish = dataBase.DishRepository.GetAll()
                                                .Select(i => new DishModel(i))
                                                .Where(i => i.Id == currRecordDish.Dish).FirstOrDefault();
                        if (currDish.Position == 1)
                        {
                            recordPosList.Add(new RecordPosModel(counter, 1));  //первое
                        }
                        else
                        {
                            recordPosList.Add(new RecordPosModel(counter, 2));  //второе
                        }
                    }
                }
                return recordPosList;
            }
            current = cycle(current);
            date = date.AddMonths(1);
            next = cycle(next);
            return Tuple.Create(current, next);
        }

         public void CreateOrUpdateRecord(DateTime date, int userId, int position)
        {
            TimeSpan timePeriod = date - DateTime.Now;
            if (timePeriod < TimeSpan.FromHours(12))
            {
                return;
            }
            Record record = dataBase.RecordRepository.GetAll()
                                                     .Where(i => i.Date == date && i.UserId == userId)
                                                     .FirstOrDefault();
            if (record != default(Record))
            {
                List<RecordDish> recDishes = dataBase.RecordDishRepository.GetAll().Where(i => i.Record == record.Id).ToList();
                if (recDishes.Count() > 1 && position == 3)
                    return;
                else if (recDishes.Count() == 1)
                {
                    Dish dish = dataBase.DishRepository.Get(recDishes[0].Dish);
                    if (dish.Position == position)
                        return;
                }
            }
            else if (position == 4)
                return;
            //если запись на дату уже сделана и стоит позиция "не ест", то удаляем запись
            if (position == 4 && record != default(Record))
            {
                dataBase.RecordRepository.Delete(record.Id);
                return;
            }

            void createRecordDish (int dishId, int recordId)
            {
                RecordDish recordDishItems = new RecordDish
                {
                    Record = recordId,
                    Dish = dishId
                };
                dataBase.RecordDishRepository.Create(recordDishItems);
                Save();

                Record recordItems = dataBase.RecordRepository.Get(recordId);
                recordItems.Price = recordItems.Price + GetDish(dishId).Price;
                dataBase.RecordRepository.Update(recordItems);
                Save();
            }
            void createRecordsOnPos (int recordId, int dishFirst, int dishSecond)
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
            //запись на еще не установленное блюдо создается на блюдо с id 1 или 2
            int dishFirstId = 1; 
            int DishSecondId = 2;
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
            if (record != default(Record))
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
                    }
                    record.Price = 0;
                    dataBase.RecordRepository.Update(record);
                    Save();
                }

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
        public void CreateUser(SignUpModel upm)
        {
            User user = new User
            {
                Role = "user",
                Name = upm.UserName,
                Password = HashPassword.HashUserPassword(upm.Password),
                Email = upm.Email,
                IsApproved = 0,
                RefreshToken = null
            };
            dataBase.UserRepository.Create(user);
            Save();
        }
        public void CreateTransaction(int admin, int user, decimal price, DateTime date) {
            Transaction transaction = new Transaction
            {
                User = user,
                Admin = admin,
                Price = price,
                Date = date
            };
            dataBase.TransactionRepository.Create(transaction);
            Save();
        }
        public void CreateTransaction(int user, decimal price, DateTime date) {
            Transaction transaction = new Transaction
            {
                User = user,
                Admin = null,
                Price = price,
                Date = date
            };
            dataBase.TransactionRepository.Create(transaction);
            Save();
        }
        public bool Save()
        {
            dataBase.Save();
            return true;
        }

         public bool CheckUserByEmail(string email) 
        {
            UserModel user = dataBase.UserRepository.GetAll()
                                                    .Select(i => new UserModel(i))
                                                    .Where(i => i.Email == email)
                                                    .FirstOrDefault();
            if (user == null)
                return false;
            else return true;
        }
        public async Task ResetPasswordOfUser(string email) 
        {
            User user = dataBase.UserRepository.GetAll()
                                                .Where(i => i.Email == email)
                                                .FirstOrDefault();
            Random rd = new Random();
            int length = rd.Next(6,30);

            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int index = rd.Next(chars.Length);
                sb.Append(chars[index]);
            }
            user.Password = HashPassword.HashUserPassword(sb.ToString());
            dataBase.UserRepository.Update(user);
            Save();

            string jsonFromFile;
            using (var reader = new StreamReader("./SenderCredentials.json"))
            {
                jsonFromFile = reader.ReadToEnd();
            }
            var credentialsFromJson = JsonConvert.DeserializeObject<CredentialsModel>(jsonFromFile);

            var emailMessage = new MimeMessage(); 
            emailMessage.From.Add(new MailboxAddress("Involta.Обеды", credentialsFromJson.Email));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Новый пароль";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "Ваш пароль был сброшен. Ваш новый пароль - " + sb.ToString()
            };
             
            using (var client = new SmtpClient())
            {
                if (credentialsFromJson.Email.Contains("@yandex.ru")) {
                    await client.ConnectAsync("smtp.yandex.ru", 25, false); 
                }
                else if (credentialsFromJson.Email.Contains("@gmail.com")) {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                }
                else if (credentialsFromJson.Email.Contains("@mail.ru")) {
                    await client.ConnectAsync("smtp.mail.ru", 25, false);
                }
                await client.AuthenticateAsync(credentialsFromJson.Email, credentialsFromJson.Password);
                await client.SendAsync(emailMessage);
 
                await client.DisconnectAsync(true);
            }
        }

        public bool ChangePasswordOfUser(ChangePasswordModel changePasswordModel) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.ReadJwtToken(changePasswordModel.token);
            int id = Int32.Parse(jwt.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value);
            User user = dataBase.UserRepository.GetAll().Where(i => i.Id == id).FirstOrDefault();
            if (HashPassword.VerifyUserPassword(changePasswordModel.OldPassword, user.Password))
            {
                user.Password = HashPassword.HashUserPassword(changePasswordModel.NewPassword);
                dataBase.UserRepository.Update(user);
                Save();
                return true;
            }
            else return false;
        }
        public void UpdateMonthMenu()
        {
            sbyte status = 0;
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            date = date.AddMonths(-1);
            bool isAnyPrevious = dataBase.MenuRepository.GetAll()
                                .Select (i => new DinnerMenuModel(i))
                                .Where (i => i.Date.Month == date.Month).Any();
            bool isAnyPrevRecords = dataBase.RecordRepository.GetAll()
                                .Select (i => new RecordModel(i))
                                .Where (i => i.Date.Month == date.Month).Any();
            //если есть Menu за прошлый месяц, то удаляем их
            if (isAnyPrevious)
            {
                for (DateTime counter = date; counter.Month == date.Month; counter = counter.AddDays(1))
                {
                    DinnerMenuModel currDay = dataBase.MenuRepository.GetAll()
                                            .Select (i => new DinnerMenuModel(i))
                                            .Where (i => i.Date == counter).FirstOrDefault();
                    dataBase.MenuRepository.Delete(currDay.Id);
                }
                Save();
            }
            //если есть записи Record и RecordDish за прошлый месяц, то удаляем их
            if (isAnyPrevRecords)
            {
                List<RecordModel> prevRecords = dataBase.RecordRepository.GetAll()
                                                .Select (i => new RecordModel(i))
                                                .Where (i => i.Date.Month == date.Month).ToList();
                foreach (RecordModel r in prevRecords)
                {
                    List<RecordDishModel> prevRecordDishes = dataBase.RecordDishRepository.GetAll()
                                                            .Select (i => new RecordDishModel(i))
                                                            .Where (i => i.Record == r.Id).ToList();
                    foreach (RecordDishModel rd in prevRecordDishes)
                    {
                        dataBase.RecordDishRepository.Delete(rd.Id);
                    }
                    dataBase.RecordRepository.Delete(r.Id);
                }
                Save();
            }
            //создаем Menu на следующий месяц
            date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            date = date.AddMonths(1);
            for (DateTime counter = date; counter.Month == date.Month; counter = counter.AddDays(1))
            {
                if ((int)counter.DayOfWeek == 0 || (int)counter.DayOfWeek == 6) //если суббота или воскресенье
                {
                    status = 0;
                }
                else status = 1;
                Menu currDay = new Menu
                {
                    Date = counter,
                    IsActive = status
                };
                dataBase.MenuRepository.Create(currDay);
            }
            Save();
        }
        public void UpdateUser(UserModel um)
        {
            var user = dataBase.UserRepository.Get(um.Id);
            user.RefreshToken = um.RefreshToken;
            dataBase.UserRepository.Update(user);
            Save();
		}
        public void ChangeUserRole(UserModel um)
        {
            var user = dataBase.UserRepository.Get(um.Id);
            user.Role = um.Role;
            dataBase.UserRepository.Update(user);
            Save();
        }
        public sbyte GetUserStatus(int id)
        {
            User user = dataBase.UserRepository.Get(id);
            if (user != null)
            {
                return user.IsApproved;
            }
            else return 0;
        }

        public void UpdateDish(UpdateDishModel updateDish)
        {
            Dish currDish = new Dish();
            if (updateDish.Id != null && updateDish.Id != 0)
            {
                currDish = dataBase.DishRepository.Get(updateDish.Id);
            }
            if (currDish != null)
            {
                List<RecordDish> recDishes = dataBase.RecordDishRepository.GetAll().Where(i => i.Dish == currDish.Id).ToList();
                foreach (RecordDish recs in recDishes)
                {
                    Record rec = dataBase.RecordRepository.GetAll().Where(i => i.Id == recs.Record).FirstOrDefault();
                    rec.Price -= currDish.Price;
                    rec.Price += updateDish.Price;
                    dataBase.RecordRepository.Update(rec);
                    Save();
                }
                currDish.Name = updateDish.Name;
                currDish.Price = updateDish.Price;
                dataBase.DishRepository.Update(currDish);
                Save();
            }
        }
    }
}
