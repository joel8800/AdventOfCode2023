using System.Text;

namespace Day07
{
    internal class Hand
    {
        public string Cards { get; set; }
        public int Bid { get; set; }
        public int Type { get; set; }
        public string CompStr { get; set; }

        public Hand(string cards, int bid)
        {
            Cards = cards;
            Bid = bid;

            Type = GetHandType();
            CompStr = GetCompString();
        }

        public override string ToString()
        {
            return $"{Cards}:{Type}:{Bid}";
        }

        public int UpdateForPart2()
        {
            CompStr = CompStr.Replace('k', 'a');
            Type = UpdateTypeForPt2();

            return Type;
        }

        private string GetCompString()
        {
            StringBuilder sb = new();

            foreach (char c in Cards)
            {
                char s = ' ';
                switch (c)
                {
                    case '2': s = 'b'; break;
                    case '3': s = 'c'; break;
                    case '4': s = 'd'; break;
                    case '5': s = 'e'; break;
                    case '6': s = 'f'; break;
                    case '7': s = 'g'; break;
                    case '8': s = 'h'; break;
                    case '9': s = 'i'; break;
                    case 'T': s = 'j'; break;
                    case 'J': s = 'k'; break;
                    case 'Q': s = 'l'; break;
                    case 'K': s = 'm'; break;
                    case 'A': s = 'n'; break;
                }
                sb.Append(s);
            }

            return sb.ToString();
        }

        private int GetHandType()
        {
            List<char> h = Cards.ToCharArray().ToList();
            h.Sort();

            switch (h.Distinct().Count())
            {
                case 5:         // nothing
                    return 1;
                case 4:         // 1 pair
                    return 2;
                case 3:         // 2 pair, 3 kind
                    for (int i = 0; i < 3; i++)
                        if (h[i] == h[i + 1] && h[i] == h[i + 2])
                            return 4;
                    return 3;
                case 2:         // 4 kind, full house
                    for (int i = 0; i < 2; i++)
                        if (h[i] == h[i + 1] && h[i] == h[i + 2] && h[i] == h[i + 3])
                            return 6;
                    return 5;
                case 1:         // 5 kind
                    return 7;
            }

            return 0;
        }

        private int UpdateTypeForPt2()
        {
            int numJ = Cards.Where(c => c == 'J').Count();

            if (numJ == 0)
                return Type;

            switch (Type)
            {
                case 1:         // high card
                    return 2;
                case 2:         // 1 pair
                    return 4;
                case 3:         // 2 pair
                    return numJ == 1 ? 5 : 6;
                case 4:         // 3 of a kind
                    return 6;
                case 5:         // full house
                case 6:         // 4 of a kind
                case 7:         // 5 of a kind
                    return 7;
                default:
                    break;
            }

            return 0;
        }

    }
}
