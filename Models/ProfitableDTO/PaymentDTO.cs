using Models.Profitable;

namespace Models.ProfitableDTO
{
    public class PaymentDTO
    {
        public Card Card { get; set; }
        public Fetlock Fetlock { get; set; }
        public Pix Pix { get; set; }
        public DateTime PaymentDate { get; set; }

        public PaymentDTO(string cardNumber, int fetlockId, int pixId, DateTime PaymentDate)
        {
            this.Card = new() { CardNumber = cardNumber };
            this.Fetlock = new() { Id = fetlockId };
            this.Pix = new() { Id = pixId };
            this.PaymentDate = PaymentDate;
        }
    }
}