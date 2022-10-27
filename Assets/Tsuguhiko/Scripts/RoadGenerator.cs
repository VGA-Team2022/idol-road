using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
   [SerializeField] GameObject _generateObjPrefab;
   [SerializeField] float interval = 2.0f;

    [SerializeField] string _objName;

    float time = 0.0f;
    bool isInsitantiated = false;
    int i = 0;
    

    
    void Update()
    {
        // Vector3 point = new Vector3(0,0,0);

        if (!isInsitantiated)
        {
            time += Time.deltaTime;
            if (time >= interval)
            {
                Debug.Log("Checked Interval");
                i++;
                Instantiate(_generateObjPrefab, transform.position + new Vector3(0f, 22.37f, 0f), Quaternion.identity);
                isInsitantiated = true;
            }
        }
        
    }


    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.name == _objName)
        {
            Destroy(_generateObjPrefab);
        }
    }
}
