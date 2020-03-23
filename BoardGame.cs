using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_BoardGames
{
    class BoardGame
    {
        protected bool Board;
        protected int NumOfDices;
        protected int NumOfChips;
        protected int NumOfCards;
        protected int MinPlayers = 2;
        protected int MaxPlayers;
        protected bool boardGame(int numOfGamers)
        {
            return ((NumOfChips = numOfGamers) < MinPlayers) ? false : true;
        }
        protected void ErrorWrongNumOfPlayers(int numOfGamers)
        {
            if (numOfGamers > MinPlayers) 
            {
                Console.WriteLine("Too many players!");
            }
            else
                Console.WriteLine("Not enough players!");
        }
    }
}
