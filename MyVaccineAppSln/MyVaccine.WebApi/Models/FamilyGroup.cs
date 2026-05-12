namespace MyVaccine.WebApi.Models
{
    public class FamilyGroup
    {
        public int FamilyGroupId { get; set; }

        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
