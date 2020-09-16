using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLYNOBORDERS.SelfB2B.Entities;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Framework.Helper;
using FLYNOBORDERS.SelfB2B.Framework.Object;

namespace FLYNOBORDERS.SelfB2B.Repo
{
    public class DepositeRepo : BaseRepo
    {
        public Result<Deposite> Save(Deposite deposite)
        {
            var result = new Result<Deposite>();

            try
            {

                var objToSave = Context.Deposites.FirstOrDefault(u => u.UID == deposite.UID);

                if (objToSave == null)
                {
                    objToSave = new Deposite();
                    Context.Deposites.Add(objToSave);
                }

                if (!IsValidToSave(deposite, result))
                    return result;

                objToSave.DType = deposite.DType;
                objToSave.ChequeNo = deposite.ChequeNo;
                objToSave.RefNum = deposite.RefNum;
                objToSave.ChequeBank = deposite.ChequeBank;
                objToSave.Amount = deposite.Amount;
                objToSave.Date = deposite.Date;
                objToSave.BankId = deposite.BankId;
                objToSave.ReceiptImage = deposite.ReceiptImage;
                objToSave.UserID = deposite.UserID;
                objToSave.AdminVerifyId = deposite.AdminVerifyId;

                Context.SaveChanges();




                result.Data = objToSave;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result<List<Deposite>> GetAllPendingDeposite(string key = "")
        {
            var result = new Result<List<Deposite>>();

            try
            {
                var objToList =
                    Context.Deposites.Where(d => d.AdminVerifyId == (int) EnumCollection.AdminVarifyEnum.NotAccept)
                        .ToList();

                if (ValidationHelper2.IsValidString(key))
                {
                    objToList =
                        objToList.Where(
                            p =>
                                p.ChequeNo.Contains(key) || p.RefNum.Contains(key) || p.ChequeBank.Contains(key))
                            .ToList();
                }

                result.Data = objToList;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result<bool> AcceptByAdmin(int id)
        {
            var result = new Result<bool>();

            try
            {
                var objToAccept = Context.Deposites.FirstOrDefault(a => a.UID == id);

                if (objToAccept == null)
                {
                    result.HasError = true;
                    result.Message = "Oops! Sorry, Invalid ID to Accept";
                    return result;
                }

                objToAccept.AdminVerifyId = (int)EnumCollection.AdminVarifyEnum.Accept;
                objToAccept.UserInfo.Balance += objToAccept.Amount;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        } 

    /*

        public Result<UserInfo> Login(string email, string password)
        {
            var result = new Result<UserInfo>();

            try
            {
                var objToLogin =
                    Context.UserInfos.FirstOrDefault(
                        l =>
                            l.CompanyInfo.CEmail == email && l.Pass == password &&
                            l.AdminVerifyID == (int) EnumCollection.AdminVarifyEnum.Accept);

                if (objToLogin == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid Email or Password or Does no Accepted by ADMIN";
                }

                result.Data = objToLogin;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result<List<UserInfo>> PendingAgent(string key = "")
        {
            var result = new Result<List<UserInfo>>();

            try
            {
                var objToPending =
                    Context.UserInfos.Include("CompanyInfo")
                        .Where(p => p.AdminVerifyID == (int) EnumCollection.AdminVarifyEnum.NotAccept).ToList();

                if (ValidationHelper2.IsValidString(key))
                {
                    objToPending =
                        objToPending.Where(
                            p =>
                                p.FirstName.Contains(key) || p.LastName.Contains(key) || p.UEmail.Contains(key) ||
                                p.CompanyInfo.CEmail.Contains(key) || p.CompanyInfo.CName.Contains(key)).ToList();
                }

                result.Data = objToPending;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }


            return result;
        }

        public Result<UserInfo> GetById(int id)
        {
            var result = new Result<UserInfo>();
            try
            {
                var objToEdit = Context.UserInfos.FirstOrDefault(u => u.UID == id);

                if (objToEdit == null)
                {
                    result.HasError = true;
                    result.Message = "Invalid ID";
                }

                result.Data = objToEdit;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }
            return result;
        }


        public Result<bool> EmailVerify(string email)
        {

           
            var result = new Result<bool>();
            try
            {
                var objToAccept = Context.UserInfos.FirstOrDefault(a => a.UEmail == email);

                if (objToAccept == null)
                {
                    result.HasError = true;
                    result.Message = "Oops! Sorry, Invalid ID to Accept";
                    return result;
                }

                objToAccept.EmailVerifyId = (int)EnumCollection.EmailVarifyEnum.Accept;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;

        }

        public Result<bool> AcceptByAdmin(int id)
        {
            var result = new Result<bool>();

            try
            {
                var objToAccept = Context.UserInfos.FirstOrDefault(a => a.UID == id);

                if (objToAccept == null)
                {
                    result.HasError = true;
                    result.Message = "Oops! Sorry, Invalid ID to Accept";
                    return result;
                }

                objToAccept.AdminVerifyID = (int) EnumCollection.AdminVarifyEnum.Accept;
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        } 

        */

        private bool IsValidToSave(Deposite deposite, Result<Deposite> result)
        {
            if (deposite.Amount < 1000)
            {
                result.HasError = true;
                result.Message = "Should Register Minimum Balance";

                return false;
            }

            return true;
        }
    }
}
