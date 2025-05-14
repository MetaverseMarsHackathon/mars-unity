using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewUI : MonoBehaviour
{
    //public TextMeshPro descriptionText;                // 텍스트 내용 표시용
    public GameObject descriptionText;   
    public RectTransform backgroundPanel;       // 조절할 패널 (ex: Panel_Background)
    public TMP_Text textbox;
    public float baseHeight = 200f;             // 기본 높이 (줄 내용이 짧을 경우)
    public float heightPer100Chars = 100f;      // 100자당 추가 높이

    public void SetDescription(string content)
    {
        //descriptionText.GetComponent<TextMeshProUGUI>().text = content;
        //descriptionText.text = content;
        
        textbox.text = content;

        float contentLength = content.Length;
        float factor = Mathf.Clamp(contentLength / 100f, 1f, 3f); // 최대 3배까지 확대

        float newHeight = baseHeight * factor;

        // 패널 높이만 조절 (너비는 그대로 유지)
        Vector2 size = backgroundPanel.sizeDelta;
        backgroundPanel.sizeDelta = new Vector2(size.x, newHeight);
    }
}
