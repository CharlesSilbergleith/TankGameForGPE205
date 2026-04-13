using UnityEngine;

public class DeathAI : Death
{
    public AudioClip Exsplotion;

    public override void Die() {
        AudioSource.PlayClipAtPoint(Exsplotion, transform.position);
        Controller controller = GetComponent<Controller>();
        GameManager.instance.AITanksLeft.Remove(controller);
        Destroy(gameObject);
        
    }
}
