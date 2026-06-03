namespace MyVaccine.WebApi.Models
{
    public class VaccineCategory
    {
        public int VaccineCategoryId { get; set; }

        public string Name { get; set; }

        public List<Vaccine> Vaccines { get; set; }
    }
}