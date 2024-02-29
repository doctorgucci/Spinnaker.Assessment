namespace Spinnaker.Assessment.UI.Models
{
    public class Customers
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber
        {
            get; set;
        }
        public int CountryId { get; set; }

        public Country Country { get; set; }
    }
}
