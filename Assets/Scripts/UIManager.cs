using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Prefab")]
    public Canvas UI;

    [Header("UI References")]
    public Slider health;
    public Image fillImage;
    public TMP_Text Score;
    public TMP_Text Lives;
    public Slider health2;
    public Image fillImage2;
    public TMP_Text Score2;
    public TMP_Text Lives2;

    [Header("Players")]
    public ControllerPlayer Player1;
    public ControllerPlayer Player2;

    private Camera camera1;
    private Camera camera2;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

    }

    public void SpawnUI(Camera cam, bool isPlayer2)
    {
        Canvas tempUI = Instantiate(UI, Vector3.zero, Quaternion.identity);

        tempUI.renderMode = RenderMode.ScreenSpaceCamera;
        tempUI.worldCamera = cam;

        Slider[] sliders = tempUI.GetComponentsInChildren<Slider>(true);
        TMP_Text[] texts = tempUI.GetComponentsInChildren<TMP_Text>(true);
        Image[] images = tempUI.GetComponentsInChildren<Image>(true);

        if (!isPlayer2)
        {
            if (sliders.Length > 0)
                health = sliders[0];

            if (texts.Length > 0) { 
             Score = texts[0];
             Lives = texts[1];
            }
               
                

            if (images.Length > 1)
                fillImage = images[1];
        }
        else
        {
            if (sliders.Length > 0)
                health2 = sliders[0];

            if (texts.Length > 0)
            {
                Score2 = texts[0];
                Lives2 = texts[1];
            }

            if (images.Length > 1)
                fillImage2 = images[1];
        }
    }


    void Update()
    {
        

            health.value = Player1.pawn.health.healthPercent();
        if (GameManager.instance.isCoop)
            health2.value = Player2.pawn.health.healthPercent();
        Score.text = "Score: "+ Player1.Score;
        if (GameManager.instance.isCoop)
            Score2.text = "Score: " + Player2.Score;
        Lives.text = "Lives: " + Player1.Lives;
        if (GameManager.instance.isCoop)
            Lives2.text = "Lives: " + Player2.Lives;
        // Player 1
        if (Player1.pawn.health.health <= 0f)
        {
            fillImage.enabled = false;
        }
        else
        {
            fillImage.enabled = true;
        }

        // Player 2  FIXED
        if (Player2 != null && Player2.pawn.health.health <= 0f)
        {
            fillImage2.enabled = false;
        }
        else if (Player2 != null)
        {
            fillImage2.enabled = true;
        }



    }
}