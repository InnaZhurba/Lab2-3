using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_BoardGames
{
    class Mafia : BoardGame
    {
        bool Speaker = false;
        int NumOfMafia = 2;
        int NumOfSimple = 4;
        public Mafia(int numofplayers)
        {
            MinPlayers = 6;
            MaxPlayers = 16;
            NumOfCards = numofplayers - 1;
            Speaker = true;
            Board = false;
            NumOfDices = 0;            
            CorrectNumOfPlayers(numofplayers);
        }
        void CorrectNumOfPlayers(int numofplayers)
        {
            if ((numofplayers >= MinPlayers) && (numofplayers <= MaxPlayers))
            {
                if (CheckingBeforeTheGame())
                    StartGame();
                else
                    Console.WriteLine("We haven`t enough resources.");
            }
            else
                Console.WriteLine("There schould be more than "+MinPlayers+" and less than "+MaxPlayers+". Try again.");
        }
        bool CheckingBeforeTheGame()
        {
            return (Speaker==true && Board==false && NumOfDices==0) ? true:false;
        }
        void StartGame()
        {
            int[] MafiaTeam = new int[NumOfMafia];//[NumOfCards/2];
            int[] SimpleTeam = new int[NumOfSimple];//[NumOfCards-MafiaTeam.Length];
            AddingPlayersToTeams(MafiaTeam,SimpleTeam);
            Prosess(MafiaTeam, SimpleTeam);
        }
        int SumArr(int[] Team)
        {
            int sum = 0;
            for (int i = 0; i<Team.Length; i++)
                sum += Team[i];
            return sum;
        }
        void Prosess(int[] MafiaTeam, int[] SimpleTeam)
        {
            int mafiateam = SumArr(MafiaTeam);
            int simpleteam = SumArr(SimpleTeam);
            Console.WriteLine("Mafia - " + mafiateam + "; simple - " + simpleteam);
            do
            {
                int rand = 0;
                int num = NumOfCards;
                int i = 0;
                do
                {
                    rand = RandNum();
                    if (simpleteam != 0)
                    {
                        //rand = RandNum();
                        i++;
                        if (i == num + 1 || i == rand)
                        {
                            simpleteam--;
                            break;
                        }
                    }
                    if (mafiateam != 0)
                    {
                        i++;
                        if (i == num + 1 || i == rand)
                        {
                            mafiateam--;
                            break;
                        }
                    }
                } while (i != num+1);
                Console.WriteLine("Mafia - " + mafiateam + "; simple - " + simpleteam);
                if ((mafiateam == 0) || (simpleteam == 0))
                    break;
            } while ((mafiateam!=0)||(simpleteam!=0));
            Subject sub;
            if (simpleteam == 0)
            {
                Console.WriteLine("Mafia team is the winer!!!");
                sub = new Subject(1);
            }
            else
            {
                Console.WriteLine("Simple team is the winer!!!");
                sub = new Subject(2);
            }
            ObserverMafia ob = new ObserverMafia();
            sub.Attach(ob);
            sub.SomeBusinessLogic();
        }
        void AddingPlayersToTeams(int[] MafiaTeam, int[] SimpleTeam)
        {
            int num = NumOfCards;
            int i = num+1, j = 0;
            do
            {
                if ((j) <= (SimpleTeam.Length - 1))
                {
                    SimpleTeam[j] += 1;
                    i--;
                    if (i == 0)
                        break;
                }
                if ((j) <= (MafiaTeam.Length - 1))
                {
                    MafiaTeam[j] += 1;
                    i--;
                    if (i == 0)
                        break;
                }
                if (i == ((num + 1) - 6))
                {
                    j = 0;
                    num = ((num + 1) - 7);
                }
                else
                    j++;


            } while (i != 0);
            ShowTeamsMembers(MafiaTeam, SimpleTeam);            
        }
        void ShowTeamsMembers(int[] MafiaTeam, int[] SimpleTeam)
        {
            Console.WriteLine("Mafia Team: mafia-"+MafiaTeam[0]+"; don-"+MafiaTeam[1]);
            Console.WriteLine("Siple Team: people-" + SimpleTeam[0] + "; lover-" + SimpleTeam[1] + "; doctor-" + SimpleTeam[2] + "; police-" + SimpleTeam[3]);
        }
        int RandNum()
        {
            var rand = new Random();
            return rand.Next(NumOfCards+1);
        }
    }
}
