using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float lifetime;
    public int damage;
    public GameObject shooter;
    public Controller whoShot;
    public float speed;
    public Rigidbody rb;

    void Start()
    {
        Destroy(gameObject, lifetime);

       

        Pawn pawnShot = shooter.GetComponentInParent<Pawn>();

        

        whoShot = pawnShot.controller;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (shooter != null && other.transform.root.gameObject == shooter.transform.root.gameObject)
        {
            return;
        }

        Health targetHealth = other.GetComponentInParent<Health>();

        if (targetHealth != null)
        {
            ControllerAi targetController = targetHealth.GetComponentInParent<ControllerAi>();

            targetHealth.TakeDamage(damage);

            if (targetHealth.health <= 0 && targetController != null && whoShot != null)
            {
                whoShot.Score += 1;
               
            }
        }

        Destroy(gameObject);
    }

    public abstract void Shoot();
    public abstract void Shoot(float speed);
    public abstract void Shoot(float speed, int dmg);
}