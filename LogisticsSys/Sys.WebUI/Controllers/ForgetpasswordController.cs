using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Sys.BLL.Users;
using Sys.Common;
using Sys.WebUI.Models;

namespace Sys.WebUI.Controllers
{
    public class ForgetpasswordController : BaseController
    {
        public string GetRandomStr(bool b, int n)
        {

            string str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (b = true)
            {
                str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";//复杂字符
            }
            StringBuilder SB = new StringBuilder();
            Random rd = new Random();
            for (int i = 0; i < n; i++)
            {
                SB.Append(str.Substring(rd.Next(0, str.Length), 1));
            }
            return SB.ToString();

        }
        // GET: Forgetpassword
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string username)
        {
            var message = "";
            if (!string.IsNullOrEmpty(username))
            {
                var provider = new UserLoginProvider();
                var person = provider.GetUser(username);
                if (person != null)
                {
                    if (person.Email != null)
                    {
                        var newpassword = GetRandomStr(true, 12);

                        person.Password = DEncrypt.Md5(newpassword);
                        provider.UpdateUser(person);
                        var emailHost = ConfigHelper.GetValue("EmailHost");
                        var emailObject = ConfigHelper.GetValue("EmailObject");
                        var emailUser = ConfigHelper.GetValue("EmailUser");
                        var emailPass = ConfigHelper.GetValue("EmailPass");
                        var emailFrom = ConfigHelper.GetValue("EmailFrom");
                        var emailBody = string.Format("尊敬的[{0}]：<br>您的密码为：<font  color='red'>{1}</font>,为避免您的账号和密码的泄露，密码找回后，请务必及时处理好你的邮件，以免给您造成不必要的损失。<br>欢迎使用，谢谢。", person.UserName, newpassword);
                        var emailAddress = new List<string>();
                        emailAddress.Add(person.Email);
                        //emailAddress.Add("skynetfy@qq.com");
                        EmailHelper.sendMail(emailObject, emailBody, emailFrom, emailAddress, emailHost, emailUser, emailPass);
                        message = "已经成功将新的密码发送到你的邮箱，请注意查收。";
                    }
                    else
                    {
                        message = "你的个人信息不完整，无法发送邮件，请联系管理员";
                    }
                }
                else
                {
                    message = "用户名不存在";
                }
            }
            return Content(message);
        }
    }
}