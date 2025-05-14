using UnityEngine;

public class ParticleToggle : MonoBehaviour
{
    public ParticleSystem particleSystemToStop;

    // 버튼에서 이 함수를 호출하면 파티클이 꺼집니다
    public void StopParticles()
    {
        if (particleSystemToStop != null)
        {
            particleSystemToStop.Stop(); // 재생 중지
            
        }
    }
}