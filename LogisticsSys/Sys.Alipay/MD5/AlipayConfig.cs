using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sys.Alipay.MD5
{
    public class AlipayConfig
    {
        //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        //↓↓↓↓↓↓↓↓↓↓Please configure your basic information here↓↓↓↓↓↓↓↓↓↓

        // 合作身份者ID，签约账号，以2088开头由16位纯数字组成的字符串，查看地址：https://globalprod.alipay.com/order/myOrder.htm
        // 下面的值默认是一个沙箱测试账号，您可参考下面网址申请自己的沙箱测试账号：https://global.alipay.com/help/integration/23
        //partner ID,It's a 16-bit string start with "2088".Login in https://globalprod.alipay.com/order/myOrder.htm to see your partner ID.
        //Below is a default sandbox account for your reference,pls apply your own sandbox account here:https://global.alipay.com/help/integration/23

        //product
        //public static string partner = "2088931778686740";
        //dev
        //public static string partner = "2088621882051320";
        public static string partner = ConfigurationManager.AppSettings["alipay_partner"];
        // MD5密钥，安全检验码，由数字和字母组成的32位字符串，查看地址：https://b.alipay.com/order/pidAndKey.htm
        //MD5 key . The security check code, 32 bit string composed of numbers and letters.See your key at https://globalprod.alipay.com/order/myOrder.htm
        // product
        //public static string key = "4m42ivy4vay90af153gem3k4hcdn9ki9";
        // dev
        //public static string key = "pa3o4imvkc82lktwpwisfce331q62hs6";
        public static string key = ConfigurationManager.AppSettings["alipay_Md5key"];
        // 服务器异步通知页面路径，不能加?id=123这类自定义参数,必须外网可以正常访问
        //Page for receiving asynchronous Notification. It should be accessable from outer net.No custom parameters like '?id=123' permitted.
        public static string notify_url = "http://rusqg.com/alipay/notify";

        // 页面跳转同步通知页面路径，需http://格式的完整路径，不能加?id=123这类自定义参数，必须外网可以正常访问
        //Page for synchronous notification.It should be accessable from outer net.No custom parameters like '?id=123' permitted.
        public static string return_url = "http://rusqg.com/";

        // 签名方式
        //sign_type
        public static string sign_type = "MD5";

        // 调试用，创建TXT日志文件夹路径，见AlipayCore.cs类中的LogResult(string sWord)打印方法。
        //Create a TXT log folder path,pls refer to the logResult(String sWord) function in the AlipayCore.cs file.

        // 字符编码格式 目前支持 gbk 或 utf-8
        // input_charset   gbk and utf-8 are supported now.
        public static string input_charset = "utf-8";


        public static string log_path = HttpRuntime.AppDomainAppPath.ToString() + "log\\";

        // 调用的接口名，无需修改
        //Service name of the interface.No need to modify.

        //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑
        //↑↑↑↑↑↑↑↑↑↑Please configure your basic information here↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑		

    }
}
