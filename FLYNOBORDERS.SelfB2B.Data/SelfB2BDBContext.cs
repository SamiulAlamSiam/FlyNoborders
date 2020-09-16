using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLYNOBORDERS.SelfB2B.Entities;

namespace FLYNOBORDERS.SelfB2B.Data
{
    public class SelfB2BDBContext : DbContext
    {
        public SelfB2BDBContext() : base("SelfB2BConnectionString") { }

        public DbSet<CompanyInfo> CompanyInfos { get; set; }

        public DbSet<UserInfo> UserInfos { get; set; }

        public DbSet<Deposite> Deposites { get; set; }

        public DbSet<PackagePicture> PackagePictures { get; set; }

        public DbSet<HotelPicture> HotelPictures { get; set; }
    }
}
