using UnityEngine;

/// <summary>左右のファンを操作するクラス </summary>
[RequireComponent(typeof(Animator))]
public class FanController : MonoBehaviour
{
    /// <summary>アニメーションの個数 </summary>
    const int ANIMATION_COUNT = 3;
    /// <summary>アニメーションを再生するまでの最小待ち時間 </summary>
    [SerializeField,Header("アニメーションを再生するまでの最小待ち時間")]
    float _minNextAnimationTime = 5f;
    /// <summary>アニメーションを再生するまの最大待ち時間 </summary>
    [SerializeField,Header("アニメーションを再生するまの最大待ち時間")]
    float _maxNextAnimationTime = 10f;

    [SerializeField, Header("反転するかどうか")]
    bool _isReverse = default;
    [ElementNames(new string[] { "地雷1", "地雷2", "JK", "イケメン", "メガネ", "壁1男", "壁2男", "壁1女", "壁2女", "強欲" })]
    [SerializeField, Header("ファンイラスト")]
    Sprite[] _fanSprites = default;
    [ElementNames(new string[] { "地雷1", "地雷2", "JK", "イケメン", "メガネ", "壁1男", "壁2男", "壁1女", "壁2女", "強欲" })]
    [SerializeField, Header("ファンイラスト 反転")]
    Sprite[] _fanSpritesReverse = default;

    float _nextAnimationTime = 0f;

    float _timer = 0f;

    SpriteRenderer _spriteRenderer => transform.GetChild(0).GetComponent<SpriteRenderer>();

    Animator _anim => transform.GetComponent<Animator>();

    private void Awake()
    {
        ChangeSprites();

        _nextAnimationTime = Random.Range(_minNextAnimationTime, _maxNextAnimationTime);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_nextAnimationTime <= _timer)
        {
            PlayRandomAnimation();
            _nextAnimationTime = Random.Range(_minNextAnimationTime, _maxNextAnimationTime);
            _timer = 0f;
        }
    }

    /// <summary>イラストを変更する </summary>
    void ChangeSprites()
    {
        var rand = Random.Range(0, _fanSprites.Length);

        if (_isReverse)
        {
            _spriteRenderer.sprite = _fanSpritesReverse[rand];
        }
        else
        {
            _spriteRenderer.sprite = _fanSprites[rand];
        }
    }

    /// <summary>ランダムで一つアニメーションを再生する</summary>
    void PlayRandomAnimation()
    {
        var rand = Random.Range(0, ANIMATION_COUNT);

        switch ((AnimationType)rand)
        {
            case AnimationType.Jump:
                if (_isReverse)
                {
                    _anim.SetTrigger("JumpRight");
                }
                else if(!_isReverse)
                {
                    _anim.SetTrigger("JumpLeft");
                }
                break;
            case AnimationType.Spin:
                if (_isReverse)
                {
                    _anim.SetTrigger("SpinRight");
                    
                }
                else
                {
                    _anim.SetTrigger("SpinLeft");
                }
                break;
            case AnimationType.Move:
                if (_isReverse)
                {
                    _anim.SetTrigger("MoveRight");

                }
                else
                {
                    _anim.SetTrigger("MoveLeft");
                }
                break;
        }
    }

    /// <summary>アニメーションの種類 </summary>
    enum AnimationType
    {
        /// <summary>ジャンプ </summary>
        Jump = 0,
        /// <summary>回転 </summary>
        Spin = 1,
        /// <summary>移動 </summary>
        Move = 2,
    }
}
