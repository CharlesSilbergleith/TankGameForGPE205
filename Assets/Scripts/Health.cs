using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;
    [HideInInspector] public Death death;

    public virtual void Start() {
        death = GetComponent<Death>();
    }
    public virtual void TakeDamage() {
        health -= 10;
        if (health <= 0)
        {
            health = 0;
            death.Die();
        }
    }
    public virtual void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            health = 0;
            death.Die();
            
        }
    }
    public virtual void takeDamage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            health = 0;
            death.Die();
        }
    }
    public virtual void Heal(float healAmount) { 
        health += healAmount;
    }
    

}
