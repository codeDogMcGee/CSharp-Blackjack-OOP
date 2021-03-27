using System.Collections.Generic;

namespace CardGameUI
{
    internal class BlackjackDeck : Deck
    {
        internal BlackjackDeck()
        {
            CreateDeck();
            ShuffleDeck();
        }

        internal PlayingCardModel RequestCard()
        {
            return DrawOneCard();
        }
    }
}
