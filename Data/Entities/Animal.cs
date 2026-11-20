using LapichkaShop.Data.Constants;

namespace LapichkaShop.Data.Entities
{
    public class Animal
    {
        public Animal()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string TypeId { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }
        public AnimalSex Sex { get; set; }
        public string Description { get; set; }
        public string Image_Path { get; set; }
        public AnimalStatus Status { get; set; }
        public ICollection<Adoption> AdoptionRequests { get; set; }
        public AnimalType AnimalType { get; set; }
        public ICollection<VetProcedure> VetProcedures { get; set; }
        public bool IsDeleted { get; set; }


    }
}
