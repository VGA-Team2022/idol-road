using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Playerの動きを止めたり、再開させたりしてアクションをさせるサポートをするスクリプト
/// </summary>
public class PlayerMotion : MonoBehaviour
{
    [SerializeField , Tooltip("アイドルのモーション絵")]
    Sprite[] _sprites;

    [SerializeField, Tooltip("モーションを続ける時間")]
    float _motionTime = 3;

    [SerializeField , Tooltip("入力を送ってくれる")]
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
        else if(_flickType != FlickType.None)
        {
            _timer += Time.deltaTime;
            if (_timer > _motionTime) 
            {
                _flickType = FlickType.None;
                _spriteRenderer.sprite = _sprites[0];
                _timer = 0;
            }
        }
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
