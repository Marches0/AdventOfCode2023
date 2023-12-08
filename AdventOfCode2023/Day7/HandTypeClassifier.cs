using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runner;

internal class HandTypeClassifier
{
    public HandType GetHandType(List<Card> cards)
    {
        var cardGroups = cards
            .GroupBy(c => c)
            .ToList();

        // Five of a kind, where all five cards have the same label: AAAAA
        if (cardGroups.Count == 1)
        {
            return HandType.FiveOfAKind;
        }

        // Four of a kind, where four cards have the same label and one card has a different label: AA8AA
        if (cardGroups.Count == 2 && cardGroups.Any(g => g.Count() == 4))
        {
            return HandType.FourOfAKind;
        }

        // Full house, where three cards have the same label,
        // and the remaining two cards share a different label: 23332
        if (cardGroups.Count == 2 && cardGroups.Any(g => g.Count() == 3))
        {
            return HandType.FullHouse;
        }

        // Three of a kind, where three cards have the same label,
        // and the remaining two cards are each different from any other card in the hand: TTT98
        if (cardGroups.Count == 3 && cardGroups.Any(g => g.Count() == 3))
        {
            return HandType.ThreeOfAKind;
        }

        // Two pair, where two cards share one label, two other cards share a second label,
        // and the remaining card has a third label: 23432
        if (cardGroups.Count == 3 && cardGroups.Count(g => g.Count() ==2) == 2)
        {
            return HandType.TwoPair;
        }

        // One pair, where two cards share one label,
        // and the other three cards have a different label from the pair and each other: A23A4
        if (cardGroups.Count == 4 && cardGroups.Any(g => g.Count() == 2))
        {
            return HandType.OnePair;
        }

        // High card, where all cards' labels are distinct: 23456
        if (cardGroups.Count == 5)
        {
            return HandType.HighCard;
        }

        throw new NotImplementedException();
    }
}

internal enum HandType
{
    HighCard = 1,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}