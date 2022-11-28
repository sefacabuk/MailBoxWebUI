using MailBoxWebUI.BusinessLogicLayer;
using MailBoxWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxWebUI.Controllers
{
    public class MailBoxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult AddMail()
        {
            return View();
        }

        public IActionResult Mails()
        {
            return View();
        }
        
        public IActionResult Mail(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                RedirectToAction("Index");
            }

            MailBoxDto mailView = new MailBoxDto();

            string link = ApiCall.ApiLink + "api/MailBox/" + id;


            try
            {
                var httpClient = new HttpClient();

                string json = "";
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var postTask = httpClient.GetAsync(link);
                postTask.Wait();
                var postResult = postTask.Result;
                var responJsonText = postResult.Content.ReadAsStringAsync().Result;

                var sonuc = (JsonConvert.DeserializeObject<MailBoxDto>(responJsonText));

                UpdateMail(new MailBoxDto
                {
                    id = sonuc.id,
                    maiL_NAME = sonuc.maiL_NAME,
                    subject = sonuc.subject,
                    content = sonuc.content,
                    datE_TIME = sonuc.datE_TIME,
                    isread = true
                });

                mailView = sonuc;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return View(mailView);

        }


        [HttpGet]
        public IActionResult MailList()
        {
            string link = ApiCall.ApiLink + "api/MailBox/";

            List<MailBoxDto> sonucList = new List<MailBoxDto>();

            try
            {
                var httpClient = new HttpClient();
                //httpClient.DefaultRequestHeaders.Authorization =
                //    new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                string json = "";
                //json = Newtonsoft.Json.JsonConvert.SerializeObject(Request);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var postTask = httpClient.GetAsync(link);
                postTask.Wait();
                var postResult = postTask.Result;
                var responJsonText = postResult.Content.ReadAsStringAsync().Result;

                sonucList = (JsonConvert.DeserializeObject<List<MailBoxDto>>(responJsonText));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var sonuc = Json(sonucList);
            return sonuc;
        }


        [HttpGet]
        public IActionResult FindByMail(string id)
        {

            string link = ApiCall.ApiLink + "api/MailBox/" + id;


            try
            {
                var httpClient = new HttpClient();

                string json = "";
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var postTask = httpClient.GetAsync(link);
                postTask.Wait();
                var postResult = postTask.Result;
                var responJsonText = postResult.Content.ReadAsStringAsync().Result;

                var sonuc = (JsonConvert.DeserializeObject<MailBoxDto>(responJsonText));


                return Json(sonuc);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult SaveMail(MailBoxDto mailBox)
        {

            string link = ApiCall.ApiLink + "api/MailBox";


            try
            {
                var httpClient = new HttpClient();

                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(mailBox);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var postTask = httpClient.PostAsync(link, httpContent);
                postTask.Wait();
                var postResult = postTask.Result;
                var responJsonText = postResult.Content.ReadAsStringAsync().Result;

                var sonuc = (JsonConvert.DeserializeObject<MailBoxDto>(responJsonText));


                return Json(new { success = true, responseText = "Saved Success" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Error" });
            }

        }


        [HttpPut]
        public IActionResult UpdateMail(MailBoxDto mailBox)
        {

            string link = ApiCall.ApiLink + "api/MailBox/";

            try
            {
                var httpClient = new HttpClient();

                string json = "";
                json = Newtonsoft.Json.JsonConvert.SerializeObject(mailBox);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var postTask = httpClient.PutAsync(link, httpContent);
                postTask.Wait();
                var postResult = postTask.Result;
                var responJsonText = postResult.Content.ReadAsStringAsync().Result;

                var sonuc = (JsonConvert.DeserializeObject<MailBoxDto>(responJsonText));


                return Json(new { success = true, responseText = "Guncelle Basarili" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Guncelle Hata" });
            }


        }
        public IActionResult DeleteMail(string id)
        {

            string link = ApiCall.ApiLink + "api/MailBox/" + id;

            try
            {
                var httpClient = new HttpClient();

                string json = "";


                var postTask = httpClient.DeleteAsync(link);
                postTask.Wait();
                var postResult = postTask.Result;
                var responJsonText = postResult.Content.ReadAsStringAsync().Result;

                var sonuc = (JsonConvert.DeserializeObject<object>(responJsonText));

                

                if (postResult.StatusCode != HttpStatusCode.OK)
                {
                    return Json(new { success = false, responseText = "Deleted Error" });
                }


                return Json(new { success = true, responseText = "Deleted Success" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Deleted Error" });
            }


        }


    }
}
