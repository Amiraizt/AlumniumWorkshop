using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumnium.Core
{
    public class Consts
    {
        #region User Types
        public const string GeneralAdminAccountType = "GeneralAdmin";
        public const string AdminAccountType = "Admin";
        #endregion

        #region Status
        public const string ACTIVE = "Active";
        public const string NOTACTIVE = "NotActive";
        public const string DELETED = "Deleted";
        #endregion

        #region User Roles 
        public const string AdminUserRole = "Admin";
        public const string SystemAdminUserRole = "System Admin";
        #endregion
        public enum NotificationType
        {
            error,
            success,
            warning,
            info
        }
    }
}
