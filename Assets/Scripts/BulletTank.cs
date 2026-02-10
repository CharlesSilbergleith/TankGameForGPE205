using UnityEngine;

public class BulletTank : Bullet
{
   

    public override void Shoot()
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot(float speed)
    {
        throw new System.NotImplementedException();
    }

    public override void Shoot(float speed, int dmg)
    {
        rb.linearVelocity = transform.forward * speed;
    }

}
