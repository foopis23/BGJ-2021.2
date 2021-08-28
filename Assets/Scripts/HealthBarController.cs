using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CallbackEvents;

public class HealthBarController : MonoBehaviour
{
    // editor fields
    public Player player;
    public RawImage healthBarRed;
    public RawImage healthBarGreen;
    public float damageTickDuration;

    // private fields
    private float currentSize;
    private float previousSize;
    private float targetSize;
    private float damageTickTime;

    // Start is called before the first frame update
    void Start()
    {
        SetBarSize(1);
        EventSystem.Current.RegisterEventListener<OnPlayerDamageContext>(OnPlayerDamage);
        targetSize = currentSize = previousSize = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - damageTickTime < damageTickDuration + Time.fixedDeltaTime * 2)
        {
            currentSize = Mathf.SmoothStep(previousSize, targetSize, (Time.time - damageTickTime) / damageTickDuration);
            SetBarSize(currentSize);
        }
    }

    private void SetBarSize(float fillFraction)
    {
        healthBarGreen.rectTransform.sizeDelta = new Vector2(healthBarRed.rectTransform.sizeDelta.x * fillFraction, healthBarRed.rectTransform.sizeDelta.y);
    }

    private void OnPlayerDamage(OnPlayerDamageContext context)
    {
        if(context.Player == player)
        {
            previousSize = currentSize;
            targetSize = player.Health / player.MaxHealth;
            damageTickTime = Time.time;
        }
    }
}
