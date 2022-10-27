using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play中のPlayer(カメラ付随)の動作制御
/// </summary>

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject _playerObj;

    //[Header("モード")]
    //[SerializeField] ScrollMode _scrollMode;

    [Header("スクロールの速さ")]
    [SerializeField] float _scrollSpeed;

    Transform _objPos;

    PlayerMotion playerMotion;

    // カウントダウン終了時にUpdate関数を回す判定
    bool _isUpdated = false;

    public float ScrollSpeed { get { return _scrollSpeed; } set { _scrollSpeed = value; } }

    void Start()
    {
        playerMotion = GetComponent<PlayerMotion>();

        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(4.0f);

        _isUpdated = true;
    }
    void Update()
    {
        if (_isUpdated)
        {
            _objPos = this.transform;

            Vector3 movepos = _objPos.position;

            movepos.y += _scrollSpeed;

            //if (movepos.y >= 9.062126f)
            //{
            //    isPassed = true;

            //    _objPos.position = movepos;
            //}

            _objPos.position = movepos;

            // ボタン操作をした時に外部の関数を実行できるように設定
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerMotion.StopMotion();
            }
            // ボタン操作をした時に外部の関数を実行できるように設定
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerMotion.ResumeMotion();
            }
        }
    }
}

//[Serializable]
//enum ScrollMode
//{
//    Anime,
//    Trans
//}
