using System;

namespace RG.Accounts.Model
{
    public class UserSocietyAccount
    {
        public int Id { get; set; }
        public User User { get; set; }
        public decimal AccountBalance { get; set; }
        public DateTime AccountDepositDate { get; set; }
        public decimal InterestForThisPeriod { get; set; }
        public decimal NumberofYears { get; set; }

    }
}
