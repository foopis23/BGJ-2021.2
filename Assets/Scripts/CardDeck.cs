using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardDeck : MonoBehaviour
{
    public float cardExpireTime = 10.0f;
    public CardObject[] allCards;
    [NonSerialized] public CardObject PurchasedCard;
    [NonSerialized] public float LastBoughtCardTime;

    public void Start()
    {
        LastBoughtCardTime = -1.0f;
    }

    public void PurchaseCard(Player player)
    {
        if (player.purchaseCardPoints < 1) return;
        player.purchaseCardPoints--;
        PurchasedCard = Instantiate(allCards[Random.Range(0, allCards.Length)]);
        PurchasedCard.Init();
        LastBoughtCardTime = Time.time;
        
        CallbackEvents.EventSystem.Current.CallbackAfter(() =>
        {
            PurchasedCard = null;
            LastBoughtCardTime = -1.0f;
        }, (int) (cardExpireTime * 1000.0f));
    }
}
