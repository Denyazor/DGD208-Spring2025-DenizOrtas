using PetGameBeta.Enums;

namespace PetGameBeta.Models
{
    public class Item
    {
        public string Name { get; set; }
        public ItemType Type { get; set; }
        public int StatIncrease { get; set; }
        public PetStat AffectedStat { get; set; }
        public List<PetType> CompatiblePets { get; set; }

        public Item(string name, ItemType type, int statIncrease, PetStat affectedStat, List<PetType> compatiblePets)
        {
            Name = name;
            Type = type;
            StatIncrease = statIncrease;
            AffectedStat = affectedStat;
            CompatiblePets = compatiblePets;
        }
    }
} 