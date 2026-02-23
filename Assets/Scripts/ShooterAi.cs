using UnityEngine;

public class ShooterAi : Shooter
{
    public override void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

        Bullet bulletScript = newBullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Access the fireForce variable from ShooterTank
            bulletScript.speed = this.fireForce;
        }
    }
}
