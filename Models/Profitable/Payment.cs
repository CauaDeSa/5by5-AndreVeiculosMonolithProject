using Models.ProfitableDTO;

namespace Models.Profitable;

public class Payment
{
    public int Id { get; set; }
    public Card Card { get; set; }
    public Fetlock Fetlock { get; set; }
    public Pix Pix { get; set; }
    public DateTime PaymentDate { get; set; }

    public Payment() { }

    public Payment(PaymentDTO paymentDTO)
    {
        this.Card = paymentDTO.Card;
        this.Fetlock = paymentDTO.Fetlock;
        this.Pix = paymentDTO.Pix;
        this.PaymentDate = paymentDTO.PaymentDate;
    }
}