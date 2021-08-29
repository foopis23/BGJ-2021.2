using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackEvents;

public class Piss : MonoBehaviour
{
    public AudioSource coinSound;
    public AudioSource cardSound;

    void Start()
    {
        EventSystem.Current.RegisterEventListener<CoinPickupContext>(PlayCoinSound);
        EventSystem.Current.RegisterEventListener<CardActionContext>(PlayCardSound);
    }

    private void PlayCoinSound(CoinPickupContext context)
    {
        coinSound.Play();
    }

    private void PlayCardSound(CardActionContext context)
    {
        cardSound.Play();
    }
}
