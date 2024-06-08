using Models.ProfitableDTO;

namespace Models.Profitable;

public class Pix
{
    public int Id { get; set; }
    public PixType Type { get; set; }
    public string PixKey { get; set; }

    public Pix() { }

    public Pix(PixDTO pixDTO)
    {
        this.Type = pixDTO.Type;
        this.PixKey = pixDTO.PixKey;
    }
}