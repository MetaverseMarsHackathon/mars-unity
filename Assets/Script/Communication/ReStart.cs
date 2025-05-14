using UnityEngine;
using UnityEngine.SceneManagement;

public class ReStart : MonoBehaviour
{
   
    public void Start()
    {
        SceneManager.LoadScene("EndScense");
    }
}
