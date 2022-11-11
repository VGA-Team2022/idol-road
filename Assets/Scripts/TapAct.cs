using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapAct : MonoBehaviour
{
    [Header("ÉVÅ[Éìñº")]
    [SerializeField] string _sceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
