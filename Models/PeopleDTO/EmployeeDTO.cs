using Models.People;

namespace Models.PeopleDTO
{
    public class EmployeeDTO
    {
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int AddressId { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public int FunctionId { get; set; }
        public double ComissionAmount { get; set; }
        public double Comission { get; set; }
    }
}