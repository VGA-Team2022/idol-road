using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play����BackGraund�X�N���[��
/// </summary>

public class BGScroll : MonoBehaviour
{
    [Header("��")]
    [SerializeField] GameObject _roadObj;

    [Header("���i")]
    [SerializeField] GameObject _bgObj;

    [Header("���[�h")]
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
