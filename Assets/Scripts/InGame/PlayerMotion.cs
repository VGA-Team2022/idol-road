using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
///  Playerの動きを止めたり、再開させたりしてアクションをさせるサポートをするスクリプト
/// </summary>
public class PlayerMotion : MonoBehaviour
{
    [SerializeField, Tooltip("アイドルのモーション絵")]
    Sprite[] _sprites;
    [SerializeField, Tooltip("アイドルのファンサ吹き出し"), ElementNames(new string[] { "投げキス", "ウィンク" })]
    GameObject[] _IdleBlowing;
    [SerializeField, Tooltip("吹き出しが出る場所")]
    GameObject _blowingSpawn = default;
    [SerializeField, Tooltip("モーションを続ける時間")]
    float _motionTime = 3;
    [SerializeField, Tooltip("ファンサの吹き出しを表示する時間")]
    float _fansaShowTime = 0.5f;
    [SerializeField, Tooltip("入力を送ってくれる")]
    private ScreenInput _screenInput;

    private SpriteRenderer _spriteRenderer;

    private FlickType _flickType = FlickType.None;

    private bool _gameStop = false;

    private float _timer = 0;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_gameStop) { return; }

        FlickType flickType = _screenInput.GetFlickType();

        if (_flickType != flickType && flickType != FlickType.None && flickType != FlickType.Tap)
        {
            int index = (int)flickType;

            //indexが合わないかも
            _spriteRenderer.sprite = _sprites[index];

            _flickType = flickType;

            _timer = 0;
        }
        else if (_flickType != FlickType.None)
        {
            _timer += Time.deltaTime;
            if (_timer > _motionTime)
            {
                _flickType = FlickType.None;
                _spriteRenderer.sprite = _sprites[0];
                _timer = 0;
            }
        }
        switch (_flickType)
        {
            case FlickType.Down:
                var kissIllust = Instantiate(_IdleBlowing[0], _blowingSpawn.transform);
                Destroy(kissIllust, _fansaShowTime);
                break;
            case FlickType.Right:
                var winkIllust = Instantiate(_IdleBlowing[1], _blowingSpawn.transform);
                Destroy(winkIllust, _fansaShowTime);
                break;
        }
    }

    /// <summary>ゲームクリア時にタクシーに向かわせる処理 </summary>
    public void GameClearMove()
    {
        transform.DOMoveZ(5, 30f);
    }

    // 動きを止めたい時にこの関数を外部に持ってこれるようにメソッド構築
    public void StopMotion()
    {
        _gameStop = true;
    }

    // 動きを再開たい時にこの関数を外部に持ってこれるようにメソッド構築
    public void ResumeMotion()
    {
        _gameStop = false;
    }
}
