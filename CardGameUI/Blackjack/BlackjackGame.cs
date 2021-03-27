using System;

namespace CardGameUI
{
    public class BlackjackGame
    {
        private BlackjackHand DealerHand { get; set; }
        private BlackjackHand PlayerHand { get; set; }
        private BlackjackDeck Deck { get; set; }

        private void InitializeGame()
        {
            Deck = new BlackjackDeck();

            var hands = Deck.DealCards(2, 2);

            // player gets the first hand bc they would get the 
            // first card in a casino game.
            PlayerHand = new BlackjackHand();
            PlayerHand.UpdateHand(hands[0], false);

            DealerHand = new BlackjackHand();
            DealerHand.UpdateHand(hands[1], true);

            
        }

        private void Hit(bool isDealer)
        {
            if (isDealer)
            {
                DealerHand.Hand.Add(Deck.RequestCard());
                DealerHand.UpdateHand(DealerHand.Hand, isDealer);

                // see if the dealer wants to hit again
                if (DealerHand.HandTotal < 17 && DealerHand.HandTotal < PlayerHand.HandTotal)
                {
                    Hit(isDealer);
                }

            }
            else
            {
                PlayerHand.Hand.Add(Deck.RequestCard());
                PlayerHand.UpdateHand(PlayerHand.Hand, isDealer);

                Console.WriteLine($"\n\nYour hand is now: {PlayerHand.VisibleHand}");
                Console.WriteLine($"{PlayerHand.VisibleTotal}");

                if (PlayerHand.HandTotal <= 21)
                {
                    bool askForHit = true;
                    while (askForHit)
                    {
                        Console.WriteLine("\nDo you want to hit or stay? Press 'h' to hit or 's' to stay.");
                        var hitOrStay = Console.ReadKey();
                        if (hitOrStay.Key == ConsoleKey.H || hitOrStay.Key == ConsoleKey.NumPad1)
                        {
                            Hit(isDealer);
                            askForHit = false;
                        }
                        else if (hitOrStay.Key == ConsoleKey.S || hitOrStay.Key == ConsoleKey.NumPad2)
                        {
                            askForHit = false;
                        }
                    }
                    
                }
                else
                {
                    Console.WriteLine("You Busted!!!");
                }
            }
        }

        public int Run()
        {
            InitializeGame();

            Console.WriteLine("\nDealer Hand:");
            Console.WriteLine($"{DealerHand.VisibleHand}");
            Console.WriteLine($"{DealerHand.VisibleTotal}");


            Console.WriteLine("\nPlayerHand:");
            Console.WriteLine($"{PlayerHand.VisibleHand}");
            Console.WriteLine($"{PlayerHand.VisibleTotal}");

            int isWinner;

            // check for blackjack
            if (PlayerHand.HandTotal == 21 || DealerHand.HandTotal == 21)
            {          
                isWinner = GameOver();
            }
            else
            {
                // see if the player wants to hit
                Console.WriteLine("\nDo you want to hit or stay? Press 'h' to hit or 's' to stay.");
                var hitOrStay = Console.ReadKey();
                if (hitOrStay.Key == ConsoleKey.H || hitOrStay.Key == ConsoleKey.NumPad1)
                {
                    Hit(false);
                }


                // see if the dealer wants to hit
                if (PlayerHand.HandTotal <= 21 && DealerHand.HandTotal < 17 && DealerHand.HandTotal < PlayerHand.HandTotal)
                {
                    Hit(true);
                }

                // end game
                isWinner = GameOver();
                //isWinner = 0;
            }
            return isWinner;
        }

        private int GameOver()
        {
            Console.WriteLine("\n\n********** GAME OVER **********");

            int isWinner;

            if (PlayerHand.HandTotal > 21 || (PlayerHand.HandTotal < DealerHand.HandTotal && DealerHand.HandTotal <= 21))
            {
                Console.WriteLine("YOU LOSE!\n");
                isWinner = -1;
            }
            else if (PlayerHand.HandTotal == DealerHand.HandTotal)
            {
                Console.WriteLine("IT'S A DRAW!\n");
                isWinner = 0;
            }
            else if (PlayerHand.HandTotal == 21 && PlayerHand.Hand.Count == 2)
            {
                Console.WriteLine("BLACKJACK!!!\n");
                isWinner = 9;
            }
            else
            {
                Console.WriteLine("YOU WIN!\n");
                isWinner = 1;
            }

            foreach (var card in DealerHand.Hand)
            {
                Console.WriteLine($"{card.Value} of {card.Suit}");
            }
            Console.WriteLine($"Dealer Total: {DealerHand.HandTotal}\n");

            foreach (var card in PlayerHand.Hand)
            {
                Console.WriteLine($"{card.Value} of {card.Suit}");
            }
            Console.WriteLine($"Player Total: {PlayerHand.HandTotal}");

            return isWinner;
        }
    }
}
