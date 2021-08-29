using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    public CardObject[] allCards;
    [NonSerialized] public CardObject PurchasedCard;

    public void PurchaseCard(Player player)
    {
        if (player.purchaseCardPoints < 1) return;
        player.purchaseCardPoints--;
        PurchasedCard = Instantiate(allCards[Random.Range(0, allCards.Length)]);
        PurchasedCard.Init();
    }
}
