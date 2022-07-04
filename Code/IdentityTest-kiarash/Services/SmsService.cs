using System.Net;

namespace IdentityTest.Services
{
    public class SmsService
    {
        public void Send(string PhoneNumber,string Code)
        {
            var client = new WebClient();
            string url =$"http://panel.kavenegar.com/v1/apikey/verify/lookup.json?receptor={PhoneNumber}&token={Code}&template=VerifyMofidAccount";
            var content= client.DownloadString(url);

        }
    }
}
