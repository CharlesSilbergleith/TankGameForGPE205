using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
  
    
    public float lifetime;     // How long before it destroys itself
    public int damage;  
    // How much damage it deals
   
    public float speed;
    public Rigidbody rb;
    void Start()
    {
        // Automatically destroy the bullet after 'lifetime' seconds
       
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the bullet forward every frame
       
            transform.Translate(Vector3.forward * speed * Time.deltaTime); 
     
       
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Example: if it hits something with a "Health" component, apply damage
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        // Destroy the bullet on any collision
        Destroy(gameObject);
    }
    public abstract void Shoot();

    public abstract void Shoot(float speed);

    public abstract void Shoot(float speed, int dmg);
}

