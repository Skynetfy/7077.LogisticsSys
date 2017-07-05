using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Common;

namespace Sys.BLL
{
    public class OrderSendEmailManager : ISendEmailManager
    {
        public void Send(string toWho, string msg, string obj)
        {
            try
            {
                var emailHost = ConfigHelper.GetValue("EmailHost");
                var emailObject = obj;
                var emailUser = ConfigHelper.GetValue("EmailUser");
                var emailPass = ConfigHelper.GetValue("EmailPass");
                var emailFrom = ConfigHelper.GetValue("EmailFrom");
                var emailBody = msg;
                var emailAddress = new List<string>();
                emailAddress.Add(toWho);
                //emailAddress.Add("skynetfy@qq.com");
                EmailHelper.sendMail(emailObject, emailBody, emailFrom, emailAddress, emailHost, emailUser, emailPass);
            }
            catch (Exception)
            {
                
                //throw;
            }
          
        }
    }
}
