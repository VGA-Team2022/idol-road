using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlidIn : MonoBehaviour
{
    /// <summary>フェードインさせるためのフェードマネージャー</summary>
    FadeManager2 _fadeManager2;

    /// <summary>
    /// フェードマネージャーを取得
    /// </summary>
    void Start()
    {
        _fadeManager2 = GetComponent<FadeManager2>();
    }

    /// <summary>
    /// フェードインをUpdateで回す
    /// </summary>
    void Update()
    {
        _fadeManager2.FadeInMethod();

        
    }

    
}
