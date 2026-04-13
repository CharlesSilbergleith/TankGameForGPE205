using UnityEngine;

public class PawnTank : Pawn
{
    [Header("shoot")]
    private float nextShootTime;
    public float firerate;
    [Header("noise")]
    public NoiseMaker noiseMaker;
    public float noiceDistance;
    [HideInInspector] public Camera camera;
     public AudioListener listener;


    


    
    public override void Start()
    {
        // Save my tank in my GameManager
        GameManager.instance.tanks.Add(this);
        noiseMaker = GetComponent<NoiseMaker>();
        health = GetComponent<Health>();
        camera = GetComponentInChildren<Camera>();
        if (GameManager.instance.isCoop)
        {
            if (controller.isPlayer2)
            {

                listener = GetComponentInChildren<AudioListener>();
                if (listener == null)
                {
                    Debug.Log("NUll");
                }
                listener.enabled = false;
                camera.rect = new Rect(0, 0, .5f, 1);
                GameManager.instance.player2 = this;
                GameManager.instance.player2Spawner = transform.position;
                GameManager.instance.player1Controller = controller;
            }
            else
            {
                camera.rect = new Rect(0.5f, 0, 1, 1);
                GameManager.instance.player1 = this;
                GameManager.instance.playerSpawner = transform.position;
            }
        }
        else {
            GameManager.instance.playerSpawner = transform.position;
            GameManager.instance.player1 = this;
            GameManager.instance.player1Controller = controller;


        }



        UIManager.Instance.SpawnUI(camera, controller.isPlayer2);

        // Do what all pawns do




        base.Start();
        
    }

    public override void OnDestroy()
    {
        // Remove my tank from the GameManager list
        GameManager.instance.tanks.Remove(this);
    }

    public override void Move(Vector3 directionToMove)
    {
        // Tell the mover to move
        mover.Move(directionToMove, moveSpeed);
       
    }

    public override void Rotate(Vector3 directionToRotate)
    {
        // Tell the mover to rotate
        mover.Rotate(directionToRotate, turnSpeed);
    }

    public override void Shoot()
    {

        if (Time.time>=nextShootTime)
        {
            shooter.Shoot();
           nextShootTime = Time.time+1/firerate;
        }
        
    }
   public override void Update() {
        noiseMaker.makeNoise(0f);
        float vol = GameManager.instance.vol;
        AudioListener.volume = vol;
        base.Update();
    }

    public override void RotateTowards(Vector3 postition,float turnSpeed) {
        mover.RotateTowards(postition, turnSpeed);
    
    }

    public override void MoveSound() {
        noiseMaker.makeNoise(noiceDistance);
    }
    

}
