namespace RG.Accounts.Model
{
    public class InterestRates
    {
        public int Id { get; set; }
        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }
        public decimal Rate { get; set; }
    }
}
