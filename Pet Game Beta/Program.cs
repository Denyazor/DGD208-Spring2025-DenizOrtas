using PetGameBeta;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Pet Game Beta";
        var game = new Game();
        await game.Run();
    }
}
