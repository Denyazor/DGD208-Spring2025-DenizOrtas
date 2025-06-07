using PetSimulator.Enums;
using PetSimulator.Models;
using PetSimulator.UI;
using System.Collections.Concurrent;

namespace PetSimulator
{
    public class Game
    {
        private List<Pet> pets;
        private Menu mainMenu;
        private bool isRunning;
        private ConcurrentDictionary<Pet, Task> petTasks;

        public Game()
        {
            pets = new List<Pet>();
            petTasks = new ConcurrentDictionary<Pet, Task>();
            mainMenu = new Menu("Pet Simulator", new List<string>
            {
                "Adopt a Pet",
                "View Pets",
                "Use Item on Pet",
                "Display Creator Info",
                "Exit"
            });
        }

        public async Task Run()
        {
            isRunning = true;
            while (isRunning)
            {
                int choice = mainMenu.Display();
                await HandleMenuChoice(choice);
            }
        }

        private async Task HandleMenuChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    await AdoptPet();
                    break;
                case 2:
                    DisplayPets();
                    break;
                case 3:
                    await UseItemOnPet();
                    break;
                case 4:
                    DisplayCreatorInfo();
                    break;
                case 5:
                    isRunning = false;
                    break;
            }
        }

        private async Task AdoptPet()
        {
            var petTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
            var petMenu = new Menu("Choose Pet Type", petTypes.Select(p => p.ToString()).ToList());
            int choice = petMenu.Display();
            PetType selectedType = petTypes[choice - 1];

            Console.Write("Enter pet name: ");
            string name = Console.ReadLine() ?? "Unnamed";

            var pet = new Pet(name, selectedType);
            pet.PetDied += OnPetDied;
            pet.StatChanged += OnPetStatChanged;
            pets.Add(pet);

            Console.WriteLine($"\nCongratulations! You've adopted a {selectedType} named {name}!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void DisplayPets()
        {
            if (pets.Count == 0)
            {
                Console.WriteLine("\nYou don't have any pets yet!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nYour Pets:");
            foreach (var pet in pets)
            {
                Console.WriteLine($"\n{pet.Name} ({pet.Type})");
                foreach (var stat in pet.Stats)
                {
                    Console.WriteLine($"{stat.Key}: {stat.Value}");
                }
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private async Task UseItemOnPet()
        {
            if (pets.Count == 0)
            {
                Console.WriteLine("\nYou don't have any pets to use items on!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            var petMenu = new Menu("Select Pet", pets.Select(p => p.Name).ToList());
            int petChoice = petMenu.Display();
            var selectedPet = pets[petChoice - 1];

            var compatibleItems = ItemDatabase.Items.Where(i => i.CompatiblePets.Contains(selectedPet.Type)).ToList();
            var itemMenu = new Menu("Select Item", compatibleItems.Select(i => i.Name).ToList());
            int itemChoice = itemMenu.Display();
            var selectedItem = compatibleItems[itemChoice - 1];

            try
            {
                selectedPet.UseItem(selectedItem);
                Console.WriteLine($"\nUsed {selectedItem.Name} on {selectedPet.Name}!");
                Console.WriteLine($"Increased {selectedItem.AffectedStat} by {selectedItem.StatIncrease}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void DisplayCreatorInfo()
        {
            Console.WriteLine("\nProject Creator: Deniz Ortaş");
            Console.WriteLine("Student Number: 2305041061");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void OnPetDied(object? sender, EventArgs e)
        {
            if (sender is Pet pet)
            {
                Console.WriteLine($"\nOh no! {pet.Name} has died!");
                pet.StopTimer();
                pets.Remove(pet);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private void OnPetStatChanged(object? sender, PetStat stat)
        {
            if (sender is Pet pet)
            {
                Console.WriteLine($"\n{pet.Name}'s {stat} is now {pet.Stats[stat]}");
            }
        }
    }
} 