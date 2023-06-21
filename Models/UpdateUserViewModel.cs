namespace ITSAIntermediate_VelaphiMhlanga.Models
{
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long IdentityNumber { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public long ContactNumber { get; set; }
    }
}