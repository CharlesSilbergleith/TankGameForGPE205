using UnityEngine;

public class Tile : MonoBehaviour
{

    public GameObject doorSouth;
    public GameObject doorNorth;
    public GameObject doorEast;
    public GameObject doorWest;

    public virtual void Update()
    {

        if (GameManager.instance.GameplayStateObject.activeSelf == false)
        {
            Destroy(this.gameObject);
        }



    }

}
