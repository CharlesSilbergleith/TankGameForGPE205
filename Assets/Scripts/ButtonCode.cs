 using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class ButtonCode : MonoBehaviour
{
    public void MainMenu() {
        
        GameManager.instance.ActiveMainMenuState();


    }
    public void Options() {
        GameManager.instance.ActiveOptionsScreenState();

    }
    public void SinglePlayer() {
        GameManager.instance.isCoop= false;

        GameManager.instance.ActiveGameplayState();


    }

    public void CoOp()
    {
        
        GameManager.instance.isCoop = true;
        GameManager.instance.ActiveGameplayState();


    }

    public void Credits()
    {
        GameManager.instance.ActiveCreditsScreenState();


    }

    public void Quit()
    {

        Application.Quit();

    }

 


}
