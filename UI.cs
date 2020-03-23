using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_BoardGames
{
    class UI
    {
        int NumOfPlayers()
        {
            Console.WriteLine("How many players?");
            return Convert.ToInt16(Console.ReadLine());
        }
        void MonopolyGame()
        {
            Monopoly monopoly = new Monopoly(NumOfPlayers());           
        }
        void AliasGame()
        {
            Alias alias = new Alias(NumOfPlayers());
        }
        void MafiaGame()
        {
            Mafia mafia = new Mafia(NumOfPlayers());
        }
        void ShowMenu()
        {
            Console.WriteLine("Choose one game:\n" +
                "1. Monopoly\n" +
                "2. Mafia\n" +
                "3. Alias\n"+
                "4. Exit");
        }
        public void Menu()
        {
            int exit = 0;
            do
            {
                ShowMenu();
                int check = Convert.ToInt16(Console.ReadLine());
                switch (check)
                {
                    case 1:
                        MonopolyGame();
                        break;
                    case 2:
                        MafiaGame();
                        break;
                    case 3:
                        AliasGame();
                        break;
                    case 4:
                        {
                            exit = 4;
                            break;
                        }

                    default:
                        {
                            ErrorOutOfMenu();
                            break;
                        }
                }
            } while (exit!=4);           
        }
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.Menu();
            Console.ReadKey();
        }
        void ErrorOutOfMenu()
        {
            Console.WriteLine("Wrong choise. Try again.");
        }
    }
}
