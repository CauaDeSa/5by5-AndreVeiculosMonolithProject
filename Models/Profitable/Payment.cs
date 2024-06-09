using Models.ProfitableDTO;

namespace Models.Profitable;

public class Payment
{
    public static readonly string SelectAll = "SELECT * FROM Payment";
    public static readonly string Select = "SELECT * FROM Payment WHERE Id = @id";
    public static readonly string Insert = "INSERT INTO Payment (CardNumber, FetlockId, PixId, PaymentDate) OUTPUT INSERTED.Id VALUES (@CardNumber, @FetlockId, @PixId, @PaymentDate)";

    public int Id { get; set; }
    public Card Card { get; set; }
    public Fetlock Fetlock { get; set; }
    public Pix Pix { get; set; }
    public DateTime PaymentDate { get; set; }

    public Payment() { }

    public Payment(PaymentDTO paymentDTO)
    {
        this.Card = new() { CardNumber = paymentDTO.CardNumber };
        this.Fetlock = new() { Id = paymentDTO.FetlockId };
        this.Pix = new() { Id = paymentDTO.PixId };
        this.PaymentDate = paymentDTO.PaymentDate;
    }
}