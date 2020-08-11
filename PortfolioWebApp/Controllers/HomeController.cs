using System;
using System.Web.Mvc;
using System.Web.Security;

namespace PortfolioWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.User user)
        {
            //if (user.UserName=="Admin@mail.com" && user.Password=="admin")
            //{
            //    Session["userId"] = 1;
            //    Session["userName"] = user.UserName;
            //    FormsAuthentication.SetAuthCookie(user.UserName, false);
            //    return RedirectToAction(nameof(Dashboard));
            //}

            var authUser = new DataObjects.Auth().Login(user.UserName, user.Password);
            if (authUser != null)
            {
                Session["userId"] = authUser.Id.ToString();
                Session["userName"] = user.UserName;
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction(nameof(Dashboard));
            }

            ModelState.AddModelError("", "Invalid credential");
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult Dashboard()
        {
            var userId=Convert.ToInt32( Session["userId"].ToString());

            var work = new DataObjects.User().GetWorkDetailsAdded(userId, DateTime.Now);
            work.UserId = userId;
            //var work = new Models.Work()
            //{
            //    UserId = userId,
            //    Id = 1,
            //    Details = string.Empty
            //};

            return View(work);
        }
        [Authorize]
        [HttpPost]
        public ActionResult Dashboard(Models.Work work)
        {
            work.Date = DateTime.Now.Date;
            var result = new DataObjects.User().AddWorkDetails(work);
            if (result)
            {
                ViewBag.StatusMsg = "Data updated successfully";
            }
            else
            {
                ViewBag.StatusMsg = "Opps! Something went wrong.";

            }

            return View(work);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.User user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.StatusMsg = "Invalid Data";
                return View();
            }
            var response = new DataObjects.User().AddNew(user);
            ViewBag.StatusMsg = response;

            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            var work = new Models.Work();
            work.UserId =Convert.ToInt32( Session["userId"].ToString());
            work.Logout = DateTime.Now;
            work.Date = DateTime.Now.Date;
            var response = new DataObjects.User().AddLogoutDate(work);
            FormsAuthentication.SignOut();
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public FileContentResult GetHistory()
        {
            var userName = Session["userName"].ToString();
            string historyFileContent = new DataObjects.User().GetHistoryFilePath(userName);
            return File(new System.Text.UTF8Encoding().GetBytes(historyFileContent), "text/csv",string.Format("{0}.csv",userName));
        }
    }
}