using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2_BoardGames
{
    class Monopoly:BoardGame
    {
        int MaxPlayers = 8;
        Rules rules = new Rules();
        List<Player> players = new List<Player>();
        Money BankMoney = new Money();
        bool[] cards;
        int penalty = 10;
        class Money
        {
            public int[] money = new int[7];
            public  Money()
            {
                int classic = 30;
                for (int i = 0; i < 7; i++)
                    money[i] = classic;
            }
            public Money(bool b)
            {
                money = new int []{3,2,4,1,1,4,2};
            }
        }
        class Player
        {
            public int wherenow = 0;
            public Money cash;//= new Money(true);
            public bool[] cards = new bool[15];//якщо картка куплена гравцем вона = true
            public int Cards = 0;//всього скільки карток у гравця
            public Player()//задаються початкові дані: кошти на гру, немає жодної картки з почотку гри
            {
                cash = new Money(true);
                for(int i = 0; i < 15; i++)
                {
                    cards[i] = false;
                }
            }
            public bool BuyCard(int[] payArray, int numofthecard)//КУПИТИ КАРТКУ: передаються масив payArray[i]=n де і-індекс тих коштів, з яких буде мінусуватись сума, а n-кількість ккупюр які будуь відніматись від payArray[i]
            {
                if (PayForCardPlaceOrBuying(payArray, "Player bought the card №" + numofthecard))
                {
                    Cards += 1;
                    cards[numofthecard-1] = true;
                    return true;
                }
                else 
                    return false;
            }
            public bool PenaltyCard(int[] payArray, int numofthecard)
            {
                return (PayForCardPlaceOrBuying(payArray, "Player payed penalty for the card №" + numofthecard)) ? true : false;
            }//ЗАПЛАТИТИ ШТРАФ за попадання на картку суперника
            bool PayForCardPlaceOrBuying(int []payArray, string stroka)
            {
                if (IsEnoughMoney(payArray))
                {
                    for (int i = 0; i < 7; i++)
                    {
                        cash.money[i] -= payArray[i];
                    }
                    Console.WriteLine(stroka);
                    return true;
                }
                else
                {
                    Console.WriteLine("Player hasn`t enough money for this operation. Try again.");
                    return false;
                }                
            }
            bool IsEnoughMoney(int[] payArray)
            {
                int problem = 0;
                for(int i=0;i<7;i++)
                {
                    if (cash.money[i] < payArray[i])
                        problem += 1;
                }
                return (problem == 0) ? true : false;
            }
            public void AddMoneyPromPenalty(int[] payArray)//отримати гроші від іншого гравця за штраф
            {
                for(int i=0;i<7;i++)
                {
                    
                    cash.money[i] += payArray[i];
                }
            }

        }
        public Monopoly(int numOfGamers)
        {
            if (boardGame(numOfGamers) && numOfGamers <= MaxPlayers)
            {
                Board = true;
                NumOfDices = 2;
                NumOfCards = 15;
                cards = new bool[NumOfCards];
                for (int i = 0; i < NumOfCards; i++)
                    cards[i] = true;
                StartGame();
            }
            else
                ErrorWrongNumOfPlayers(numOfGamers);
        }
        void CreatePlayers()
        {
            for(int i = 0; i < NumOfChips; i++)
            {
                players.Add(new Player());
            }
            CheckBankMoney();
        }
        void CheckBankMoney()
        {
            int num = 0;
            for(int i = 0; i < 7; i++)
            {
                for (int j = 0; j < NumOfChips; j++)
                    num += players[j].cash.money[i];
                BankMoney.money[i] -= num;
                num = 0;
            }
            ShowPlayersMoney();
        }
        void ShowPlayersMoney()
        {
            Console.WriteLine(string.Join(" ", cards));
            Console.WriteLine("Bank: "+ 
                    "1x" + BankMoney.money[0] + " ; " +
                    "5x" + BankMoney.money[1] + " ; " +
                    "10x" + BankMoney.money[2] + " ; " +
                    "20x" + BankMoney.money[3] + " ; " +
                    "50x" + BankMoney.money[4] + " ; " +
                    "100x" + BankMoney.money[5] + " ; " +
                    "500x" + BankMoney.money[6] + " ; ");
            for (int i = 0; i < NumOfChips; i++)
            {
                Console.WriteLine("Plyer "+(i+1)+" : " +
                    "1x"+players[i].cash.money[0]+" ; " +
                    "5x" + players[i].cash.money[1] + " ; "+
                    "10x" + players[i].cash.money[2] + " ; "+
                    "20x" + players[i].cash.money[3] + " ; "+
                    "50x" + players[i].cash.money[4] + " ; "+
                    "100x" + players[i].cash.money[5] + " ; "+
                    "500x" + players[i].cash.money[6] + " ; ");
            }
        }
        void StartGame()
        {
            rules.Monopoly();
            CreatePlayers();
            LetsPlay(WhoFirst());
        }
        void LetsPlay(int whofirst)
        {
            bool gameover = false;
            int winer;
            do
            {
                for(int i=whofirst;i<NumOfChips;i++)
                {
                    int dice = ThrowDices();
                    int adding = (players[i].wherenow + dice);
                    //if (adding == NumOfCards)
                    //    players[i].wherenow = 1;
                    //else 
                    if (adding > NumOfCards)
                        players[i].wherenow = adding - NumOfCards;
                    else
                        players[i].wherenow += dice;
                   Console.WriteLine("You are the Player " + (i + 1) + " and card number = " + players[i].wherenow);
                   ShowPlayersMoney();
                   if (OwnerOfTheCard(players[i].wherenow) != 9)
                        {
                            Console.WriteLine("Paying the penalty...");
                            int[] array = HowMuchMoney(players[i]);
                            if (players[i].PenaltyCard(array, players[i].wherenow))
                                players[OwnerOfTheCard(players[i].wherenow)].AddMoneyPromPenalty(array);
                            else
                                Console.WriteLine("Bad choice.");
                        }
                   else
                        {
                            Console.WriteLine("Buying this card...");
                            int[] array = HowMuchMoney(players[i]);
                            if (players[i].BuyCard(array, players[i].wherenow))
                        {
                            AddMoneyToTheBank(array);
                            cards[(players[i].wherenow - 1)] = false;
                        }
                            else
                                Console.WriteLine("Bad choice.");
                        }
                   if (i == (NumOfChips - 1))
                        i = 0;
                   if (players[i].Cards == 44 || (CountPlayersMoney(players[i]) == 0) || AllCardsIsGone())
                    {
                        GameIsOver();
                        gameover = true;
                    }

                }
            } while (gameover != true);
        }
        int WhoFirst()
        {
            int[] dicesNumber = new int[NumOfChips];
            int max = 0;
            for(int i = 0; i < NumOfChips; i++)
            {
                dicesNumber[i] = ThrowDices();
                if(max < dicesNumber[i])
                    max = i;
            }
            return max;
        }
        int ThrowDices()
        {
            var rand = new Random();
            return rand.Next(1, 7) + rand.Next(1, 7);
        }
        int OwnerOfTheCard(int numofthecard)
        {
            int owner = 9;
            for(int i=0;i<NumOfChips;i++)
            {
                if (players[i].cards[numofthecard-1] == true)
                    owner = i;
            }
            return owner;      
        }
        int CountPlayersMoney(Player player)
        {
            int sum = 0;
            for(int i=0;i<7;i++)
            {
                sum += player.cash.money[i];
            }
            return sum;
        }
        int[] HowMuchMoney(Player player)
        {
            link1:
            int[] array = new int[7] { 0,0,0,0,0,0,0};   
            int num;
            string yes=null;
            do
            {
                Console.WriteLine("Choose one on these types of money and press enter. " +
                " '1-1,2-5,3-10,4-20,5-50,6-100,7-500':");
                Console.WriteLine("If you choose 20/50/100/500 so we will choose this money for the less if bank will be agree.\n" +
                    "And we will ask you again about the type.");
                num = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine("How many points of this type will you pay?");
                array[num-1] = Convert.ToInt16(Console.ReadLine());
                if ((num == 6 || num == 7 || num == 5 || num == 4) && array[num - 1] == 1)
                {
                    ExchangeWithBank(num - 1, player);
                    ShowPlayersMoney();
                    goto link1;
                }

                Console.WriteLine("That`s all?(yes/no)");
                yes = Console.ReadLine();
            }
            while (yes != "yes");

            int sum = 0;
            for (int i = 0; i < 7; i++)
            {
                sum+= MoneyValue(num-1)* array[i];
            }
            if (sum >= penalty)
                return array;
            else
                return HowMuchMoney(player);
        }
        int MoneyValue(int i)
        {
            int[] values = new int[7] { 1, 5, 10, 20, 50, 10, 500 };
            return values[i];
        }
        void ExchangeWithBank(int num, Player player)
        {
            if(BankMoney.money[num]>0 && BankMoney.money[num-1] >= (MoneyValue(num) / 10))
            {
                player.cash.money[num] -= 1;
                player.cash.money[num - 1] += (MoneyValue(num) / 10);
                BankMoney.money[num] += 1;
                BankMoney.money[num - 1] -= (MoneyValue(num) / 10);
            }
        }
        void AddMoneyToTheBank(int[] array)
        {
            for(int i=0;i<7;i++)
            {
                BankMoney.money[i] += array[i];
            }
        }
        bool AllCardsIsGone()
        {
            int num=0;
            for(int i=0;i<NumOfCards;i++)
            {
                if(cards[i]==false)
                    num += 1;
            }
            return (num == NumOfCards) ? true : false;

        }
        void GameIsOver()
        {
            Console.WriteLine("GAME IS OVER!!!");
            Subject sub = new Subject(1);
            ObserverMonopoly ob = new ObserverMonopoly();
            sub.Attach(ob);
            sub.SomeBusinessLogic();
        }
    }
}
