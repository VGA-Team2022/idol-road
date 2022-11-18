using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapAct : MonoBehaviour
{
    [Header("ÉVÅ[Éìñº")]
    [SerializeField] string _sceneName;

    FadeManager2 _fadeManager2;

    bool _isPushed = false;
    void Start()
    {
        _fadeManager2= GetComponent<FadeManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isPushed= true;
        }

        if (_isPushed == true)
        {
            _fadeManager2.FadeOutMethod();

            StartCoroutine(DelayChange());
        }
    }

    IEnumerator DelayChange()
    {
        yield return new WaitForSeconds(5.0f);

        _fadeManager2.ChangeScene(_sceneName);
    }
    
}
