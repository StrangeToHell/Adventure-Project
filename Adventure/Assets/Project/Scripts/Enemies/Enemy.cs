using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int Health = 1;

    public virtual void Hit ()
    {
        Health--;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.GetComponent<Sword>() != null)
        {
            if (otherCollider.GetComponent<Sword>().IsAttacking)
            {
                Hit();
            }
        } else if (otherCollider.GetComponent<Arrow>() != null)
        {
            Hit();
            Destroy(otherCollider.gameObject);
        }
    }
}
