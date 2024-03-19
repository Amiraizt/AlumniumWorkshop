using Alumnium.Core.DbContext;
using Alumnium.Core;

namespace AlumniumWorkshop.Areas.Helpers
{
    public class ExceptionHandler
    {
        ApplicationDBContext _db;
        public ExceptionHandler(ApplicationDBContext db)
        {
            _db = db;

        }
        public void LogException(Exception exception, string className = "", string methodName = "")
        {
            try
            {
                ExceptionLog exceptionLog = new ExceptionLog
                {
                    ClassName = className,
                    MethodName = methodName,
                    Message = exception?.Message,
                    DateTime = DateTime.Now,
                    InnerException = exception?.InnerException?.Message,
                    StackTrace = exception?.StackTrace
                };
                
                _db.Add<ExceptionLog>(exceptionLog);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public void LogException(string body, string title, string className = "", string methodName = "")
        {
            try
            {
                ExceptionLog exceptionLog = new ExceptionLog
                {
                    ClassName = className,
                    MethodName = methodName,
                    Message = body,
                    DateTime = DateTime.Now,
                    InnerException = title,
                    StackTrace = ""
                };
                _db.ExceptionLogs.Add(exceptionLog);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }


    }
}
