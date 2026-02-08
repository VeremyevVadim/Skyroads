using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField]
    private Ship Ship;

    [SerializeField]
    private Animator _animator;

    private void FixedUpdate()
    {
        if (Ship == null)
        {
            return;
        }
       
       _animator.speed = Ship.ForwardSpeed;
    }
}
