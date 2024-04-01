using Alumnium.Core.DbContext;
using Alumnium.Core;
using AlumniumWorkshop.Areas.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AlumniumWorkshop.Controllers
{
    public class BaseController : Controller
    {
        protected CoreServices CS;
        protected UserManager<ApplicationUser> _userManager;
        protected SignInManager<ApplicationUser> _signManager;
        protected ExceptionHandler EXH;
        protected ApplicationDBContext _db;
        protected IConfiguration _configuration;
        protected RoleManager<IdentityRole> _roleManager;

        public BaseController(RoleManager<IdentityRole> roleMgr,IConfiguration configuration, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ApplicationDBContext db)
        {
            _userManager = userManager;
            _signManager = signInManager;
            _db = db;
            CS = new CoreServices(db, configuration);
            EXH = new ExceptionHandler(db);
            _roleManager = roleMgr;
        }
        
        protected void Alert(string message, Consts.NotificationType notificationType)
        {
            string msg = "";
            switch (notificationType)
            {
                case Consts.NotificationType.success:
                    //msg = " title='تم';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";
                    msg = " title='تم';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; sweetAlert( title,message,type);";
                    break;
                case Consts.NotificationType.error:
                    //msg = "swal({title: " + "خطأ" + ",text:" + message + ",type: " + notificationType.ToString().ToLower() + "});";
                    //msg = " title='خطأ';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";
                    msg = " title='خطأ';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; sweetAlert( title,message,type);";

                    break;
                case Consts.NotificationType.info:
                    //msg = " swal({title: " + "تنبيه" + ",text:" + message + ",type: " + notificationType.ToString().ToLower() + "});";
                    //msg = " title='تنبيه';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";
                    msg = " title='تنبيه';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";

                    break;
                case Consts.NotificationType.warning:
                    //msg = "swal({title: " + "تحذير" + ",text:" + message + ",type: " + notificationType.ToString().ToLower() + "});";
                    //msg = " title='تحذير';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";
                    msg = " title='تحذير';message='" + message + "';type='" + notificationType.ToString().ToLower() + "'; swal({title: title,text:message,type: type});";

                    break;
            }
            //var msg = "sweetAlert('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "";
            TempData["notification"] = msg;
        }
    }
}
