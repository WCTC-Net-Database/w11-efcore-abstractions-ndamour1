using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private readonly GameContext _context;
    private readonly MenuManager _menuManager;
    private readonly OutputManager _outputManager;

    private IPlayer _player;
    private IMonster _goblin;

    public GameEngine(GameContext context, MenuManager menuManager, OutputManager outputManager)
    {
        _menuManager = menuManager;
        _outputManager = outputManager;
        _context = context;
    }

    public void Run()
    {
        if (_menuManager.ShowMainMenu())
        {
            SetupGame();
        }
    }

    private void GameLoop()
    {
        _outputManager.Clear();
        int restorePlayerHealth = _context.Players.OfType<Player>().FirstOrDefault().Health;
        int restoreGoblinHealth = _context.Monsters.OfType<Goblin>().FirstOrDefault().Health;

        while (true)
        {
            if (_player.Health > 0 && _goblin.Health > 0)
            {
                _outputManager.WriteLine("Choose an action:", ConsoleColor.Cyan);
                _outputManager.WriteLine("1. Attack");
                _outputManager.WriteLine("2. Quit");

                _outputManager.Display();

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AttackCharacter();
                        break;
                    case "2":
                        _outputManager.WriteLine("Exiting game...", ConsoleColor.Red);
                        _outputManager.Display();
                        Environment.Exit(0);
                        break;
                    default:
                        _outputManager.WriteLine("Invalid selection. Please choose 1.", ConsoleColor.Red);
                        break;
                }
            }
            else
            {
                if (_player.Health > 0 && _goblin.Health <= 0)
                {
                    _outputManager.WriteLine($"{_player.Name} wins!", ConsoleColor.Yellow);
                }
                else if (_player.Health <= 0 && _goblin.Health > 0)
                {
                    _outputManager.WriteLine($"{_player.Name} has fallen in battle.", ConsoleColor.Gray);
                }

                _outputManager.WriteLine($"Would you like to play again?");
                _outputManager.WriteLine("1. Yes");
                _outputManager.WriteLine("2. No");
                _outputManager.Display();

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        _outputManager.Clear();
                        _player.Health = restorePlayerHealth;
                        _goblin.Health = restoreGoblinHealth;
                        break;
                    case "2":
                        _outputManager.WriteLine("Exiting game...", ConsoleColor.Red);
                        _outputManager.Display();
                        Environment.Exit(0);
                        break;
                    default:
                        _outputManager.WriteLine("Invalid selection. Please choose a valid option.", ConsoleColor.Red);
                        break;
                }
            }
        }
    }

    private void AttackCharacter()
    {
        _outputManager.Clear();
        if (_goblin is ITargetable targetableGoblin && targetableGoblin.Health > 0)
        {
            _player.Attack(targetableGoblin);
            if (targetableGoblin.Health > 0)
            {
                _player.UseAbility(_player.Abilities.First(), targetableGoblin);
                if (_player is ITargetable targetablePlayer && _player.Health > 0)
                {
                    _goblin.Attack(targetablePlayer);
                }
            }
        }
    }

    private void SetupGame()
    {
        _player = _context.Players.OfType<Player>().FirstOrDefault();
        _outputManager.WriteLine($"{_player.Name} has entered the game.", ConsoleColor.Green);

        // Load monsters into random rooms 
        LoadMonsters();

        // Pause before starting the game loop
        Thread.Sleep(500);
        GameLoop();
    }

    private void LoadMonsters()
    {
        _goblin = _context.Monsters.OfType<Goblin>().FirstOrDefault();
    }
}
