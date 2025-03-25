using UnityEngine;
using UnityEngine.UI;

public class CustomFontText : MonoBehaviour
{
    public Font customFont;
    public string[] displayTexts = { "Hello, Unity!", "This is a new line.", "Another line here." };
    public Camera customCamera;
    private int currentTextIndex = 0;

    public Vector2 textPosition = new Vector2(-10, -10);
    public Color textColor = Color.white;
    public int fontSize = 24;

    private float charDuration = 0.25f;
    private float minSwitchInterval = 1.5f;
    private float timer = 0f;

    public PauseMenu pauseMenu;
    // 控制需要触发暂停的文本索引
    public int pauseTriggerIndex = 2;

    void Start()
    {
        
        GameObject textObject = new GameObject("CustomText");
        textObject.transform.SetParent(transform);

        Text textComponent = textObject.AddComponent<Text>();
        textComponent.text = displayTexts[currentTextIndex];

        textComponent.font = customFont;

        textComponent.alignment = TextAnchor.UpperLeft;
        textComponent.rectTransform.anchorMin = new Vector2(0, 1);
        textComponent.rectTransform.anchorMax = new Vector2(0, 1);
        textComponent.rectTransform.pivot = new Vector2(0, 1);
        textComponent.rectTransform.anchoredPosition = textPosition;

        textComponent.color = textColor;
        textComponent.fontSize = fontSize;

        textComponent.resizeTextForBestFit = true;
        textComponent.resizeTextMinSize = 10;
        textComponent.resizeTextMaxSize = 200;

        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
        textComponent.verticalOverflow = VerticalWrapMode.Overflow;

        Canvas canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = customCamera;
        }

        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();

        timer = Mathf.Max(displayTexts[currentTextIndex].Length * charDuration, minSwitchInterval);
    }

    void Update()
    {
        if (pauseMenu != null && pauseMenu.isPaused)
        {
            return;
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && currentTextIndex < displayTexts.Length - 1)
        {
            currentTextIndex++;
            CheckAndPause();
            Text textComponent = GetComponentInChildren<Text>();
            textComponent.text = displayTexts[currentTextIndex];
            timer = Mathf.Max(displayTexts[currentTextIndex].Length * charDuration, minSwitchInterval);
        }

        if (currentTextIndex < displayTexts.Length - 1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                currentTextIndex++;
                CheckAndPause();
                Text textComponent = GetComponentInChildren<Text>();
                textComponent.text = displayTexts[currentTextIndex];
                timer = Mathf.Max(displayTexts[currentTextIndex].Length * charDuration, minSwitchInterval);
            }
        }
    }

    void CheckAndPause()
    {
        if (currentTextIndex == pauseTriggerIndex && pauseMenu != null)
        {
            pauseMenu.gameObject.SetActive(true);
            //pauseMenu.PauseGame();
        }
    }
}