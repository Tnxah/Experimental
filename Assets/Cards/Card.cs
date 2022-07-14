using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : ScriptableObject 
{
    [HideInInspector]
    public CardType type;

    public Sprite background;

    public virtual void Init() 
    {
        Debug.Log($"Init: {name}");
    }
}

public enum CardType
{
    NormalCard,
    HeroCard
}
