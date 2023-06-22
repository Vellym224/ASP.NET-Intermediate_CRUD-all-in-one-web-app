namespace ITSAIntermediate_VelaphiMhlanga.Models.Domain
{
    public class Address
    {

        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public int UnitNumber { get; set; }
        public string ComplexName { get; set; }
    }
}
