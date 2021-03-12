using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamagable
{
    [SerializeField]
    private float CurrentHealth;
    [SerializeField]
    private float TotalHealth;

    public float Health => CurrentHealth;
    public float MaxHealth => TotalHealth;

    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }
    public virtual  void Destroy()
    {
        Destroy(gameObject);
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy();
        }
    }

}
