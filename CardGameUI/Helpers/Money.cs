namespace CardGameUI
{
    public class Money
    {
        public int PlayerMoney { get; set; } = 100;

        private int lastBet;

        public void Bet(int betSize)
        {
            lastBet = betSize;
        }

        public void Win()
        {
            PlayerMoney += lastBet;
        }

        public void Blackjack()
        {
            PlayerMoney += lastBet * 2;
        }

        public void Lose()
        {
            PlayerMoney -= lastBet;
        }
    }
}
