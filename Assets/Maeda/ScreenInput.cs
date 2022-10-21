using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// フリックの方向やタップかを取得できる
/// </summary>

public class ScreenInput : MonoBehaviour
{
    [SerializeField, Tooltip("フリックの判定に必要な操作距離")]
    Vector2 _flickMinRange = new Vector2(0f, 0f);


    //勝手に追加
    //==========================================
    [SerializeField, Tooltip("audioManager")]
    SEPlay _sePlay = default;
    [SerializeField, Tooltip("入力方向を表示するテキスト")]
    Text _inputText = default;
    [SerializeField]
    GameManager _manager = default;
    //==========================================

    /// <summary>
    /// 現在のフリック方向などを保存する
    /// </summary>
    FlickType _flickType = FlickType.None;

    /// <summary>
    /// 初めに触れた、もしくはスワイプした先を保存する
    /// </summary>
    Vector2 _StartPosition = new Vector2();

    /// <summary>
    /// 指を離した場所を保存する
    /// </summary>
    Vector2 _EndPosition = new Vector2();

    void Update()
    {
        InputFrickGet();
    }

    /// <summary>
    /// 画面を触っているかを取得する
    /// </summary>
    private void InputFrickGet()
    {
        //Unity上でのデバック用
        if (UnityEngine.Application.isEditor)
        {
            //押した場所を保存
            if (Input.GetMouseButtonDown(0))
            {
                ResetParameter();
                _StartPosition = Input.mousePosition;
            }//スワイプした場所を保存
            else if (Input.GetMouseButton(0))
            {
                 _StartPosition = Input.mousePosition;
            }
            //離した場所を保存
            else if (Input.GetMouseButtonUp(0))
            {
                _EndPosition = Input.mousePosition;
                Frickdirection();
            }
            //入力がない場合はリセットする
            else if (_flickType != FlickType.None || _flickType != FlickType.None)
            {
                ResetParameter();
            }
        }
        else //スマホ用
        {   
            //一本以上指が触れた時
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                switch (touch.phase)
                {
                    //押した、もしくはスワイプした場所を保存
                    case TouchPhase.Began:
                        ResetParameter();
                        _StartPosition = touch.position;
                        break;
                    case TouchPhase.Moved:
                        _StartPosition = touch.position;
                        break;
                    //離した場所を保存
                    case TouchPhase.Ended:
                        _EndPosition = touch.position;
                        Frickdirection();
                        break;
                }
            }//入力がない場合はリセットする
            else if (_flickType != FlickType.None || _flickType != FlickType.None)
            {
                ResetParameter();
            }
        }
    }

    /// <summary>
    /// 触れた位置関係からタップかフリックか判定する
    /// </summary>
    private void Frickdirection()
    {
        Vector2 flickVector = new Vector2((new Vector3(_EndPosition.x, 0 , 0) - new Vector3(_StartPosition.x, 0 , 0)).magnitude,
                                          (new Vector3(0 , _EndPosition.y, 0) - new Vector3(0, _StartPosition.y , 0)).magnitude);

        if (flickVector.x <= _flickMinRange.x && flickVector.y <= _flickMinRange.y)
        {
            _flickType = FlickType.Tap;
        }
        else if (flickVector.x > flickVector.y)
        {
            if (Mathf.Sign(_EndPosition.x - _StartPosition.x) > 0)
            {
                _flickType = FlickType.Right;
            }
            else { _flickType = FlickType.Left; }
        }
        else
        {
            if (Mathf.Sign(_EndPosition.y - _StartPosition.y) > 0)
            {
                _flickType = FlickType.Up;
            }
            else { _flickType = FlickType.Down; }
        }


        //勝手に追加
        if (_manager.CurrentEnemy != null)
        {
            _manager.CurrentEnemy.Dead();
            _sePlay.SEShot(_flickType);
        }
       
        _inputText.text = _flickType.ToString();
    }

    /// <summary>
    /// 変数の初期化用
    /// </summary>
    private void ResetParameter()
    {
        _flickType = FlickType.None;
    }

    public FlickType GetFlickType() 
    {
        return _flickType;
    }
}

public enum FlickType
{
    None,
    Tap,
    Up,
    Down,
    Right,
    Left
}
