using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner;

internal class CamelCardReader
{
    public Hand Read(string line)
    {
        // 32T3K 765
        var parts = line.Split(' ');

        return new Hand()
        {
            Cards = parts[0]
                .Select(GetCard)
                .ToList(),
            Bid = int.Parse(parts[1]),
            CardsRaw = parts[0]
        };
    }

    private Card GetCard(char c)
    {
        return c switch
        {
            '2' => Card.Two,
            '3' => Card.Three,
            '4' => Card.Four,
            '5' => Card.Five,
            '6' => Card.Six,
            '7' => Card.Seven,
            '8' => Card.Eight,
            '9' => Card.Nine,
            'T' => Card.Ten,
            'J' => Card.Joker,
            'Q' => Card.Queen,
            'K' => Card.King,
            'A' => Card.Ace,
            _ => throw new NotImplementedException(),
        };
    }
}

internal enum Card
{
    Joker = 1,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

internal class Hand
{
    public List<Card> Cards { get; set; }
    public HandType HandType { get; set; }
    public long Bid { get; set; }
    public string CardsRaw { get; set; }


    public override string ToString()
    {
        return $"{CardsRaw} {HandType}";
    }
}