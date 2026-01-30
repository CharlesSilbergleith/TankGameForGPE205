using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
    
    public GameObject bullet;
    public abstract void Shoot();
    public float fireForce;
    public int damage;

}
