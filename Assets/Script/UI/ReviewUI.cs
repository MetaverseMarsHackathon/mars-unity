using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ReviewUI : MonoBehaviour
{
    public TMP_Text textbox;
    public RectTransform backgroundPanel;
    public float baseHeight = 200f;
    public float heightPer100Chars = 100f;

    public Button endButton; // 버튼 연결 필요
    private Action onEndCallback;

    public void SetDescription(string content)
    {
        textbox.text = content;

        float contentLength = content.Length;
        float factor = Mathf.Clamp(contentLength / 100f, 1f, 3f);
        float newHeight = baseHeight * factor;

        Vector2 size = backgroundPanel.sizeDelta;
        backgroundPanel.sizeDelta = new Vector2(size.x, newHeight);
    }

    public void SetEndCallback(Action callback)
    {
        onEndCallback = callback;

        if (endButton != null)
        {
            endButton.onClick.RemoveAllListeners();
            endButton.onClick.AddListener(() => onEndCallback?.Invoke());
        }
    }
}