using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager tutorialManager;

    [Header("Player Stats")]
    [SerializeField] Rigidbody2D playerRb2D;

    [SerializeField] ParticleSystem smokeParticles;
    [SerializeField] ParticleSystem fireParticles;

    [Header("Camers")]
    [SerializeField] GameObject cmMeni;
    [SerializeField] GameObject cmGamePlay;

    [Header("Canvas and Animations")]
    [SerializeField] Canvas controllerCanvas;
    [SerializeField] Canvas menuCanvas;

    [SerializeField] Animator menuAnim;
    [SerializeField] Animator controllerCanvasAnim;

    [Header("Level Start Animations")]
    [SerializeField] Animator startLevelAnimator;
    [SerializeField] Animator countDownAnim;

    private void Awake()
    {
        if (tutorialManager == null)
        {
            tutorialManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Not found Instance");
            return;
        }
    }
    private void Start()
    {
        playerRb2D.bodyType = RigidbodyType2D.Static;

        cmMeni.SetActive(true);
        cmGamePlay.SetActive(false);

        controllerCanvas.enabled = false;
        menuCanvas.enabled = true;

        smokeParticles.Stop();
        fireParticles.Stop();
    }

    public void StartButton()
    {
        Debug.Log("START !!!");
        //playerRb2D.bodyType = RigidbodyType2D.Dynamic;

        cmMeni.SetActive(false);
        cmGamePlay.SetActive(true);

        controllerCanvas.enabled = true;
        //menuCanvas.enabled = false;

        //playerPartilcesOne.Play();
        //playerPartilcesTwo.Play();

        menuAnim.Play("TutorialHideMenuAnimation");
        controllerCanvasAnim.Play("TutorialShowControllerAnimation");

        startLevelAnimator.Play("StartTutorial");
        countDownAnim.Play("CountDown");
    }

    public void BeforeMenuAnimation()
    {
        playerRb2D.bodyType = RigidbodyType2D.Dynamic;
        //TO CHANGE WHEN PRESSED, IT JUST HAS TO FLY
        smokeParticles.Play();
        fireParticles.Play();
    }

    public void OnPlayerParticles()
    {
        smokeParticles.Play();
        fireParticles.Play();
    }
}
