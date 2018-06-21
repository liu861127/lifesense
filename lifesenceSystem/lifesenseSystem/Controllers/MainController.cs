using lifesenseSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace lifesenseSystem.Controllers
{

    public class MainController : Controller
    {
        //
        // GET: /Main/

        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// AJAX登录
        /// </summary>
        /// <returns></returns>
        public JsonResult LogYes()
        {
            object obj = null;
            try
            {
                if (Session["ValidateCode"].ToString() != Request["ValiCode"])
                {
                    obj = new
                    {
                        result = false,
                        message = "验证码错误",
                        url = ""
                    };
                }
                else
                {
                    string customername = Request["UserID"].ToString();
                    //此处验证用户名、密码
                    if (!ValidateLogOn(Request["HiUserPwd"].ToString(), customername))
                    {
                        obj = new
                        {
                            result = false,
                            message = "用户名或密码错误",
                            url = ""
                        };
                    }
                    else
                    {

                            string userid = Request["UserID"].ToString();
                            //保存用户身份票据
                            FormsAuthentication.SetAuthCookie(userid, false);
                            //存入Session
                            Session["User"] = userid;
                            Session.Timeout = 240;
                            string Sessionid = Session.SessionID;
                            HttpContext.Cache.Insert(userid, Sessionid);
                            //Session["Username"] = Request["UserID"].ToString();
                            //如果是跳转过来的，则返回上一页面
                            if (!string.IsNullOrEmpty(Request["ReturnUrl"]))
                            {
                                obj = new
                                {
                                    result = true,
                                    message = "",
                                    url = Request["ReturnUrl"].ToString()
                                };
                            }
                            else
                            {
                                string url = "Content/Index";
                                //用户个人主页
                                obj = new
                                {
                                    result = true,
                                    message = "登录成功",
                                    url = url
                                };
                            }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                obj = new
                {
                    result = false,
                    message = ex.Message,
                    url = ""
                };
            }
            return Json(obj);
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            //取消Session会话
            Session.Abandon();
            //删除Forms验证票证
            FormsAuthentication.SignOut();
            Session["User"] = null;
            return RedirectToAction("Index", "Content");
        }
        /// <summary>
        /// 刷新验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        private bool ValidateLogOn(string pwd, string loginName)
        {
            lifesenseEntities modelEntities = new lifesenseEntities();
            t_admin model = modelEntities.t_admin.Where(m => m.LoginPwd == pwd && m.LoginName == loginName).FirstOrDefault();
            if (model!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
