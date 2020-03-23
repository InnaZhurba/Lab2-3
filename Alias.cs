using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_BoardGames
{
    class Alias:BoardGame
    {        
        int MaxPlayers = 14;
        int NumOfWordsInCard = 8;
        List<Team> teams = new List<Team>();
        class Team
        {
            public int NumOfMembers = 0;
            public int NumOfCorrectAnswers = 0;
        }
        public Alias(int numOfGamers)
        {
            if (boardGame(numOfGamers) && numOfGamers <= MaxPlayers)
            {
                Board = true;
                NumOfDices = 0;
                NumOfCards = 15;
                for(int i=0;i<2;i++)
                {
                    teams.Add(new Team());
                }
                teams[0].NumOfMembers = NumOfChips / 2;
                teams[1].NumOfMembers = NumOfChips - teams[0].NumOfMembers;
                StartGame();
            }
            else
                ErrorWrongNumOfPlayers(numOfGamers);
        }
        void StartGame()
        {
            Rules rule = new Rules();
            rule.Alias();
            //bool gameover = false;
            //do
            //{
                for (int i = 0; i < teams.Count; i++)
                {
                    for (int j = 0; j < NumOfCards* NumOfWordsInCard; j++)
                    {
                    bool num = RandAnswer();
                            if (num)
                                teams[i].NumOfCorrectAnswers += 1;
                    Console.WriteLine("Questiong "+(j+1)+" - "+num);
                    }                   
                }
                Console.WriteLine(WhoWin() + " - the winner!!!");
                ShowResults();
            //    gameover = true;

            //} while (gameover != true);
        }
        void ShowResults()
        {
            for(int i=0;i<teams.Count;i++)
            {
                Console.WriteLine("Team "+(i+1)+" has "+teams[i].NumOfCorrectAnswers+" correct answers with "+ teams[i].NumOfMembers+" members.");
            }
        }
        string WhoWin()
        {
            if (teams[0].NumOfCorrectAnswers > teams[1].NumOfCorrectAnswers)
                return "Team 1";
            else if (teams[0].NumOfCorrectAnswers == teams[1].NumOfCorrectAnswers)
                return "Both Teams";
            else
                return "Team 2";
        }
        bool RandAnswer()
        {
            var rand = new Random();
            return rand.Next(2) == 1 ? true : false;

        }
    }
}
