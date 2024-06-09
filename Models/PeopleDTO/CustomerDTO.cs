using Models.People;

namespace Models.PeopleDTO
{
    public class CustomerDTO
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int AddressId { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public double Income { get; set; }
        public string DocumentPDF { get; set; }
    }
}