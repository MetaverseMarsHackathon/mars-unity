using UnityEngine;

public class ParticleToggle : MonoBehaviour
{
    public ParticleSystem particleSystemToStop;


    private void Start()
    {
        if (particleSystemToStop != null)
        {
            particleSystemToStop.Stop(); // 재생 중지
            
        }
    }
    
}