using Models.Profitable;

namespace Models.ProfitableDTO
{
    public class PaymentDTO
    {
        public string CardNumber { get; set; }
        public int FetlockId { get; set; }
        public int PixId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}