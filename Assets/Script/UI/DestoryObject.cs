using System;
using UnityEngine;

public class DestoryObject : MonoBehaviour
{
   [SerializeField] private GameObject _gameObject;

   private void Start()
   {
      Destroy(_gameObject);
   }
   
}
