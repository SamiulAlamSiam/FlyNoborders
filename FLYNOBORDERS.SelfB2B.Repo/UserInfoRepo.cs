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
    public class UserInfoRepo : BaseRepo
    {
        public Result<UserInfo> Save(UserInfo userInfo)
        {
            var result = new Result<UserInfo>();

            try
            {
                
                var objToSave2 = Context.CompanyInfos.FirstOrDefault(u => u.CID == userInfo.CompanyID);

                if (objToSave2 == null)
                {
                    objToSave2 = new CompanyInfo();
                    Context.CompanyInfos.Add(objToSave2);                  
                }

                if (!IsValidToSave(userInfo, result))
                    return result;

                objToSave2.CName = userInfo.CompanyInfo.CName;
                objToSave2.TradeLicenseNo = userInfo.CompanyInfo.TradeLicenseNo;
                
                objToSave2.Website = userInfo.CompanyInfo.Website;
                objToSave2.CEmail = userInfo.CompanyInfo.CEmail;
                objToSave2.PhnNumber = userInfo.CompanyInfo.PhnNumber;
                objToSave2.Address = userInfo.CompanyInfo.Address;
                objToSave2.City = userInfo.CompanyInfo.City;
                objToSave2.Zip = userInfo.CompanyInfo.Zip;
                

                Context.SaveChanges();

                
                var objToSave1 = Context.UserInfos.FirstOrDefault(u => u.UID == userInfo.UID);

                if (objToSave1 == null)
                {
                    objToSave1 = new UserInfo();
                    Context.UserInfos.Add(objToSave1);
                }

                objToSave1.FirstName = userInfo.FirstName;
                objToSave1.LastName = userInfo.LastName;
                objToSave1.UEmail = userInfo.UEmail;
                objToSave1.Pass = userInfo.Pass;
                objToSave1.TimeZone = userInfo.TimeZone;
                objToSave1.PreferredRoe = userInfo.PreferredRoe;
                objToSave1.UserTypeID = userInfo.UserTypeID;
                objToSave1.AdminVerifyID = userInfo.AdminVerifyID;
                objToSave1.CompanyID = objToSave2.CID;

                Context.SaveChanges();

                result.Data = Context.UserInfos.Include("CompanyInfo").FirstOrDefault(u => u.UID == userInfo.UID);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        }

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


        public Result<UserInfo> ForgotPassword(string email)
        {


            var result = new Result<UserInfo>();
            try
            {
                var objToAccept = Context.UserInfos.FirstOrDefault(a => a.UEmail == email);

                if (objToAccept == null)
                {
                    result.HasError = true;
                    result.Message = "Oops! Sorry, Invalid Email to Accept";
                    return result;
                }

                result.Data = objToAccept;

                //objToAccept.EmailVerifyId = (int)EnumCollection.EmailVarifyEnum.Accept;
                //Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;

        }



        private bool IsValidToSave(UserInfo userInfo, Result<UserInfo> result)
        {
            if (userInfo.Pass.Length < 6)
            {
                result.HasError = true;
                result.Message = "Password Length Should be 6 Character Long";

                return false;
            }

            if (Context.UserInfos.Any(u => u.UEmail == userInfo.UEmail && u.UID != userInfo.UID))
            {
                result.HasError = true;
                result.Message = "Email Already Exits";

                return false;
            }
            return true;
        }
    }
}
