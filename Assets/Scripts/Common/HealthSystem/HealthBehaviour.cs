using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField][Range(0,1)] float health;

    public float Health { get => health; set => health = value; }

    public bool DealDamage(float amount) // -> was critical?
    {
        bool crit = false;
        health -= amount;

        if (health <= 0)
            crit = true;

        health = Mathf.Clamp(health, 0, 1);

        return crit;
    }

}
