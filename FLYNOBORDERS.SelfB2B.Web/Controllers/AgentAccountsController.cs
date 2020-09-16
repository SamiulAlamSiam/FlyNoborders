using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using FLYNOBORDERS.SelfB2B.Entities;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Framework.Object;
using FLYNOBORDERS.SelfB2B.Model;
using FLYNOBORDERS.SelfB2B.Web.Framework.Attributes;
using FLYNOBORDERS.SelfB2B.Web.Framework.Bases;
using FLYNOBORDERS.SelfB2B.Web.Framework.Utils;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    [SELFB2BAuthorize(EnumCollection.UserTypeEnum.Agent)]
    public class AgentAccountsController : BaseController
    {
        // GET: Admin

        public ActionResult GeneralLedger()
        {
            return View();
        }

        public ActionResult DepositRequest()
        {
            var depositeModel = new DepositeModel();
            return View(depositeModel);
        }

        [HttpPost]
        public ActionResult DepositRequest(DepositeModel model, HttpPostedFileBase image1)
        {
            /*if (!ModelState.IsValid)
            {
                return View(model);
            }*/

            if (image1 != null)
            {
                model.ReceiptImage = new byte[image1.ContentLength];
                image1.InputStream.Read(model.ReceiptImage, 0, image1.ContentLength);
            }

            var deposite = new Deposite()
            {
                DType = model.DType,
                ChequeNo = model.ChequeNo,
                RefNum = model.RefNum,
                ChequeBank = model.ChequeBank,
                Amount = model.Amount,
                Date = model.Date,
                BankId = model.BankId,
                ReceiptImage = model.ReceiptImage,
                UserID = HttpUtil.Current.ID,
                AdminVerifyId = (int)EnumCollection.AdminVarifyEnum.NotAccept
            };

            var result = DepositeRepo.Save(deposite);

            if (result.HasError == true)
            {
                ViewBag.Error = result.Message;
                return View(model);
            }
            //Context.Deposites.Add(deposite);
            //Context.SaveChanges();
            ViewBag.Success = "Your Deposite is send to Admin for Verify";
            return View();
        }

    }
}