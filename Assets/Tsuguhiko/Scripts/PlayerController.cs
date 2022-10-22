using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play中のPlayer(カメラ付随)の動作制御
/// </summary>

public class PlayerController : MonoBehaviour
{

    [Header("Playerのアニメーター")]
    [SerializeField] Animator _playerAnimator;

    [SerializeField] GameObject _playerObj;

    [Header("モード")]
    [SerializeField] ScrollMode _scrollMode;

    [Header("スクロールの速さ")]
    [SerializeField]float _scrollSpeed;

    Transform _objPos;
    void Start()
    {
        if (_scrollMode == ScrollMode.Anime)
        {
            StartCoroutine(DelayMove());
        }
        
    }

    IEnumerator DelayMove()
    {
        yield return new WaitForSeconds(4.0f); // カウントダウン用に待機時間を作る

        _playerAnimator.SetBool("isMoving", true);

    }

    
    void Update()
    {
        if (_scrollMode == ScrollMode.Trans)
        {
            _objPos = _playerObj.transform;

            Vector3 movepos = _objPos.position;

            movepos.y -= _scrollSpeed;

            if (movepos.y <= 0.2197596)
            {
                movepos.y = 0.2197596f;

                _objPos.position = movepos;
            }

            _objPos.position = movepos;
        }
    }
}

[Serializable]
enum ScrollMode
{
    Anime,
    Trans
}
