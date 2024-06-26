﻿using Alumnium.Core.DbContext;
using Alumnium.Core;
using Microsoft.AspNetCore.Identity;
using AlumniumWorkshop.Models.Item;
using System.Reflection;
using AlumniumWorkshop.Models.SiteRequest;
using Microsoft.EntityFrameworkCore;
using AlumniumWorkshop.Models.AlmniumType;
using Microsoft.Build.Execution;
using AlumniumWorkshop.Models.Reports;
using static iTextSharp.text.pdf.AcroFields;

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
        public CoreServices(ApplicationDBContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            EXH = new ExceptionHandler(db);
            _configuration = configuration;
            _userManager = userManager;
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
                    UnitPrice = a.Price,
                    Status = a.Status,
                    CreationDate = a.CreationDate.Date.ToShortDateString(),
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
                var item = new Alumnium.Core.Item()
                {
                    Name = model.Name,
                    Quantity = model.Quantity,
                    Price = model.UnitPrice,
                    CreationDate = DateTime.Now,
                    Status = Consts.ACTIVE
                };
                _db.Items.Add(item);
                _db.SaveChanges();

                var itemLog = new ItemLog()
                {
                    ItemId = item.Id,
                    Quantity = item.Quantity,
                    AdditionDate = DateTime.Now
                };
                _db.ItemLogs.Add(itemLog);
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
                getItem.CreationDate = DateTime.Now;
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
        public (bool Result, string response) ReduceItemQuantity(int itemId, int qty, decimal metres)
        {
            try
            {
                var item = _db.Items.FirstOrDefault(a => a.Id == itemId);
                var mtrs = (int)metres;
                //var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == alumuniumTypeId);

                if (item.Quantity > qty)
                {
                    item.Quantity = item.Quantity - (qty * metres);
                    _db.Items.Update(item);
                    _db.SaveChanges();
                }
                else
                    return (false, "كمية المواد غير كافية!");


                //item.Quantity = item.Quantity - qtyRequired;
                _db.Items.Update(item);
                _db.SaveChanges();
                return (true, "");

            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, "");
            }
        }

        public (bool Result, string response) ReduceItemQuantity(UseItemModel model)
        {
            try
            {
                var item = _db.Items.FirstOrDefault(a => a.Id == model.ItemId);


                if (item.Quantity > model.Quantity)
                {
                    item.Quantity = item.Quantity - model.Quantity;
                    _db.Items.Update(item);
                    _db.SaveChanges();

                    var usedItem = new SiteUsedItems()
                    {
                        ItemId = item.Id,
                        SiteId = model.SiteId,
                        UsedDate = DateTime.Now,
                        UsedQuantity = model.Quantity
                    };
                    _db.SiteUsedItems.Add(usedItem);
                    _db.SaveChanges();
                }
                else
                    return (false, "كمية المواد غير كافية في المخزن!");

                return (true, "");

            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, "");
            }
        }

        public bool ChangeItemStatus(int id)
        {
            try
            {
                var getItem = _db.Items.FirstOrDefault(a => a.Id == id);
                if (getItem.Status == Consts.NOTACTIVE)
                    getItem.Status = Consts.ACTIVE;
                else
                    getItem.Status = Consts.NOTACTIVE;
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
        public bool EditItemWarehouse(NewItemQuantityModel model)
        {
            try
            {
                var getItem = _db.Items.FirstOrDefault(a => a.Id == model.ItemId);
                getItem.Quantity = getItem.Quantity + model.NewQuantity;
                getItem.CreationDate = DateTime.Now;
                _db.Items.Update(getItem);
                _db.SaveChanges();

                var itemLog = new ItemLog()
                {
                    ItemId = model.ItemId,
                    Quantity = model.NewQuantity,
                    AdditionDate = DateTime.Now
                };
                _db.ItemLogs.Add(itemLog);
                _db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }

        }
        public (bool Result, RootSiteUsedItemModel model) GetSiteItemsList(int siteId)
        {
            try
            {
                var siteAluminums = _db.SiteUsedAlumimums.Where(a => a.SiteRequestId == siteId).ToList();
                var items = new List<AlmuniumUsedItems>();
                foreach (var almnium in siteAluminums)
                {
                    items = _db.AluminumUsedItems.Where(a => a.AluminumTypeId == almnium.AlmuniumTypeId).ToList();
                    items.AddRange(items);
                    items = items.ToList();
                }
                var itemsList = items.GroupBy(a => a.ItemId);
                var siteItems = itemsList.Select(a => new UseItemModel
                {
                    ItemId = a.Key,
                    ItemName = _db.Items.FirstOrDefault(c => c.Id == a.Key)?.Name

                }).ToList();
                var model = new RootSiteUsedItemModel
                {
                    SiteId = siteId,
                    Items = siteItems
                };
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        #endregion

        #region Site
        public (bool Result, SiteRequestModel model) GetSiteRequestById(int id)
        {
            try
            {
                var getSite = _db.SiteRequests.FirstOrDefault(a => a.Id == id);
                var usedAluminum = _db.SiteUsedAlumimums.Where(a => a.SiteRequestId == getSite.Id).ToList();
                var site = new SiteRequestModel()
                {
                    Id = getSite.Id,
                    MetersNumber = getSite.MetersNumber,
                    SiteName = getSite.SiteName,
                    SiteOwnerName = getSite.SiteOwnerName,
                    SiteOwnerPhone = getSite.SiteOwnerPhone,
                    TotalPrice = getSite.TotalPrice,
                    UsedAluminumList = usedAluminum.Select(a => new UsedAluminumModel
                    {
                        AluminumTypeId = a.AlmuniumTypeId,
                        AluminumTypeName = a.AlumniumType.TypeName
                    }).ToList()
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
        public (bool Result, CreateSiteRequestModel model) GetModelForSiteRequest()
        {
            try
            {
                var aluminums = _db.AlumniumTypes.Where(a => a.Status == Consts.ACTIVE).ToList();
                var request = new CreateSiteRequestModel()
                {
                    Aluminums = aluminums.Select(a => new UsedAluminumModel
                    {
                        AluminumTypeId = a.Id,
                        AluminumTypeName = a.TypeName,
                        IsSelected = false
                    }).ToList()
                };
                return (true, request);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, EditSiteRequestModel model) GetEditSiteRequestModel(int id)
        {
            try
            {
                var getSite = _db.SiteRequests.FirstOrDefault(a => a.Id == id);
                var usedAluminum = _db.SiteUsedAlumimums.Include(a => a.AlumniumType).Where(a => a.SiteRequestId == getSite.Id).ToList();
                var site = new EditSiteRequestModel()
                {
                    Id = getSite.Id,
                    MetersNumber = getSite.MetersNumber,
                    SiteName = getSite.SiteName,
                    SiteOwnerName = getSite.SiteOwnerName,
                    SiteOwnerPhone = getSite.SiteOwnerPhone,
                    WindowsNumber = getSite.WindowsNumber,
                    DoorsNumber = getSite.DoorsNumber,
                    TotalPrice = getSite.TotalPrice,
                    UsedAluminumList = usedAluminum.Select(a => new UsedAluminumModel
                    {
                        AluminumTypeId = a.AlmuniumTypeId,
                        AluminumTypeName = a.AlumniumType.TypeName
                    }).ToList()
                };
                return (true, site);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, string response) CreateSiteRequest(CreateSiteRequestModel model)
        {
            try
            {
                var request = new SiteRequest()
                {
                    SiteName = model.SiteName,
                    SiteOwnerName = model.SiteOwnerName,
                    CreatedOn = DateTime.Now,
                    MetersNumber = model.MetersNumber,
                    WindowsNumber = model.WindowsNumber,
                    DoorsNumber = model.DoorsNumber,
                    SiteOwnerPhone = model.SiteOwnerPhone,
                    Status = Consts.ACTIVE,
                    TotalPrice = CalculateSiteRequestTotal(model.Aluminums.ToList(), model.MetersNumber),

                };
                _db.SiteRequests.Add(request);
                _db.SaveChanges();

                foreach (var usedTpe in model.Aluminums)
                {

                    var almunium = _db.AluminumUsedItems.Include(a => a.Item).FirstOrDefault(a => a.AluminumTypeId == usedTpe.AluminumTypeId);
                    var usedItemQty = almunium.ItemQuantity * model.MetersNumber;
                    if (usedItemQty >= almunium.Item.Quantity)
                    {
                        var usedAlmniums = _db.SiteUsedAlumimums.Where(a => a.SiteRequestId == request.Id).ToList();
                        if (usedAlmniums.Count() != 0)
                        {
                            foreach (var al in usedAlmniums)
                            {
                                _db.SiteUsedAlumimums.Remove(al);
                                _db.SaveChanges();
                            }
                        }
                        return (false, "كمية المواد غير كافية, الرجاء تجديد المخزن");
                    }

                    var type = new SiteUsedAlumimum()
                    {
                        SiteRequestId = request.Id,
                        AlmuniumTypeId = usedTpe.AluminumTypeId
                    };
                    _db.SiteUsedAlumimums.Add(type);
                    _db.SaveChanges();


                    //var usedItems = _db.AluminumUsedItems.Where(a => a.AluminumTypeId == usedTpe.AluminumTypeId).ToList();
                    //foreach (var item in usedItems)
                    //{
                    //    var reduceResult = ReduceItemQuantity(item.ItemId, item.ItemQuantity, request.MetersNumber);
                    //    if (!reduceResult.Result)
                    //    {
                    //        _db.SiteUsedAlumimums.Remove(type);
                    //        _db.SaveChanges();
                    //        _db.SiteRequests.Remove(request);
                    //        _db.SaveChanges();

                    //        return (false, reduceResult.response);

                    //    }

                    //}
                }
                var result = CalculateSiteRequestTotal(model.Aluminums.ToList(), model.MetersNumber);



                return (true, "");
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, "error");
            }
        }
        public bool UpdateSiteRequest(EditSiteRequestModel model)
        {
            try
            {
                var getSite = _db.SiteRequests.FirstOrDefault(a => a.Id == model.Id);
                //getSite.MetersNumber = model.MetersNumber;
                getSite.SiteOwnerPhone = model.SiteOwnerPhone;
                getSite.SiteOwnerName = model.SiteOwnerName;
                getSite.SiteName = model.SiteName;
                getSite.WindowsNumber = model.WindowsNumber;
                getSite.DoorsNumber = model.DoorsNumber;

                //getSite.TotalPrice = CalculateSiteRequestTotal(model.UsedAluminumList, model.MetersNumber);
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
        public decimal CalculateSiteRequestTotal(List<UsedAluminumModel> usedAluminums, decimal meters)
        {
            try
            {
                decimal totalPrice = 0;

                decimal AluminumPrice = 0;
                foreach (var aluminum in usedAluminums)
                {
                    decimal itemsPrice = 0;
                    var types = _db.AluminumUsedItems.Include(a => a.Item).Where(a => a.AluminumTypeId == aluminum.AluminumTypeId).ToList();
                    foreach (var item in types)
                    {
                        itemsPrice += item.Item.Price * item.ItemQuantity * meters;
                    }
                    totalPrice += itemsPrice;
                }

                return totalPrice;
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
                var types = _db.AlumniumTypes.Where(a => a.Status != Consts.DELETED).Select(a => new AlmuniumTypeModel
                {
                    Id = a.Id,
                    Name = a.TypeName,
                    Quantity = a.Quantity,
                    Status = a.Status
                }).ToList();


                return (true, types);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        //public (bool Result, AlmuniumTypeModel model) GetTypeById(int id)
        //{
        //    try
        //    {
        //        var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == id);
        //        var usedItems = _db.AluminumUsedItems.Include(a=>a.Item).Where(a => a.AluminumTypeId == type.Id).ToList();
        //        var typeModel = new AlmuniumTypeModel()
        //        {
        //            Id = type.Id,
        //            Name = type.TypeName,
        //            Quantity = type.Quantity,
        //            Items = usedItems.Select(a => new UsedItemsModel
        //            {
        //                Id = a.ItemId,
        //                Name = a.Item.Name,
        //                Quantity = 
        //            }).ToList()
        //        };

        //        return (true, typeModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
        //        return (false, new());
        //    }
        //}

        public (bool Result, CreateAluminumTypeModel model) GetModelForCreateType()
        {
            try
            {
                var usedItems = _db.Items.Where(a => a.Status == Consts.ACTIVE).ToList();

                var typeModel = new CreateAluminumTypeModel()
                {

                    Items = usedItems.Select(a => new UsedItemsModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        IsSelected = false
                    }).ToList()
                };

                return (true, typeModel);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool Result, EditAluminumTypeModel model) GetModelForEditType(int id)
        {
            try
            {
                var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == id);
                var usedItems = _db.AluminumUsedItems.Include(a => a.Item).Where(a => a.AluminumTypeId == id).ToList();
                var items = _db.Items.Where(a => a.Status != Consts.DELETED).ToList();
                var typeModel = new EditAluminumTypeModel()
                {
                    Id = type.Id,
                    Name = type.TypeName,
                    Quantity = (int)type.Quantity,
                    Items = items.Select(a =>
                    {
                        var usedItem = _db.AluminumUsedItems.FirstOrDefault(c => c.ItemId == a.Id && c.AluminumTypeId == type.Id);
                        return new UsedItemsModel
                        {
                            Id = a.Id,
                            Name = a.Name,
                            IsSelected = usedItem != null ? true : false,
                            Quantity = usedItem != null ? usedItem.ItemQuantity : 0
                        };
                    }).ToList()
                };

                return (true, typeModel);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public bool CreateType(CreateAluminumTypeModel model)
        {
            try
            {
                var type = new AlumniumType()
                {
                    Quantity = Convert.ToDecimal(model.Quantity),
                    Status = Consts.ACTIVE,
                    TypeName = model.Name
                };
                _db.AlumniumTypes.Add(type);
                _db.SaveChanges();

                foreach (var item in model.Items)
                {
                    if (item.IsSelected)
                    {
                        var itm = new AlmuniumUsedItems()
                        {
                            ItemId = item.Id,
                            AluminumTypeId = type.Id,
                            ItemQuantity = item.Quantity
                        };
                        _db.AluminumUsedItems.Add(itm);
                        _db.SaveChanges();
                    }


                }
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
        public bool UpdateType(EditAluminumTypeModel model)
        {
            try
            {
                var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == model.Id);
                //type.UsedItemId = model.UsedItemId;
                type.Quantity = model.Quantity;
                type.TypeName = model.Name;
                _db.AlumniumTypes.Update(type);
                _db.SaveChanges();

                foreach (var item in model.Items)
                {
                    if (item.IsSelected)
                    {
                        var usedItem = _db.AluminumUsedItems.FirstOrDefault(a => a.ItemId == item.Id && a.AluminumTypeId == type.Id);
                        if (usedItem != null)
                        {
                            usedItem.ItemQuantity = item.Quantity;
                            _db.AluminumUsedItems.Update(usedItem);
                            _db.SaveChanges();
                        }
                        else
                        {
                            var newItm = new AlmuniumUsedItems()
                            {
                                ItemId = item.Id,
                                AluminumTypeId = type.Id,
                                ItemQuantity = item.Quantity
                            };
                            _db.AluminumUsedItems.Add(newItm);
                            _db.SaveChanges();
                        }

                    }
                    else
                    {
                        var usedItem = _db.AluminumUsedItems.FirstOrDefault(a => a.ItemId == item.Id && a.AluminumTypeId == type.Id);
                        if (usedItem != null)
                        {
                            _db.AluminumUsedItems.Remove(usedItem);
                            _db.SaveChanges();
                        }
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return false;
            }
        }
        public bool ChangeTypeStatus(int id)
        {
            try
            {
                var type = _db.AlumniumTypes.FirstOrDefault(a => a.Id == id);
                if (type.Status == Consts.ACTIVE)
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

        #region Reports

        public List<ConsumedAluminumModel> GetConsumedAluminumReport(DateTime startDate, DateTime endDate)
        {
            var usedAluminum = _db.AlumniumTypes.ToList();
            List<ConsumedAluminumModel> aluminumList = new List<ConsumedAluminumModel>();

            foreach (var alm in usedAluminum)
            {
                var aluminumUnits = _db.SiteUsedAlumimums.Where(a => a.AlmuniumTypeId == alm.Id && a.SiteRequest.CreatedOn >= startDate && a.SiteRequest.CreatedOn <= endDate).Include(a => a.SiteRequest).ToList();
                var usedQty = aluminumUnits.Count();
                var usedMeters = aluminumUnits.Sum(a => a.SiteRequest.MetersNumber);
                var model = new ConsumedAluminumModel()
                {
                    AluminumId = alm.Id,
                    AluminumName = alm.TypeName,
                    Quantity = usedQty,
                    UsedMeters = (int)usedMeters
                };
                aluminumList.Add(model);
            }
            return aluminumList;
        }
        public (bool result, List<ConsumedItemsModel> itemsList) GetItemsConsumingReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var items = _db.SiteUsedItems.Include(a=>a.Item).Where(a => a.UsedDate >= startDate && a.UsedDate <= endDate).ToList().GroupBy(a => new { a.ItemId, a.Item })
                    .Select(t => new
                    {
                        ItemId = t.Key.ItemId,
                        ItemName = t.Key.Item.Name,
                        UsedQuntity = t.Sum(a => a.UsedQuantity),
                        Item = t.Key.Item
                    }).ToList();

                //List<ItemsConsumingReportModel> ItemsReport = new List<ItemsConsumingReportModel>();
                var itemsReport = items.Select(a =>
                {
                    //var consumeditems = items.Where(b => b.ItemId == a.ItemId).ToList();
                    //var consumeditemsQty = items.Where(b => b.ItemId == a.ItemId).Sum(a => a.Select(t => t.UsedQuantity));
                    return new ConsumedItemsModel
                    {
                        Id = a.ItemId,
                        Name = a.ItemName,
                        Quantity = a.UsedQuntity,
                        WareHouseQuantity = (int)a.Item.Quantity,
                        TotalPrice = a.Item.Price * a.UsedQuntity,
                        UnitPrice = a.Item.Price
                    };
                }).ToList();

                //var Items = _db.Items.Where(a => a.Status != Consts.DELETED).ToList();
                //var consumedAluminum = GetConsumedAluminumReport(startDate, endDate);
                //List<ConsumedItemsModel> itemsList = new List<ConsumedItemsModel>();
                //foreach (var conAlm in consumedAluminum)
                //{
                //    var almniumItems = _db.AluminumUsedItems.Include(a => a.Item).Where(a => a.AluminumTypeId == conAlm.AluminumId).ToList();
                //    foreach (var alm in almniumItems)
                //    {
                //        var itemQty = alm.ItemQuantity * conAlm.UsedMeters;
                //        var model = new ConsumedItemsModel()
                //        {
                //            Id = alm.ItemId,
                //            Name = alm.Item.Name,
                //            Quantity = itemQty,
                //            TotalPrice = itemQty * alm.Item.Price
                //        };
                //        itemsList.Add(model);
                //    }
                //}

                ////List<ItemsConsumingReportModel> ItemsReport = new List<ItemsConsumingReportModel>();
                //var itemsReport = Items.Select(a =>
                //{
                //    var consumedItems = itemsList.Where(c => c.Id == a.Id).ToList();
                //    return new ConsumedItemsModel
                //    {
                //        Id = a.Id,
                //        Name = a.Name,
                //        Quantity = consumedItems.Sum(c => c.Quantity),
                //        TotalPrice = consumedItems.Sum(c => c.TotalPrice),
                //        UnitPrice = a.Price,
                //    };
                //}).ToList();

                return (true, itemsReport);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }

        public (bool result, ItemsConsumingReportModel report) PrepareItemsConsumingReportModel(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = GetItemsConsumingReport(startDate, endDate);
                if (!result.result)
                    return (false, new());
                var report = new ItemsConsumingReportModel()
                {
                    Title = "تقرير استهلاك المواد ",
                    TotalPrice = result.itemsList.Sum(a => a.TotalPrice),
                    ConsumedItems = result.itemsList,
                    StartDate = startDate.ToShortDateString(),
                    EndDate = endDate.ToShortDateString()
                };
                return (true, report);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool result, SiteReportModel report) PrepareSiteReportModel(int Id)
        {
            try
            {
                var site = _db.SiteRequests.FirstOrDefault(a => a.Id == Id);
                var usedAlumuniums = _db.SiteUsedAlumimums.Include(a => a.AlumniumType).Where(a => a.SiteRequestId == Id).ToList();
                var itemsModel = new List<SiteReportModel.ItemModel>();
                var aluminumModel = new List<SiteReportModel.AluminumModel>();
                var itemsList = _db.SiteUsedItems.Where(a => a.SiteId == Id).Include(a => a.Item).ToList().GroupBy(a=> new { a.Item, a.UsedDate.Date }).Select(a => new
                {
                    itemName = a.Key.Item.Name,
                    totalPrice = a.Sum(t=>t.UsedQuantity) * a.Key.Item.Price,
                    price = a.Key.Item.Price,
                    usedQty = a.Sum(t=>t.UsedQuantity),
                    usedDate = a.Key.Date.ToShortDateString()
                }).ToList();
                 
                foreach(var itm in itemsList)
                {
                    var item = new SiteReportModel.ItemModel
                    {
                        ItemName = itm.itemName,
                        Price = (double)(itm.totalPrice),
                        UnitPrice = itm.price.ToString(),
                        UsedQuantity = itm.usedQty.ToString(),
                        UsedQuantityDate = itm.usedDate.ToString()
                    };
                    itemsModel.Add(item);
                }
                foreach (var alm in usedAlumuniums)
                {
                    //var items = _db.AluminumUsedItems.Include(a => a.Item).Where(a => a.AluminumTypeId == alm.AlmuniumTypeId).ToList();
                    //foreach (var itm in items)
                    //{
                    //    var item = new SiteReportModel.ItemModel
                    //    {
                    //        ItemName = itm.Item.Name,
                    //        UsedQuantity = ((int)(itm.ItemQuantity * site.MetersNumber)).ToString(),
                    //        UnitPrice = itm.Item.Price.ToString(),
                    //        Price = (itm.ItemQuantity * site.MetersNumber * itm.Item.Price).ToString()
                    //    };
                    //    itemsModel.Add(item);
                    //}
                    var meterPrice = CalculateAluminumPricePerMeter(alm.AlmuniumTypeId);
                    var aluminum = new SiteReportModel.AluminumModel
                    {
                        AluminumName = alm.AlumniumType.TypeName,
                        MeterPrice = meterPrice.ToString(),
                        TotalPrice = (meterPrice * (double)site.MetersNumber).ToString()
                    };
                    aluminumModel.Add(aluminum);
                }
                var model = new SiteReportModel()
                {
                    SiteOwnerName = site.SiteOwnerName,
                    SiteName = site.SiteName,
                    SiteOwnerPhone = site.SiteOwnerPhone,
                    SiteTotalPrice = CalculateSiteTotalPrice(site.Id).ToString(),
                    Meters = ((int)site.MetersNumber).ToString(),
                    WindowsNumber = site.WindowsNumber.ToString(),
                    DoorsNumber = site.DoorsNumber.ToString(),
                    Title = site.SiteName,
                    Aluminums = aluminumModel,
                    Items = itemsModel,
                    CurrentTotalPrice = itemsModel.Sum(a=>a.Price).ToString()
                };
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool result, SitesGeneralReportModel report) PrepareSitesGeneralReportModel(DateTime startDate, DateTime endDate)
        {
            try
            {
                var sites = _db.SiteRequests.Where(a => a.Status != Consts.DELETED).Where(a => a.CreatedOn.Date >= startDate.Date && a.CreatedOn.Date <= endDate.Date).ToList();
                var model = new SitesGeneralReportModel
                {
                    Title = "تقرير المواقع ",
                    StartDate = startDate.ToShortDateString(),
                    EndDate = endDate.ToShortDateString(),
                    TotalPrice = sites.Sum(a => a.TotalPrice).ToString(),
                    Sites = sites.Select(a => new SitesGeneralReportModel.SiteModel
                    {
                        SiteName = a.SiteName,
                        SiteOwnerName = a.SiteOwnerName,
                        SiteOwnerNumber = a.SiteOwnerPhone,
                        WindowsNumber = a.WindowsNumber.ToString(),
                        MetersNumber = a.MetersNumber.ToString(),
                        DoorsNumber = a.DoorsNumber.ToString(),
                        UsedAlumunium = GetSiteUsedAluminiumNames(a.Id),
                        TotalPrice = CalculateCurrentSiteTotalPrice(a.Id).ToString()
                    }).ToList(),
                };
                return (true, model);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        #endregion
        public double CalculateAluminumPricePerMeter(int aluminumId)
        {
            decimal totalPrice = 0;
            var aluminumItems = _db.AluminumUsedItems.Where(a => a.AluminumTypeId == aluminumId).Include(a => a.AlumniumType).Include(a => a.Item).ToList();
            foreach (var item in aluminumItems)
            {
                totalPrice += item.Item.Price * item.ItemQuantity;
            }
            return (double)totalPrice;
        }
        public double CalculateSiteTotalPrice(int siteId)
        {
            double totalPrice = 0;
            var siteUsedAluminiums = _db.SiteUsedAlumimums.Include(a => a.SiteRequest).Where(a => a.SiteRequestId == siteId).ToList();
            foreach (var alm in siteUsedAluminiums)
            {
                totalPrice += CalculateAluminumPricePerMeter(alm.AlmuniumTypeId) * (double)alm.SiteRequest.MetersNumber;
            }
            return totalPrice;
        }
        public double CalculateCurrentSiteTotalPrice(int siteId)
        {
            double totalPrice = 0;
            var siteUsedItems = _db.SiteUsedItems.Include(a => a.Item).Where(a => a.SiteId == siteId).ToList();
            foreach (var itm in siteUsedItems)
            {
                totalPrice += (double)(itm.UsedQuantity * itm.Item.Price);
            }
            return totalPrice;
        }
        public string GetSiteUsedAluminiumNames(int siteId)
        {
            var name = "";
            var siteUsedAluminium = _db.SiteUsedAlumimums.Include(a => a.AlumniumType).Where(a => a.SiteRequestId == siteId).ToList();
            foreach (var alm in siteUsedAluminium)
            {
                name += alm.AlumniumType.TypeName;
            }
            return name;
        }
        public (bool result, ItemsConsumingReportModel report) PrepareStoreReportModel()
        {
            try
            {
                var result = GetStoreReport();
                if (!result.result)
                    return (false, new());
                var report = new ItemsConsumingReportModel()
                {
                    Title = "تقرير المخزن ",
                    //TotalPrice = result.itemsList.Sum(a => a.TotalPrice),
                    ConsumedItems = result.itemsList
                };
                return (true, report);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool result, List<ConsumedItemsModel> itemsList) GetStoreReport()
        {
            try
            {
                var items = _db.Items.Where(e => e.Status == Consts.ACTIVE).ToList();

                var itemsReport = items.Select(a =>
                {
                  
                    return new ConsumedItemsModel
                    {
                        Id = a.Id,
                        Name = a.Name,
                        WareHouseQuantity = (int)a.Quantity
                    };
                }).ToList();

                return (true, itemsReport);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }
        public (bool result, ItemsConsumingReportModel report) PrepareItemsStoreReportModel(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = GetItemsStoreReport(startDate, endDate);
                if (!result.result)
                    return (false, new());
                var report = new ItemsConsumingReportModel()
                {
                    Title = "تقرير اضافة المواد ",
                    ConsumedItems = result.itemsList,
                    StartDate = startDate.ToShortDateString(),
                    EndDate = endDate.ToShortDateString()
                };
                return (true, report);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }

        public (bool result, List<ConsumedItemsModel> itemsList) GetItemsStoreReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var items = _db.ItemLogs.Include(e => e.Item).Where(a => a.AdditionDate.Date >= startDate.Date && a.AdditionDate.Date <= endDate.Date).ToList();

                var itemsReport = items.Select(a =>
                {

                    return new ConsumedItemsModel
                    {
                        Id = a.Id,
                        Name = a.Item.Name,
                        Quantity = (int)a.Quantity,
                        AdditionDate = a.AdditionDate.ToShortDateString()
                    };
                }).ToList();

                return (true, itemsReport);
            }
            catch (Exception ex)
            {
                EXH.LogException(ex, MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                return (false, new());
            }
        }

        #region User
        public async Task<bool> EditUserRole(string userId, string role)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(a => a.Id == userId);
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRoleAsync(user, role);
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
