using UnityEngine;
using UnityEngine.UI;

public class CustomFontText : MonoBehaviour
{
    [Header("数据资源")]
    public DialogueData dialogueData;       // 剧情文本
    public EventData eventData;     // 剧情事件

    [Header("UI 设置")]
    public Font customFont;
    public Camera customCamera;
    public Vector2 textPosition = new Vector2(-10, -10);
    public Color textColor = Color.white;
    public int fontSize = 24;

    [Header("文字切换控制")]
    private int currentTextIndex = 0;
    private float charDuration = 0.25f;
    private float minSwitchInterval = 1.5f;
    private float timer = 0f;

    [Header("外部引用")]
    public PauseMenu pauseMenu;

    private Text textComponent;

    void Start()
    {
        // 创建文字对象
        GameObject textObject = new GameObject("CustomText");
        textObject.transform.SetParent(transform);

        textComponent = textObject.AddComponent<Text>();
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

        // 确保有 Canvas
        Canvas canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = customCamera;
        }

        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();

        // 初始化文本
        if (dialogueData != null && dialogueData.lines.Length > 0)
        {
            textComponent.text = dialogueData.lines[currentTextIndex];
            timer = Mathf.Max(dialogueData.lines[currentTextIndex].Length * charDuration, minSwitchInterval);
        }
    }

    void Update()
    {
        if (pauseMenu != null && pauseMenu.isPaused)
        {
            return;
        }

        // 点击/按键跳到下一行
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) 
            && currentTextIndex < dialogueData.lines.Length - 1)
        {
            ShowNextLine();
        }

        // 自动切换
        if (currentTextIndex < dialogueData.lines.Length - 1)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ShowNextLine();
            }
        }
    }

    void ShowNextLine()
    {
        currentTextIndex++;
        if (currentTextIndex >= dialogueData.lines.Length) return;

        textComponent.text = dialogueData.lines[currentTextIndex];
        timer = Mathf.Max(dialogueData.lines[currentTextIndex].Length * charDuration, minSwitchInterval);

        CheckAndTriggerEvent(currentTextIndex);
    }

    void CheckAndTriggerEvent(int index)
    {
        if (eventData == null) return;

        foreach (var e in eventData.events)
        {
            if (e.lineIndex == index)
            {
                switch (e.eventType)
                {
                    case EventType.Pause:
                        if (pauseMenu != null)
                            pauseMenu.gameObject.SetActive(true);
                        break;
                    case EventType.PlaySound:
                        Debug.Log("播放音效: " + e.eventParam);
                        break;
                    case EventType.ChangeScene:
                        Debug.Log("切换场景: " + e.eventParam);
                        break;
                    case EventType.Custom:
                        Debug.Log("触发自定义事件: " + e.eventParam);
                        break;
                }
            }
        }
    }
}
