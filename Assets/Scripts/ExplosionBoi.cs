using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CallbackEvents;

public class ExplosionBoi : MonoBehaviour
{
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.Current.RegisterEventListener<ExplosionEventContext>(OnExplosion);
    }

    private void OnExplosion(ExplosionEventContext context)
    {
        var particleObject = Instantiate(explosionPrefab);
        particleObject.transform.position = context.Pos;
        for(int i = 0; i < particleObject.transform.childCount; i++)
        {
            particleObject.transform.GetChild(i).localScale *= context.PowerLevel; 
        }

        EventSystem.Current.CallbackAfter(() => {
            Destroy(particleObject);
        }, 5000);
    }
}
