using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NormalCard", menuName = "Cards/Normal")]
public class NormalCard : Card
{
    public int number;

    public Suit suit;

    public Sprite symbol;

    public override void Init()
    {
        base.Init();

        type = CardType.NormalCard;
    }
}

public enum Suit
{
    Clubs,
    Spades,
    Hearts,
    Diamonds
}
