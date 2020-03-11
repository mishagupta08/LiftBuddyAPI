using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using LiftBuddyAPI.Models;
using Newtonsoft.Json;
using DataAccess;

namespace LiftBuddyAPI.Controllers
{
    public class LoginController : ApiController
    {
        public HomeRepository repository;        

        [HttpGet, Route("api/Home/Login/{MobileNo}")]
        public async Task<IHttpActionResult> Login(string MobileNo)
        {
            repository = new HomeRepository();
            var objResponse = await repository.Login(MobileNo);
            return Content(HttpStatusCode.OK, objResponse, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Home/VerifyOTP")]
        public async Task<IHttpActionResult> VerifyOTP()
        {
            repository = new HomeRepository();
            var detail = await Request.Content.ReadAsStringAsync();
            var OTPInfo = JsonConvert.DeserializeObject<tblOtpTransaction>(detail);
            this.repository = new HomeRepository();            
            var objResponse = await repository.VerifyOTP(OTPInfo);
            return Content(HttpStatusCode.OK, objResponse, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, Route("api/Home/GetUser/{MobileNo}")]
        public async Task<IHttpActionResult> GetUser(string MobileNo)
        {
            repository = new HomeRepository();            
            var result = await repository.GetUser(MobileNo);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Home/SaveUserDetail")]
        public async Task<IHttpActionResult> SaveProfileDetail()
        {
            repository = new HomeRepository();
            var detail = await Request.Content.ReadAsStringAsync();
            var prodetail = JsonConvert.DeserializeObject<tblUser>(detail);
            var result = await repository.SaveProfileDetail(prodetail);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }        

        [HttpPost, Route("api/Home/UpdateUserProfile")]
        public async Task<IHttpActionResult> UpdateUserProfile()
        {
            repository = new HomeRepository();
            var detail = await Request.Content.ReadAsStringAsync();
            var prodetail = JsonConvert.DeserializeObject<tblUser>(detail);
            var result = await repository.UpdateUserProfile(prodetail);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, Route("api/Home/CheckUNValidity/{UserId}/{UserName}")]
        public async Task<IHttpActionResult> CheckUNValidity(int UserId, string UserName)
        {
            repository = new HomeRepository();
            var result = await repository.CheckUNValidity(UserId, UserName);
            return Content(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

    }
}
