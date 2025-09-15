using Fighters.Models;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.Services;

namespace Fighters.Controllers
{
    public class GameController
    {
        private readonly GameManager _gameManager;
        private readonly Dictionary<string, IRace> _races;
        private readonly Dictionary<string, IWeapon> _weapons;
        private readonly Dictionary<string, IArmor> _armors;
        private readonly Dictionary<string, FighterType> _fighterClasses;

        public GameController()
        {
            _gameManager = new GameManager();

            _races = new Dictionary<string, IRace>
            {
                { "0", new Human() },
                { "1", new Elf() },
                { "2", new Orc() }
            };

            _weapons = new Dictionary<string, IWeapon>
            {
                { "0", new Firsts() },
                { "1", new Sword() },
                { "2", new Axe() },
                { "3", new Bow() }
            };

            _armors = new Dictionary<string, IArmor>
            {
                { "0", new NoArmor() },
                { "1", new ClothArmor() },
                { "2", new LeatherArmor() },
                { "3", new ChainMail() }
            };

            _fighterClasses = new Dictionary<string, FighterType>
            {
                { "0", FighterType.Knight },
                { "1", FighterType.Mercenary }
            };
        }

        public void ProcessCommand( string command )
        {
            switch ( command.ToLower().Trim() )
            {
                case "add-fighter":
                    AddFighter();
                    break;
                case "play":
                    _gameManager.StartBattle();
                    break;
                case "reset":
                    _gameManager.ResetBattle();
                    Console.WriteLine( "Битва сброшена!" );
                    break;
                case "clear":
                    _gameManager.ClearFighters();
                    Console.WriteLine( "Все бойцы удалены!" );
                    break;
                case "list":
                    ListFighters();
                    break;
                case "help":
                    ShowHelp();
                    break;
                default:
                    Console.WriteLine( "Неизвестная команда. Введите 'help' для справки." );
                    break;
            }
        }

        private void AddFighter()
        {
            Console.WriteLine( "Введите имя персонажа:" );
            string? name = Console.ReadLine()?.Trim();

            if ( string.IsNullOrEmpty( name ) )
            {
                Console.WriteLine( "Имя не может быть пустым!" );
                return;
            }

            FighterType? fighterClass = SelectEnumOption( "Выберите класс персонажа:", _fighterClasses,
                new[] { "Рыцарь", "Наемник" } );

            if ( fighterClass == null ) return;


            IRace? race = SelectOption( "Выберите расу:", _races,
                new[] { "Человек", "Эльф", "Орк" } );
            if ( race == null ) return;

            IWeapon? weapon = SelectOption( "Выберите оружие:", _weapons,
                new[] { "Без оружия", "Меч", "Топор", "Лук" } );
            if ( weapon == null ) return;

            IArmor? armor = SelectOption( "Выберите броню:", _armors,
                new[] { "Без одежды", "Простая одежда", "Кожаная броня", "Кольчуга" } );
            if ( armor == null ) return;

            IFighter fighter = CreateFighter( fighterClass.Value, name, race );
            fighter.SetWeapon( weapon );
            fighter.SetArmor( armor );

            _gameManager.AddFighter( fighter );
            Console.WriteLine( $"Боец {name} добавлен!" );
        }

        private T? SelectOption<T>( string prompt, Dictionary<string, T> options, string[] optionNames )
            where T : class
        {
            Console.WriteLine( prompt );

            for ( int i = 0; i < optionNames.Length; i++ )
            {
                Console.WriteLine( $"{i} - {optionNames[ i ]}" );
            }

            string? input = Console.ReadLine()?.Trim();

            if ( string.IsNullOrEmpty( input ) || !options.ContainsKey( input ) )
            {
                Console.WriteLine( "Неверный выбор!" );
                return null;
            }

            return options[ input ];
        }

        private T? SelectEnumOption<T>( string prompt, Dictionary<string, T> options, string[] optionNames )
            where T : struct, Enum
        {
            Console.WriteLine( prompt );

            for ( int i = 0; i < optionNames.Length; i++ )
            {
                Console.WriteLine( $"{i} - {optionNames[ i ]}" );
            }

            string? input = Console.ReadLine()?.Trim();

            if ( string.IsNullOrEmpty( input ) || !options.ContainsKey( input ) )
            {
                Console.WriteLine( "Неверный выбор!" );
                return null;
            }

            return options[ input ];
        }

        private IFighter CreateFighter( FighterType fighterClass, string name, IRace race )
        {
            return fighterClass switch
            {
                FighterType.Knight => new Knight( name, race ),
                FighterType.Mercenary => new Mercenary( name, race ),
                _ => throw new ArgumentException( "Неизвестный класс бойца" )
            };
        }

        private void ListFighters()
        {
            List<IFighter> fighters = _gameManager.GetFighters();

            if ( fighters.Count == 0 )
            {
                Console.WriteLine( "Нет добавленных бойцов." );
                return;
            }

            Console.WriteLine( "Список бойцов:" );
            for ( int i = 0; i < fighters.Count; i++ )
            {
                IFighter fighter = fighters[ i ];
                Console.WriteLine(
                    $"{i + 1}. {fighter.Name} - Здоровье: {fighter.GetCurrentHealth()}/{fighter.GetMaxHealth()}" );
            }
        }

        private void ShowHelp()
        {
            Console.WriteLine( """
                               Доступные команды:
                               add-fighter - Добавить нового бойца на арену
                               play - Начать битву
                               reset - Сбросить битву (восстановить здоровье)
                               clear - Удалить всех бойцов
                               list - Показать список бойцов
                               help - Показать эту справку
                               exit - Выйти из игры
                               """ );
        }

        public void ShowWelcome()
        {
            Console.WriteLine( "Добро пожаловать в игру Fighter Game!" );
            Console.WriteLine( "Введите команду:" );
            ShowHelp();
        }
    }
}