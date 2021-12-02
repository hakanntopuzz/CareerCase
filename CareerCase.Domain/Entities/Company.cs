namespace CareerCase.Domain.Entities
{
    public class Company : EntityBase<int>
    {
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int JobLimit { get; set; }
    }
}