using Models.Profitable;

namespace Models.ProfitableDTO
{
    public class PixDTO
    {
        public PixType Type { get; set; }
        public string PixKey { get; set; }

        public PixDTO(int typeId, string pixKey)
        {
            this.Type = new() { Id = typeId };
            this.PixKey = pixKey;
        }
    }
}