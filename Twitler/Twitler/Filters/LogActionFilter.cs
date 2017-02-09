using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Twitler.Filters
{
    public class LogActionFilterAttribute: ActionFilterAttribute
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            logger.Info($"Start of executing {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName}/{filterContext.ActionDescriptor.ActionName}. User: {filterContext.HttpContext.User.Identity.Name}. Address: {filterContext.HttpContext.Request.UserHostAddress}. Request time stamp: {filterContext.HttpContext.Timestamp} ");
            base.OnActionExecuting(filterContext);
        }
    }
}