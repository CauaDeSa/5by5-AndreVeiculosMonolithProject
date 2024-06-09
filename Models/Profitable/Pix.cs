using Models.ProfitableDTO;

namespace Models.Profitable;

public class Pix
{
    public static readonly string Select = "SELECT * FROM Pix WHERE Id = @pixId";
    public static readonly string Insert = "INSERT INTO Pix (TypeId, PixKey) OUTPUT INSERTED.Id VALUES (@TypeId, @PixKey)";

    public int Id { get; set; }
    public PixType Type { get; set; }
    public string PixKey { get; set; }

    public Pix() { }

    public Pix(PixDTO pixDTO)
    {
        this.Type = new() { Id = pixDTO.TypeId };
        this.PixKey = pixDTO.PixKey;
    }
}