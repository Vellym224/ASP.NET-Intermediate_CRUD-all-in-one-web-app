namespace ITSAIntermediate_VelaphiMhlanga.Models.Domain
{
    public class CompanyDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public long ContactNumber { get; set; }
        public int RegistrationNumber { get; set; }
        public string Address { get; set; }
        public string BusinessType { get; set; }
    }
}
