using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sys.RSA2.Alipay
{
    public class AlipayConfig
    {
        public static string local_private_key = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQC5KLwfQgemwSFU95fBWNjxautfCade0bFfgsPl5AZRNHo2lYbzo4daGzcDl3wzN16P0FpLTxWT0k17RV5+nPGGUGsqH4Gut4bFmzvkER50mbexTaDc1wvBvDaNxAI1ozi7z1cR5Y/lFYINbRcLEWuLVHKEm/2xH4b93pVF1pTYRjJvbaYmlO4TZRr77GTDkb255wGZumJ5bsxFInHcmHqCkJXinSvpXW+dwSdeYFPXKmvORLHs9ezH9qy2+fKgiT/v0GgGnW008sbZj+RdlsClSpzDzDeyHQ0YWMwYeOztuSTx76JG3B+vUD1naahZwOgFrXBCN2TDn55otxfwX6zXAgMBAAECggEAV4EExtDxxMDo+7q3IUzX2d/ptFRfoNQAu16VLgQBaEcgqsMnr+TMhqzt5uikbZ/xNI4G3ihVzjJJI8S4z6VkD1HaE2Ioa2WYb7LZLwNYHUq4ITbKmxOWHszQC1dio+6rVTa7s12GD4GGoMm5ZiWpaYNTS5Bc6GQ566hRa1vpo/tZ2UshzL35pgC6Yt7bZ31Wx9bwDvunNY3rF82E0odhU0ufdJYWl0ybVfmBzGpB9sXQoL3YBVmLrYMWfVLE7jvld1ZOGiDI7zLmD2EcQlnL5Ygk4tLVOrJE915FRdNVhesd9veY2z6FcyQ7ggrhNYX+G2FQqL6Rz6xu+m9LztIdoQKBgQDxudQhcj1QETOq+eSPIMs5RjQs1YwUNVO/lXgQEPRT+w73wrYkmz57YxVUtRPhQVadqeL7ldasTK8l/nUmpC5TB5i1UCNN08j0pIDVhwt3uGGQ9JO/ebiL2xo65koc3e9tJ43i9FmtHw/6BP78je4XgH1Ge5j5HqcpZzddKqr+0wKBgQDEF8k8CPM4ioWLWocSYnanUp9K0La8J5BQx6nhg5jgpZ46CznSqKbRS1PYsiAw2Wh9lOebiJpmWHEvPClk/SzF7uXj+MaaXuqhBmo/DA3+cSiSUI/aNCovoN3r2gBMuoP1M+c4eLOSdGYY8mphNGfUHNlPBRRdcTWzAbYqEy//bQKBgDgwXBESPxJXjU0XHXvvwY2ktggd85vW8Yq9MdV6O3EyEL6i8jf8JkAEPjcciGx6BapMjUiyAd63TJdmWNEMpQAD6glrgWlb64CCpLf91jqUD4nkcFu37aAE5EoAsgWXyUn0QdUZu6a0a8BQXP1T+J5Z7cTughaWe4DyTzpG6sylAoGAR4LVJEuvR0NdCEDvWUkHNGXxKXuL6HoVKyBlV5SCQAql9Uz6vVQ74b+yyCfNjYL7lDCldhcPF5vRSwSJpAagOV6x/71N0CYlMqwAOxXOVKg3v4QZsd2sNMpZpBVQXqr2TImtHO8HrbU9NoPRP7m406XhqeuUTa9ngexx3k45BtECgYEA2scR08cc4IyhHY/05Gp6aOt4KEK0GR0SuaXD+IXpXuw+ASAnIXzyxEiGKwTV/qkzNNl0tX3ZV+m1Dk47WhjLsGEeSX48LrBA4SlEdUdmDDsnYt/nZ3ONce5uRH1V0fiz+ZQEawBtSpgqtOMc6L+toTl/eNREfnq/NN1BQiuZgIo=";

        public static string local_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuSi8H0IHpsEhVPeXwVjY8WrrXwmnXtGxX4LD5eQGUTR6NpWG86OHWhs3A5d8Mzdej9BaS08Vk9JNe0VefpzxhlBrKh+BrreGxZs75BEedJm3sU2g3NcLwbw2jcQCNaM4u89XEeWP5RWCDW0XCxFri1RyhJv9sR+G/d6VRdaU2EYyb22mJpTuE2Ua++xkw5G9uecBmbpieW7MRSJx3Jh6gpCV4p0r6V1vncEnXmBT1yprzkSx7PXsx/astvnyoIk/79BoBp1tNPLG2Y/kXZbApUqcw8w3sh0NGFjMGHjs7bkk8e+iRtwfr1A9Z2moWcDoBa1wQjdkw5+eaLcX8F+s1wIDAQAB";
        //partner ID,It's a 16-bit string start with "2088".Login in https://globalprod.alipay.com/order/myOrder.htm to see your partner ID.
        //Below is a default sandbox account for your reference,pls apply your own sandbox account here:https://global.alipay.com/help/integration/23
        public static string partner = "2088621891276675";

        //merchant's private key,the guide to generate merchant's public key and private key,pls refer to:https://global.alipay.com/service/website/25
        public static string private_key = HttpRuntime.AppDomainAppPath.ToString() + "key\\rsa_private_key.pem";

        //Alipay's public key
        //Alipay's public key,below is Alipay's public key of sandbox environment
        //Pls use the Alipay's public key(alipay_public_key.pem) of production environment instead if you are in production environment
        //public static string alipay_public_key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqHFTyE4EJ4f5JM9uK4WMypHHCsC6LvcL2YU92FcVNzdQxjqerdQBVshytIpNIx9JcsT2F8cdRIomF2gHkaytpTMelQisewmGEm5/gectLqUJltL97CGMCNbm9dWkL9O+Xe+ZX5+v3aE7XuGhVF4ANx7eS6/EPaqaFJT1vFSfuMrXByQnHqIWLBRfjRDg4oQPZaMao+Iclsr8viERHFAGYHVKubGOVVb8c2Vfq6sZ9DCc7G1nLEjmOyUgdLpV2LaaCPnkxVvF4SNOFNkHtbPUYuGmnAdEnwiiTaI/xBguFwvy7CoQ05Bue6zC17ZQ4vChSr+W33EPDDtPP4ONw94+OQIDAQAB";
        public static string alipay_public_key = HttpRuntime.AppDomainAppPath.ToString() + "key\\sandbox_alipay_public_key.pem";

        //Page for receiving asynchronous Notification. It should be accessable from outer net.No custom parameters like '?id=123' permitted.
        public static string notify_url = "http://MerchantSite/create_forex_trade-CSHARP-UTF-8-RSA/notify_url.aspx";

        //Page for synchronous notification.It should be accessable from outer net.No custom parameters like '?id=123' permitted.
        public static string return_url = "http://www.alipay.com";

        // 签名方式
        public static string sign_type = "RSA";

        // input_charset   gbk and utf-8 are supported now.
        public static string input_charset = "utf-8";

        //Service name of the interface.No need to modify.
        public static string service = "create_forex_trade";

    }
}
