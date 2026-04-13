using UnityEngine;

public class DeathTank : Death
{
    public AudioClip explosion;

    private PawnTank pawn;

    void Awake()
    {
        pawn = GetComponent<PawnTank>();
    }

    public override void Die()
    {
        if (pawn.controller.Lives > 0)
        {
            pawn.health.isImmune(true);
            AudioSource.PlayClipAtPoint(explosion, transform.position);
            pawn.transform.position = GameManager.instance.playerSpawner;
            pawn.health.isImmune(false);
            pawn.health.health = pawn.health.maxHealth;
            pawn.controller.Lives -= 1;
        }
        else if (GameManager.instance.player2 != null && GameManager.instance.player1 != null)
        {
            if (pawn.controller.isPlayer2)
            {
                Destroy(gameObject);

            }
            else
            {
                GameManager.instance.player2.listener.enabled = true;
            }
        }
        else {
            GameManager.instance.ActiveGameOverScreenState();
        }
      
    }
}