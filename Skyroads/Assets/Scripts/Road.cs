using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    private Ship Ship = null;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Move road with ship forward speed
    private void FixedUpdate()
    {
        if (!Ship)
        {
            return;
        }
       
       _animator.speed = Ship.ForwardSpeed;
    }
}
