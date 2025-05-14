using UnityEngine;

public class EndStart : MonoBehaviour
{
    [SerializeField] private GameObject endGameObject;
    public void Start()
    {
        endGameObject.SetActive(true);
    }

   
}
