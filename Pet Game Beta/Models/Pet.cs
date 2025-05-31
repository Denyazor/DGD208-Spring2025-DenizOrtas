using PetSimulator.Enums;

namespace PetSimulator.Models
{
    public class Pet
    {
        public string Name { get; private set; }
        public PetType Type { get; private set; }
        public Dictionary<PetStat, int> Stats { get; private set; }
        private System.Timers.Timer statDecreaseTimer;
        public event EventHandler<PetStat>? StatChanged;
        public event EventHandler? PetDied;
        private Dictionary<PetStat, int> lastNotifiedValues;

        public Pet(string name, PetType type)
        {
            Name = name;
            Type = type;
            Stats = new Dictionary<PetStat, int>
            {
                { PetStat.Hunger, 50 },
                { PetStat.Sleep, 50 },
                { PetStat.Fun, 50 }
            };
            lastNotifiedValues = new Dictionary<PetStat, int>(Stats);

            InitializeStatDecreaseTimer();
        }

        private void InitializeStatDecreaseTimer()
        {
            statDecreaseTimer = new System.Timers.Timer(3000); // Decrease stats every 3 seconds
            statDecreaseTimer.Elapsed += OnStatDecreaseTimerElapsed;
            statDecreaseTimer.Start();
        }

        private void OnStatDecreaseTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            foreach (var stat in Stats.Keys.ToList())
            {
                Stats[stat] = Math.Max(0, Stats[stat] - 1);
                
                // Notify only if:
                // 1. The value is below 20 (critical)
                // 2. The value has changed by 10 or more since last notification
                if (Stats[stat] <= 20 || Math.Abs(Stats[stat] - lastNotifiedValues[stat]) >= 10)
                {
                    StatChanged?.Invoke(this, stat);
                    lastNotifiedValues[stat] = Stats[stat];
                }

                if (Stats[stat] == 0)
                {
                    PetDied?.Invoke(this, EventArgs.Empty);
                    statDecreaseTimer.Stop();
                    return;
                }
            }
        }

        public void UseItem(Item item)
        {
            if (!item.CompatiblePets.Contains(Type))
            {
                throw new InvalidOperationException($"This item is not compatible with {Type} pets.");
            }

            Stats[item.AffectedStat] = Math.Min(100, Stats[item.AffectedStat] + item.StatIncrease);
            StatChanged?.Invoke(this, item.AffectedStat);
            lastNotifiedValues[item.AffectedStat] = Stats[item.AffectedStat];
        }

        public void StopTimer()
        {
            statDecreaseTimer.Stop();
            statDecreaseTimer.Dispose();
        }
    }
} 