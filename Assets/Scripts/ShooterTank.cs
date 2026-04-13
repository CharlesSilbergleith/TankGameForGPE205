using UnityEngine;

public class ShooterTank : Shooter
{ 
    public AudioClip shootAudio;
    public AudioSource audioSource;
    void Start()
    {
        shooter = this.gameObject;
        audioSource = GetComponent<AudioSource>();
    }

   

    
    public override void Shoot()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(shootAudio, .5f);
        }
       
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

        Bullet bulletScript = newBullet.GetComponent<Bullet>();

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