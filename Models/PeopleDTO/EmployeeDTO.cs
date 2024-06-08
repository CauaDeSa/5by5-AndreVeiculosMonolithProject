using Models.People;

namespace Models.PeopleDTO
{
    public class EmployeeDTO
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public JobTitle Function { get; set; }
        public Decimal ComissionAmount { get; set; }
        public Decimal Comission { get; set; }

        public EmployeeDTO(string document, string name, DateTime birthDate, int addressId, string telephone, string email, int functionId, Decimal comissionAmount, Decimal comission)
        {
            Document = document;
            Name = name;
            BirthDate = birthDate;
            Address = new() { Id = addressId };
            Telephone = telephone;
            Email = email;
            Function = new() { Id = functionId};
            ComissionAmount = comissionAmount;
            Comission = comission;
        }
    }
}