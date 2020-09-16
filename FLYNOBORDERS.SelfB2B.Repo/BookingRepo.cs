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
    public class BookingRepo : BaseRepo
    {
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

        public Result<bool> MinusBalance(int id, double price)
        {
            var result = new Result<bool>();

            try
            {
                var objToAccept = Context.UserInfos.FirstOrDefault(a => a.UID == id);

                if (objToAccept == null)
                {
                    result.HasError = true;
                    result.Message = "Oops! Sorry, Invalid ID";
                    return result;
                }

                if (objToAccept.Balance < price)
                {
                    result.HasError = true;
                    result.Message = "Oops! Invalid BALANCE";
                    return result;
                }

                objToAccept.Balance -= price;
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
