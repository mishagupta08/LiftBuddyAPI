using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using LiftBuddyAPI.Models;
using DataAccess;

namespace LiftBuddyAPI.Controllers
{
    public class HomeController : ApiController
    {
        public HomeRepository repository;

        [HttpGet, Route("api/Home/GetVehicleTypeList")]
        public async Task<IHttpActionResult> GetVehicleTypeList()
        {
            repository = new HomeRepository();
            var objResponse = await repository.GetVehicleTypeList();
            return Content(HttpStatusCode.OK, objResponse, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Home/ManageRequestDetail/{operation}")]
        public async Task<IHttpActionResult> ManageRequestDetail(string operation)
        {
            repository = new HomeRepository();
            var detail = await Request.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<tblRequestRide>(detail);
            var objResponse = await repository.ManageRequestDetail(data, operation);
            return Content(HttpStatusCode.OK, objResponse, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, Route("api/Home/ManageOfferRideDetail/{operation}")]
        public async Task<IHttpActionResult> ManageOfferRideDetail(string operation)
        {
            repository = new HomeRepository();
            var detail = await Request.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<tbleOfferRide>(detail);
            var objResponse = await repository.ManageOfferRideDetail(data, operation);
            return Content(HttpStatusCode.OK, objResponse, Configuration.Formatters.JsonFormatter);
        }

        
    }
}
