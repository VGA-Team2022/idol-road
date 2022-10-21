using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play中のBackGraundスクロール
/// </summary>

public class BGScroll : MonoBehaviour
{
    [Header("道")]
    [SerializeField] GameObject _roadObj;

    [Header("風景")]
    [SerializeField] GameObject _bgObj;

    [Header("モード")]
    [SerializeField] ScrollMode _scrollMode;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
enum ScrollMode
{
    Anime,
    Transform
}
