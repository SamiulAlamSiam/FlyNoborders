using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using FLYNOBORDERS.SelfB2B.Entities;
using FLYNOBORDERS.SelfB2B.Framework.Constant;
using FLYNOBORDERS.SelfB2B.Web.Framework.Attributes;
using FLYNOBORDERS.SelfB2B.Web.Framework.Bases;

namespace FLYNOBORDERS.SelfB2B.Web.Controllers
{
    [SELFB2BAuthorize(EnumCollection.UserTypeEnum.Admin)]
    public class HotelController : BaseController
    {
        // GET: Package

        public ActionResult Index()
        {
            return View(Context.HotelPictures.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase file, HotelPicture emp)
        {
            
            string fileName = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssff") + fileName;

            string extension = Path.GetExtension(file.FileName);

            string path = Path.Combine(Server.MapPath("~/images/"), _filename);

            emp.Image = "~/images/" + _filename;


            /*if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                
            }
            else
            {
                ViewBag.msg = "Invalid Type";
            }  */

            /*if (file.ContentLength <= 100000)
            {
                
            }
            else
            {
                ViewBag.msg = "Image file should be equal or less than 1mb";
            }*/

            Context.HotelPictures.Add(emp);
            if (Context.SaveChanges() > 0)
            {
                file.SaveAs(path);
                ViewBag.msg = "Congratulations! HOTEL Added Successfully";
                ModelState.Clear();
            }
            return View();
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var p = Context.HotelPictures.Find(id);

            if (p == null)
            {
                return HttpNotFound();
            }
            Session["imgPath"] = p.Image;

            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase file, HotelPicture emp)
        {
            //if (ModelState.IsValid)
            //{
            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                string _filename = DateTime.Now.ToString("yymmssff") + fileName;

                string extension = Path.GetExtension(file.FileName);

                string path = Path.Combine(Server.MapPath("~/images/"), _filename);

                emp.Image = "~/images/" + _filename;


                /*if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                {
                
                }
                else
                {
                    ViewBag.msg = "Invalid Type";
                }  */

                /*if (file.ContentLength <= 100000)
                {
                
                }
                else
                {
                    ViewBag.msg = "Image file should be equal or less than 1mb";
                } */

                Context.Entry(emp).State = EntityState.Modified;

                string oldImagePath = Request.MapPath(Session["imgPath"].ToString());

                if (Context.SaveChanges() > 0)
                {
                    file.SaveAs(path);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    ViewBag.msg = "Hotel Updated Successfully";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                emp.Image = Session["imgPath"].ToString();
                Context.Entry(emp).State = EntityState.Modified;

                if (Context.SaveChanges() > 0)
                {
                    TempData["msg"] = "Data Updated";
                    return RedirectToAction("Index");
                }
            }
            // }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var p = Context.HotelPictures.Find(id);

            if (p == null)
            {
                return HttpNotFound();
            }

            if (MessageBox.Show("Are You Sure ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No)
                return RedirectToAction("Index");

            string currentImage = Request.MapPath(p.Image);
            Context.Entry(p).State = EntityState.Deleted;

            if (Context.SaveChanges() > 0)
            {
                if (System.IO.File.Exists(currentImage))
                {
                    System.IO.File.Delete(currentImage);
                }
                ViewBag.msg = "Hotel Successfully Deleted";
                return RedirectToAction("Index");
            }
            return View(p);
        }
    }
}