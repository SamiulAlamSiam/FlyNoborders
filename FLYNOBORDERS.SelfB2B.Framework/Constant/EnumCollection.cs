using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLYNOBORDERS.SelfB2B.Framework.Object;

namespace FLYNOBORDERS.SelfB2B.Framework.Constant
{
    public class EnumCollection
    {
        public enum UserTypeEnum
        {
            Admin = 1,
            Agent = 2
        }

        public enum AdminVarifyEnum
        {
            Accept = 1,
            NotAccept = 2
        }

        public enum EmailVarifyEnum
        {
            Accept = 1,
            NotAccept = 2
        }

        public enum BankNameEnum
        {
            Dhaka_Bank,
            Dutch_Bangla_Bank,
            Esturn_Bank,
            Pubali_Bank,
            Janata_Bank
        }

        public enum VisaDetailsesEnum
        {
            APos = 1,
            ANeg = 2,
            BPos = 3,
            BNeg = 4,
            ABPos = 5,
            ABNeg = 6,
            OPos = 7,
            ONeg = 8
        }

        public enum TripEnum
        {
            RoundTrip = 1,
            OneWay = 2,
            MultipleWay = 3
        }

        public enum TravleTypeEnum
        {
            Economy = 1,
            Business = 2,
            FirstClass = 3
        }

        public enum PreferredRoeEnum
        {
            USD = 1,
            BDT = 2
        }

        public enum TimeZoneEnum
        {
            GMT_12_Eniwetok_Kwajalein,
            GMT_6_DHAKA
        }

        public static List<EnumDetails> GetEnumList(Type getType)
        {
            var list = new List<EnumDetails>();

            var values = Enum.GetValues(getType);

            int i = 1;

            foreach (var v in values)
            {
                list.Add(new EnumDetails()
                {
                    ID = i,
                    Name = v.ToString()
                });

                i++;
            }

            return list;
        } 
    }
}
