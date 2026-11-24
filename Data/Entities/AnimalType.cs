using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LapichkaShop.Data.Entities
{
    public class AnimalType
    {
        public AnimalType()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public ICollection<Animal> Animals { get; set; }
    }
}
