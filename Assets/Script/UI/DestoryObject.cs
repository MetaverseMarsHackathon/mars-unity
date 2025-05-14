using UnityEngine;

public class DestoryObject : MonoBehaviour
{
   [SerializeField] private GameObject _gameObject;
   
   public void destoryobject()
   {
      Destroy(_gameObject);
   }
}
