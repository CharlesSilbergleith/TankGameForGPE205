using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;
     public Death death;
    private bool Immune;

    public virtual void Start() {
        death = GetComponent<Death>();
    }
    public void isImmune(bool immune) {
        Immune = immune;
    }
    public virtual void TakeDamage() {
        if(!Immune)
        health -= 10;
        if (health <= 0)
        {
            health = 0;
            death.Die();
        }
    }
    public virtual void TakeDamage(float dmg)
    {
        if (!Immune)
            health -= dmg;
        if (health <= 0)
        {
            health = 0;
            death.Die();
            
        }
    }
    public virtual void takeDamage(int dmg) {
        if (!Immune)
            health -= dmg;
        if (health <= 0) {
            health = 0;
            death.Die();
        }
    }
    public virtual void Heal(float healAmount) { 
        health += healAmount;
    }
    public virtual float healthPercent() { 
        return health/maxHealth;
    }

}
