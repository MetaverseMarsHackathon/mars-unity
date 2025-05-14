using System;
using UnityEngine;

public class FixSolarPanel : MonoBehaviour
{
    [SerializeField] private Animator panelAnimator;


    private void Start()
    {
        if (panelAnimator != null)
        {
            panelAnimator.SetBool("IsTrue", true);
        }
    }
    
}