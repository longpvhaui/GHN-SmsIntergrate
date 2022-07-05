using GHN_SmsIntergrate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GHN_SmsIntergrate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GHNSmsIntergrateController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string urlGetDataFromGHN = "https://dev-online-gateway.ghn.vn/shiip/public-api/v2/shipping-order/detail";
        private const string urlSendsms = "https://br.noahsms.com/api/SendSms/SendSmsCskhWithoutChecksum";
        private const string token = "0755c4e5-fc0d-11ec-8edf-ba8fb51195cc";
        private readonly AppSetting _appSetting;
        public GHNSmsIntergrateController(IHttpClientFactory httpClientFactory, AppSetting appSetting)
        {
            _httpClientFactory = httpClientFactory;
            _appSetting = appSetting;
        }

        [HttpPost]
        [Route("order-status")]
        public async Task<IActionResult> GetOrderStatus(OrderStatusRequest orderStatus)
        {
            if (orderStatus != null)
            {
                await GetInfoDetail(orderStatus.OrderCode);
                return Ok();
            }
            else return BadRequest();
        }
        [HttpGet]
        public async Task GetInfoDetail(string orderCode)
        {

            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                urlGetDataFromGHN);
            httpRequestMessage.Content = new StringContent(
               JsonConvert.SerializeObject(new { order_code = orderCode }),
               Encoding.UTF8, "application/json");
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("Token", token);
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                OrderDetailResponse myDeserializedClass = JsonConvert.DeserializeObject<OrderDetailResponse>(result);
                SendSms(myDeserializedClass.data.to_phone, myDeserializedClass.data.status);
            }
        }


        [HttpPost]
        [Route("lfof")]
        public async Task SendSms(string phones,string status)
        {
            var requestContent = new SmsModel();
            requestContent.UserName = _appSetting.UserName;
            requestContent.Password = _appSetting.Password;
            requestContent.BrandName = Constants.BRAND_NAME;
            requestContent.TimeSend = DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss");
            requestContent.ClientId = _appSetting.ClientId;
            requestContent.Phones.Add(phones);
            if(status != null )
            {
                if (status == "ready_to_pick") requestContent.SmsContent = Constants.READY_TO_PICK;
                else if(status == "ready_to_delivery") requestContent.SmsContent = Constants.READY_TO_DELIVERY;
                else requestContent.SmsContent = Constants.COMPLETE;
            }
            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.PostAsJsonAsync(urlSendsms, requestContent);
        }
            
    
    }
}
