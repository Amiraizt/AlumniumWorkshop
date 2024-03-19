using Alumnium.Core.DbContext;
using Alumnium.Core;
using Microsoft.AspNetCore.Identity;
using AlumniumWorkshop.Models.Item;
using System.Reflection;
using AlumniumWorkshop.Models.SiteRequest;
using Microsoft.EntityFrameworkCore;
using AlumniumWorkshop.Models.AlmniumType;

namespace AlumniumWorkshop.Areas.Helpers
{
    public class CoreServices
    {
        IConfiguration _configuration;
        ExceptionHandler EXH;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        ApplicationDBContext _db;
        public CoreServices(ApplicationDBContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            EXH = new ExceptionHandler(db);
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }
        public CoreServices(ApplicationDBContext db, IConfiguration configuration)
        {
            _db = db;
            EXH = new ExceptionHandler(db);
            _configuration = configuration;
        }

        #region Item
        public (bool Result, List<ItemModel> model) GetItemsList()
        {
            try
            {
                var items = _db.Items.Where(a => a.Status != Consts.DELETED).Select(a => new ItemModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Quantity = a.Quantity,
                    UnitPrice = a.Price
                }).ToList();
                return (true, items);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, ItemModel item) GetItemById(int Id)
        {
            try
            {
                var getItem = _db.Items.FirstOrDefault(a => a.Id == Id);
                var model = new ItemModel()
                {
                    Id = getItem.Id,
                    Name = getItem.Name,
                    Quantity = getItem.Quantity,
                    UnitPrice = getItem.Price
                };
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public bool DeleteItem(int id)
        {
            try
            {
                var item = _db.Items.FirstOrDefault(a => a.Id == id);
                item.Status = Consts.DELETED;
                _db.Items.Update(item);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool CreateItem(ItemModel model)
        {
            try
            {
                var item = new Item()
                {
                    Name = model.Name,
                    Quantity = model.Quantity,
                    Price = model.UnitPrice
                };
                _db.Items.Add(item);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool UpdateItem(ItemModel model)
        {
            try
            {
                var getItem = _db.Items.FirstOrDefault(a => a.Id == model.Id);
                getItem.Name = model.Name;
                getItem.Quantity = model.Quantity;
                getItem.Price = model.UnitPrice;

                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool ReduceItemQuantity(int alumuniumTypeId, decimal metres)
        {
            try
            {
                var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == alumuniumTypeId);
                var item = _db.Items.FirstOrDefault(a => a.Id == type.UsedItemId);
                var qtyRequired = type.Quantity * metres;
                item.Quantity = item.Quantity - qtyRequired;
                _db.Items.Update(item);
                _db.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool ChangeItemStatus(int id, bool status)
        {
            try
            {
                var getItem = _db.Items.FirstOrDefault(a => a.Id == id);
                if (!status)
                    getItem.Status = Consts.NOTACTIVE;
                else
                    getItem.Status = Consts.ACTIVE;
                _db.Items.Update(getItem);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        #endregion

        #region Site
        public (bool Result, SiteRequestModel model) GetSiteRequestById(int id)
        {
            try
            {
                var getSite = _db.SiteRequests.Include(a => a.AlumniumType).FirstOrDefault(a => a.Id == id);
                var site = new SiteRequestModel()
                {
                    Id = getSite.Id,
                    AluminumTypeId = getSite.AlmuniumTypeId,
                    AluminumTypeName = getSite.AlumniumType.TypeName,
                    MetersNumber = getSite.MetersNumber,
                    SiteName = getSite.SiteName,
                    SiteOwnerName = getSite.SiteOwnerName,
                    SiteOwnerPhone = getSite.SiteOwnerPhone,
                    TotalPrice = getSite.TotalPrice
                };
                return (true, site);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, List<SiteRequestModel> model) GetSiteRequestsList()
        {
            try
            {
                var sites = _db.SiteRequests.Where(a => a.Status != Consts.DELETED).Select(a => new SiteRequestModel
                {
                    Id = a.Id,
                    AluminumTypeName = a.AlumniumType.TypeName,
                    MetersNumber = a.MetersNumber,
                    SiteName = a.SiteName,
                    SiteOwnerName = a.SiteOwnerName,
                    SiteOwnerPhone = a.SiteOwnerPhone,
                    TotalPrice = a.TotalPrice
                }).ToList();


                return (true, sites);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public bool CreateSiteRequest(SiteRequestModel model)
        {
            try
            {
                var request = new SiteRequest()
                {
                    SiteName = model.SiteName,
                    SiteOwnerName = model.SiteOwnerName,
                    CreatedOn = DateTime.Now,
                    MetersNumber = model.MetersNumber,
                    AlmuniumTypeId = model.AluminumTypeId,
                    SiteOwnerPhone = model.SiteOwnerPhone,
                    Status = Consts.ACTIVE,
                    TotalPrice = model.TotalPrice
                };
                var reduceResult = ReduceItemQuantity(request.AlmuniumTypeId, request.MetersNumber);
                if (!reduceResult)
                    return false;
                _db.SiteRequests.Add(request);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool UpdateSiteRequest(SiteRequestModel model)
        {
            try
            {
                var getSite = _db.SiteRequests.Include(a => a.AlumniumType).ThenInclude(a => a.Item).FirstOrDefault(a => a.Id == model.Id);
                getSite.MetersNumber = model.MetersNumber;
                getSite.SiteOwnerPhone = model.SiteOwnerPhone;
                getSite.SiteOwnerName = model.SiteOwnerName;
                getSite.AlmuniumTypeId = model.AluminumTypeId;
                getSite.SiteName = model.SiteName;
                getSite.TotalPrice = CalculateSiteRequestTotal(model.AluminumTypeId, model.MetersNumber);
                _db.SiteRequests.Update(getSite);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool DeleteSiteRequest(int id)
        {
            try
            {
                var getSite = _db.SiteRequests.FirstOrDefault(a => a.Id == id);
                getSite.Status = Consts.DELETED;
                _db.SiteRequests.Update(getSite);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public decimal CalculateSiteRequestTotal(int almuniumTypeId, decimal meters)
        {
            try
            {
                var alumnium = _db.AlumniumTypes.Include(a => a.Item).FirstOrDefault(a => a.Id == almuniumTypeId);
                var qtyRequired = alumnium.Quantity * meters;
                var price = qtyRequired * alumnium.Item.Price;
                return price;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return 0;
            }
        }
        #endregion

        #region AlmuniumType
        public (bool Result, List<AlmuniumTypeModel> model) GetTypesList()
        {
            try
            {
                var types = _db.AlumniumTypes.Include(a=>a.Item.Name).Where(a => a.Status != Consts.DELETED).Select(a => new AlmuniumTypeModel
                {
                    Id = a.Id,
                    Name = a.TypeName,
                    Quantity = a.Quantity,
                    UsedItemId = a.UsedItemId,
                    UsedItemName = a.Item.Name
                }).ToList();


                return (true, types);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, AlmuniumTypeModel model) GetTypeById(int id)
        {
            try
            {
                var type = _db.AlumniumTypes.Include(a=>a.Item).FirstOrDefault(a => a.Id == id);
                var typeModel = new AlmuniumTypeModel()
                {
                    Id = type.Id,
                    Name = type.TypeName,
                    Quantity = type.Quantity,
                    UsedItemId = type.UsedItemId,
                    UsedItemName = type.Item.Name
                };

                return (true, typeModel);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public bool CreateType(AlmuniumTypeModel model)
        {
            try
            {
                var type = new AlumniumType()
                {
                    UsedItemId = model.UsedItemId,
                    Quantity = model.Quantity,
                    Status = Consts.ACTIVE,
                    TypeName = model.Name
                };
                _db.AlumniumTypes.Add(type);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool DeleteType(int id)
        {
            try
            {
                var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == id);
                type.Status = Consts.DELETED;
                _db.AlumniumTypes.Update(type);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool UpdateType(AlmuniumTypeModel model)
        {
            try
            {
                var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == model.Id);
                type.UsedItemId = model.UsedItemId;
                type.Quantity = model.Quantity;
                type.TypeName = model.Name;
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool ChangeTypeStatus(int id, bool status)
        {
            try
            {
                var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == id);
                if (!status)
                    type.Status = Consts.NOTACTIVE;
                else
                    type.Status = Consts.ACTIVE;
                _db.AlumniumTypes.Update(type);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        #endregion
    }
}
