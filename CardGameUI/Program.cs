using System;
using System.Text;
using System.Threading.Tasks;

namespace CardGameUI
{
    class Program
    {
        static void Main()
        {
            Money blackjackMoney = new Money();

            while (true)
            {
                Console.WriteLine("\n\n\n*** Welcome to BernJack! ***\n");

                BlackjackGame game = new BlackjackGame();

                int betSize = 0;
                while (betSize == 0)
                {
                    Console.WriteLine($"How much would you like to wager(ex. 0 - {blackjackMoney.PlayerMoney})? Wallet: ${blackjackMoney.PlayerMoney}");
                    string bet = Console.ReadLine();

                    if (Int32.TryParse(bet, out betSize))
                    {
                        if (betSize > 0 && betSize <= blackjackMoney.PlayerMoney)
                        {
                            blackjackMoney.Bet(betSize);
                        }
                        else
                        {
                            betSize = 0;
                        }
                    }
                    else
                    {
                        betSize = 0;
                    }
                }

                int isWinner = game.Run();

                if (isWinner == 1)
                {
                    blackjackMoney.Win();
                }
                else if (isWinner == 9)
                {
                    blackjackMoney.Blackjack();
                }
                else if (isWinner == -1)
                {
                    blackjackMoney.Lose();
                }

                if (blackjackMoney.PlayerMoney < 1)
                {
                    Console.WriteLine($"You are out of money. Game Over. Wallet: ${blackjackMoney.PlayerMoney}");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    Console.WriteLine($"\nYour wallet balance is ${blackjackMoney.PlayerMoney}");
                }
            }
        }
    }
}
