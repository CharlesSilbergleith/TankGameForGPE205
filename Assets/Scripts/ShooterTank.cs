using UnityEngine;

public class ShooterTank : Shooter
{
    public override void Shoot() {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);

        bullet bulletScript = newBullet.GetComponent<bullet>();
        if (bulletScript != null)
        {
            // Access the fireForce variable from ShooterTank
            bulletScript.speed = this.fireForce;
        }
    }
}
