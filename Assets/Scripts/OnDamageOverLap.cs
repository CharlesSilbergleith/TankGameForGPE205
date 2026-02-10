using UnityEngine;
[RequireComponent (typeof(Collider))]
public class OnDamageOverLap : MonoBehaviour
{
    public float damage;
    private Collider _collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
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
}
