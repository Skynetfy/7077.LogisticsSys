using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.BLL
{
    public interface ISendEmailManager
    {
        void Send(string toWho, string msg, string obj);
    }
}
