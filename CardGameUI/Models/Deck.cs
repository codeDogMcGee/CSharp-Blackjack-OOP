using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGameUI
{
    internal abstract class Deck
    {
        protected List<PlayingCardModel> fullDeck = new List<PlayingCardModel>();
        protected List<PlayingCardModel> drawPile = new List<PlayingCardModel>();
        protected List<PlayingCardModel> discardPile = new List<PlayingCardModel>();

        protected void CreateDeck()
        {
            // clear is more efficient than creating a new deck bc it maintains the memory space
            fullDeck.Clear();

            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardValue val in Enum.GetValues(typeof(CardValue)))
                {
                    fullDeck.Add(new PlayingCardModel { Suit = suit, Value = val });
                }
            }
        }

        internal virtual void ShuffleDeck()
        {
            var rand = new Random();
            // lambda orders the deck based on a random number, instead of rand.Next()
            // could have x.suit to order the deck by suit
            drawPile = fullDeck.OrderBy(x => rand.Next()).ToList();
        }

        internal virtual List<List<PlayingCardModel>> DealCards(int numberOfHands, int cardsPerHand)
        {
            //List<PlayingCardModel> hand1 = new List<PlayingCardModel>();
            List<List<PlayingCardModel>> output = new List<List<PlayingCardModel>>();
            for (int card = 0; card < cardsPerHand; card++)
            {
                for (int hand = 0; hand < numberOfHands; hand++)
                {
                    // check if the index (hand) exists,
                    // if so add a card, if not create the hand in
                    // output first then add a card.
                    if (hand < output.Count)
                    {
                        output[hand].Add(DrawOneCard());
                    }
                    else
                    {
                        output.Add(new List<PlayingCardModel>());
                        output[hand].Add(DrawOneCard());
                    }
                }
            }

            return output;
        }

        protected virtual PlayingCardModel DrawOneCard()
        {
            // Take() does not remove from list, just returns it
            PlayingCardModel output = drawPile.Take(1).First();
            drawPile.Remove(output);
            return output;
        }
    }
}
