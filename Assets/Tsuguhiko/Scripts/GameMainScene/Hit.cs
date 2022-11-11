using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] string _hitName;
    [SerializeField] GameObject _destroyObj;
    void OnTriggerEnter(Collider other)
    {
        if(other.name == _hitName)
        {
            Destroy(_destroyObj);
        }
    }
}
