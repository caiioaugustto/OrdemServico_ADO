using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Uteis
{
    public class AutorizacaoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Uteis.SessionManager.IsAuthenticated)
            {
                //HttpContext.Current.Response.RedirectToAction("Index", "Login");
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary(
                       new { controller = "Login", action = "Index"}

                       )
                   );
            }
        }
    }
}