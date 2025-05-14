using UnityEngine;

public class FixSolarPanel : MonoBehaviour
{
    [SerializeField] private Animator panelAnimator;

    // 버튼에서 호출할 함수
    public void FixPanel()
    {
        if (panelAnimator != null)
        {
            panelAnimator.SetBool("IsTrue", true);
        }
    }
}