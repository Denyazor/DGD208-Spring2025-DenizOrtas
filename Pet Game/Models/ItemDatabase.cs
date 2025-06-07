using PetSimulator.Enums;

namespace PetSimulator.Models
{
    public static class ItemDatabase
    {
        public static List<Item> Items { get; private set; }

        static ItemDatabase()
        {
            Items = new List<Item>
            {
                new Item("Dog Food", ItemType.Food, 20, PetStat.Hunger, new List<PetType> { PetType.Dog }),
                new Item("Cat Food", ItemType.Food, 20, PetStat.Hunger, new List<PetType> { PetType.Cat }),
                new Item("Bird Seed", ItemType.Food, 20, PetStat.Hunger, new List<PetType> { PetType.Bird }),
                new Item("Fish Food", ItemType.Food, 20, PetStat.Hunger, new List<PetType> { PetType.Fish }),
                new Item("Rabbit Food", ItemType.Food, 20, PetStat.Hunger, new List<PetType> { PetType.Rabbit }),
                
                new Item("Ball", ItemType.Toy, 15, PetStat.Fun, new List<PetType> { PetType.Dog, PetType.Cat }),
                new Item("Mouse Toy", ItemType.Toy, 15, PetStat.Fun, new List<PetType> { PetType.Cat }),
                new Item("Bird Toy", ItemType.Toy, 15, PetStat.Fun, new List<PetType> { PetType.Bird }),
                new Item("Fish Toy", ItemType.Toy, 15, PetStat.Fun, new List<PetType> { PetType.Fish }),
                new Item("Rabbit Toy", ItemType.Toy, 15, PetStat.Fun, new List<PetType> { PetType.Rabbit }),
                
                new Item("Dog Bed", ItemType.Bed, 25, PetStat.Sleep, new List<PetType> { PetType.Dog }),
                new Item("Cat Bed", ItemType.Bed, 25, PetStat.Sleep, new List<PetType> { PetType.Cat }),
                new Item("Bird Nest", ItemType.Bed, 25, PetStat.Sleep, new List<PetType> { PetType.Bird }),
                new Item("Fish Tank", ItemType.Bed, 25, PetStat.Sleep, new List<PetType> { PetType.Fish }),
                new Item("Rabbit Hutch", ItemType.Bed, 25, PetStat.Sleep, new List<PetType> { PetType.Rabbit }),
                
                new Item("Medicine", ItemType.Medicine, 30, PetStat.Hunger, new List<PetType> { PetType.Dog, PetType.Cat, PetType.Bird, PetType.Fish, PetType.Rabbit })
            };
        }
    }
} 