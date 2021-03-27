using System.Collections.Generic;

namespace CardGameUI
{
    internal class BlackjackHand
    {
        internal string VisibleHand { get; private set; }
        internal string VisibleTotal { get; private set; }
        internal int HandTotal { get; private set; }
        internal List<PlayingCardModel> Hand { get; set; }

        internal void UpdateHand(List<PlayingCardModel> hand, bool isDealer)
        {
            Hand = hand;
            int handSum = HandValue(hand);

            if (isDealer)
            {
                PlayingCardModel upCard = hand[1];
                List<PlayingCardModel> upHand = new List<PlayingCardModel>() { upCard };

                VisibleHand = "XXXX " + CreateVisibleHand(upHand);
                VisibleTotal = $"{HandValue(upHand)}";
                HandTotal = handSum;
            }
            else
            {
                VisibleHand = CreateVisibleHand(hand);
                VisibleTotal = $"{handSum}";
                HandTotal = handSum;
            }

        }

        private readonly Dictionary<CardValue, int> cardValues = new Dictionary<CardValue, int>()
        {
            {CardValue.Ace, 1 },
            {CardValue.Two, 2 },
            {CardValue.Three, 3 },
            {CardValue.Four, 4 },
            {CardValue.Five, 5 },
            {CardValue.Six, 6 },
            {CardValue.Seven, 7 },
            {CardValue.Eight, 8 },
            {CardValue.Nine, 9 },
            {CardValue.Ten, 10 },
            {CardValue.Jack, 10 },
            {CardValue.Queen, 10 },
            {CardValue.King, 10 }
        };

        private int HandValue(List<PlayingCardModel> hand)
        {
            int total = 0;
            List<PlayingCardModel> aces = new List<PlayingCardModel>();

            foreach (var card in hand)
            {
                // seperate the aces for now
                if (card.Value == CardValue.Ace)
                {
                    aces.Add(card);
                }
                else
                {
                    total += cardValues[card.Value];
                }
            }

            // figure out if each ace should be a 1 or 11
            foreach (var card in aces)
            {
                if (total <= 10)
                {
                    total += 11;
                }
                else
                {
                    total += 1;
                }
            }

            return total;
        }

        private string CreateVisibleHand(List<PlayingCardModel> hand)
        {
            string handString = "";
            foreach (var card in hand)
            {
                handString += $"{card.Value}{card.Suit} ";
            }
            return handString;
        }
    }
}
