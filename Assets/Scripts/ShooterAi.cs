using UnityEngine;

public class ShooterAi : Shooter
{
    public AudioClip shootAudio;
   public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public override void Shoot()
    {
        audioSource.PlayOneShot(shootAudio,.5f);
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Access the fireForce variable from ShooterTank
            bulletScript.speed = this.fireForce;
        }
       

        if (bulletScript != null)
        {
            bulletScript.shooter = this.gameObject;
            bulletScript.speed = this.fireForce;
        }
        else
        {
            Debug.LogWarning("Bullet prefab is missing Bullet script!");
        }
    }
}
