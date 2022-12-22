using UnityEngine;

/// <summary>ステージをスクロールさせるクラス </summary>
public class StageScroller : MonoBehaviour
{
    [SerializeField, Header("スクロールスピード"), Range(0.01f, 1f)]
    float _scrollSpeed = 1f;
    /// <summary>スクロールしているか </summary>
    bool _isScroll = false;

    void Update()
    {
        if (_isScroll)
        {
            var pos = transform.position;
            pos.z -= _scrollSpeed;
            transform.position = pos;
        }
    }

    /// <summary>スクロールを開始したり、止めたりする </summary>
    public void ScrollOperation()
    {
        _isScroll = !_isScroll;
    }

    /// <summary>スクロールを手動で操作する </summary>
    /// <param name="flag">true=移動 false=停止</param>
    public void ScrollOperation(bool flag)
    {
        _isScroll = flag;
    }
}
