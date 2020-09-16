using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FLYNOBORDERS.SelfB2B.Entities;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Framework.Object;
using FLYNOBORDERS.SelfB2B.Model;
using FLYNOBORDERS.SelfB2B.Web.Framework.Bases;
using FLYNOBORDERS.SelfB2B.Web.Framework.Utils;
using Newtonsoft.Json;
using System.Net.Mail;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    public class AccountController : BaseController
    {   
        /*public static string RandomNumber()
        {
            Random r = new Random();
            string activationCode = r.Next(100000, 999999).ToString();
            return activationCode;
        }

        public static string activationCode = RandomNumber();*/

        // GET: Account

        public ActionResult Registration()
        {
            var reg = new RegistrationModel();
            return View(reg);
        }

        [HttpPost]
        public ActionResult Registration(RegistrationModel registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationModel);
            }

            MD5 md5 = MD5.Create();
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(registrationModel.CPass);
            byte[] hash = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            var Hashedpass = sb.ToString();

            var companyInfo = new CompanyInfo()
            {
                CName = registrationModel.CName,
                TradeLicenseNo = registrationModel.TradeLicenseNo,
                Website = registrationModel.Website,
                CEmail = registrationModel.CEmail,
                PhnNumber = registrationModel.PhnNumber,
                Address = registrationModel.Address,
                City = registrationModel.City,
                Zip = registrationModel.Zip
            };

            var userInfo = new UserInfo()
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                UEmail = registrationModel.UEmail,
                Pass = Hashedpass,
                TimeZone = registrationModel.TimeZone,
                PreferredRoe = registrationModel.PreferredRoe,
                UserTypeID = (int)EnumCollection.UserTypeEnum.Agent,
                AdminVerifyID = (int)EnumCollection.AdminVarifyEnum.NotAccept,
                CompanyInfo = companyInfo,
            };

            var result = UserInfoRepo.Save(userInfo);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(registrationModel);
            }

            SendMail(registrationModel.CEmail);

            Session["uEmail"] = registrationModel.UEmail;


            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated && HttpUtil.Current != null)
            {
                if (HttpUtil.Current.UserTypeID == (int) EnumCollection.UserTypeEnum.Admin)
                    return RedirectToAction("Index", "Admin");
                else if (HttpUtil.Current.UserTypeID == (int)EnumCollection.UserTypeEnum.Agent)
                    return RedirectToAction("Index", "Agent");
            }
            var loginModel = new LoginModel();
            return View(loginModel);
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            MD5 md5 = MD5.Create();
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(loginModel.Pass);
            byte[] hash = md5.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            var HashLoginPass = sb.ToString();

            var result = UserInfoRepo.Login(loginModel.CEmail, HashLoginPass);

            if (result.HasError)
            {
                ViewBag.Error = result.Message;
                return View(loginModel);
            }

            var userProfile = new UserProfile()
            {
                ID = result.Data.UID,
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                UserTypeID = result.Data.UserTypeID,
                UEmail = result.Data.UEmail,
                CEmail = result.Data.CompanyInfo.CEmail,
                PreferredRoe = result.Data.PreferredRoe,
                CName = result.Data.CompanyInfo.CName,
                TradeLicenseNo = result.Data.CompanyInfo.TradeLicenseNo,
                Website = result.Data.CompanyInfo.Website,
                PhnNumber = result.Data.CompanyInfo.PhnNumber,
                Address = result.Data.CompanyInfo.Address,
                City = result.Data.CompanyInfo.City,
                Zip = result.Data.CompanyInfo.Zip
            };

            var upJson = JsonConvert.SerializeObject(userProfile);
            FormsAuthentication.SetAuthCookie(upJson, false);

            if (userProfile.UserTypeID == (int) EnumCollection.UserTypeEnum.Admin)
                return RedirectToAction("Index", "Admin");
            else if (userProfile.UserTypeID == (int)EnumCollection.UserTypeEnum.Agent)
                return RedirectToAction("Index", "Agent");
            else
              return RedirectToAction("Logout");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(UserInfo userInfo)
        {
            var result = UserInfoRepo.ForgotPassword(userInfo.CompanyInfo.CEmail);
            MailMessage mailMessage = new MailMessage("mdrubel1476@gmail.com", userInfo.CompanyInfo.CEmail);
            mailMessage.Subject = "Password Recovery";
            mailMessage.Body = "Your Password is: " + result.Data.Pass;

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Send(mailMessage);

            return View("Login");
        }      

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public static void SendMail(string cEmail)
        {

            MailMessage mailMessage = new MailMessage("mdrubel1476@gmail.com", cEmail);
            mailMessage.Subject = "Verification Code";
            mailMessage.Body = "Congratulatios! Your Registration was Successfully Done. Please Wait for Admin Approval";

            SmtpClient smtpClient = new SmtpClient();

            /*SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "mdrubel1476@gmail.com",
                Password = "rubel1476"
            };
            smtpClient.EnableSsl = true;*/
            smtpClient.Send(mailMessage);
        }


    }
}