using Models.People;

namespace Models.PeopleDTO
{
    public class CustomerDTO
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public decimal Income { get; set; }
        public string DocumentPDF { get; set; }

        public CustomerDTO(string document, string name, DateTime birthDate, int addressId, string telephone, string email, decimal income, string documentPDF)
        {
            Document = document;
            Name = name;
            BirthDate = birthDate;
            Address = new() { Id = addressId };
            Telephone = telephone;
            Email = email;
            Income = income;
            DocumentPDF = documentPDF;
        }
    }
}