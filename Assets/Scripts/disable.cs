using UnityEngine;

public class disable : MonoBehaviour
{

    // Update is called once per frame
    public void Update()
    {

        if (GameManager.instance.GameplayStateObject.activeSelf == false)
        {
            Destroy(this.gameObject);
        }
    }
}
