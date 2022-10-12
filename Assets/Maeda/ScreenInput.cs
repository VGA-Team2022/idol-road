using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class ScreenInput : MonoBehaviour
{
    public enum FlickType
    {
        None,
        Tap,
        Up,
        Down,
        Right,
        Left
    }

    [SerializeField, Tooltip("フリックの判定に必要な操作距離")]
    Vector2 _flickMinRange = new Vector2(0f, 0f);

    FlickType _flickType = FlickType.None;

    Vector2 _StartPosition = new Vector2();

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

        }
        else //スマホ用
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                    case TouchPhase.Moved:
                        _StartPosition = touch.position;
                        break;

                    case TouchPhase.Ended:
                        _EndPosition = touch.position;
                        Frickdirection();
                        break;
                }
            }
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
