using Models.Profitable;

namespace Models.ProfitableDTO
{
    public class PixDTO
    {
        public int TypeId { get; set; }
        public string PixKey { get; set; }

        public PixDTO(int typeId, string pixKey)
        {
            this.TypeId = typeId;
            this.PixKey = pixKey;
        }
    }
}