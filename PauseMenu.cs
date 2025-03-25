using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup pauseMenuCanvas;
    public Image grayscaleOverlay;
    public Button continueButton;
    public Button quitButton;
    public TextMeshProUGUI titleText;

    public bool isPaused
    {
        get { return _isPaused; } // ʹ��˽���ֶδ洢ֵ
        private set { _isPaused = value; }
    }
    private bool _isPaused = false; // ˽���ֶ�

    private void Awake()
    {
        //����Ϸ������ʱ����������Ϊ�Ǽ���״̬
        gameObject.SetActive(false);
    }

    public void Start()
    {
        pauseMenuCanvas.alpha = 0;
        pauseMenuCanvas.interactable = false;
        pauseMenuCanvas.blocksRaycasts = false;
        grayscaleOverlay.enabled = false;


        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
        pauseMenuCanvas.alpha = 1;
        pauseMenuCanvas.interactable = true;
        pauseMenuCanvas.blocksRaycasts = true;
        grayscaleOverlay.enabled = true;
    }

    public void ContinueGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        pauseMenuCanvas.alpha = 0;
        pauseMenuCanvas.interactable = false;
        pauseMenuCanvas.blocksRaycasts = false;
        grayscaleOverlay.enabled = false;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}