using Models.Profitable;

namespace Models.ProfitableDTO
{
    public class PaymentDTO
    {
        public string CardNumber { get; set; }
        public int FetlockId { get; set; }
        public int PixId { get; set; }
        public DateTime PaymentDate { get; set; }

        public PaymentDTO(string cardNumber, int fetlockId, int pixId, DateTime PaymentDate)
        {
            this.CardNumber = cardNumber;
            this.FetlockId = fetlockId;
            this.PixId = pixId;
            this.PaymentDate = PaymentDate;
        }
    }
}