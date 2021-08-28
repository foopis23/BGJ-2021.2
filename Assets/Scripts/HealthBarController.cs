using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    // editor fields
    public Player player;
    public RawImage HealthBarRed;
    public RawImage HealthBarGreen;
    public float damageTickDuration;

    // private fields
    private float barSize;

    // Start is called before the first frame update
    void Start()
    {
        SetBarSize(1);
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(barSize - player.Health / player.MaxHealth) > 0.001f)
        {
            barSize = player.Health / player.MaxHealth;
            SetBarSize(barSize);
        }
    }

    private void SetBarSize(float fillFraction)
    {
        HealthBarGreen.rectTransform.sizeDelta = new Vector2(HealthBarRed.rectTransform.sizeDelta.x * fillFraction, HealthBarRed.rectTransform.sizeDelta.y);
    }
}
