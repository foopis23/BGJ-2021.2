using UnityEngine;

public class MenuEasterEggController : MonoBehaviour
{
    
    public float eggFreq = 300.0f;
    
    private float _lastEgg = 0.0f;
    private Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - _lastEgg >= eggFreq)
        {
            _lastEgg = Time.time;
            _animator.Play("HorseMenu");
        }
    }
}
