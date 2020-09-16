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
    public class PackagePictureRepo : BaseRepo
    {
        public Result<PackagePicture> Save(PackagePicture packagePicture)
        {
            var result = new Result<PackagePicture>();

            try
            {

                var objToSave = Context.PackagePictures.FirstOrDefault(u => u.PID == packagePicture.PID);

                if (objToSave == null)
                {
                    objToSave = new PackagePicture();
                    Context.PackagePictures.Add(objToSave);
                }

                if (!IsValidToSave(packagePicture, result))
                    return result;

                objToSave.PName = packagePicture.PName;
                objToSave.Image = packagePicture.Image;
                objToSave.Price = packagePicture.Price;

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

        public Result<List<PackagePicture>> GetAll(string key = "")
        {
            var result = new Result<List<PackagePicture>>();

            try
            {
                var objToAll = Context.PackagePictures.ToList();

                if (ValidationHelper2.IsValidString(key))
                    objToAll = objToAll.Where(p => p.PName.Contains(key)).ToList();

                result.Data = objToAll;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Message = ex.Message;
            }

            return result;
        } 

        private bool IsValidToSave(PackagePicture deposite, Result<PackagePicture> result)
        {
            if (deposite.Price < 1000)
            {
                result.HasError = true;
                result.Message = "Please Enter Minimum Balance";

                return false;
            }

            return true;
        }
    }
}
