namespace MyVaccine.WebApi.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Dependent> Dependents { get; set; }

        public List<FamilyGroup> FamilyGroups { get; set; }

        public List<VaccineRecord> VaccineRecords { get; set; }

        public List<Allergy> Allergies { get; set; }
    }
}