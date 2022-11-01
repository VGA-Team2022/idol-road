using UnityEngine;

/// <summary>SEを再生するクラス </summary>
public class SEPlay : MonoBehaviour
{
    [SerializeField, Header("入力で鳴らすSE"), FlickSENames(new string[] { "上入力時", "下入力時", "左入力時", "右入力時" })]
    AudioClip[] _seClips = default;
    /// <summary>音を鳴らすコンポーネント</summary>
    AudioSource _audio => GetComponent<AudioSource>();

    /// <summary>入力された方向にあったSEを鳴らす </summary>
    /// <param name="playerInput">プレイヤーの入力</param>
    public void SEShot(FlickType playerInput)
    {
        _audio.Stop();

        switch (playerInput)
        {
            case FlickType.Up:          //上入力
                _audio.PlayOneShot(_seClips[0]);
                break;
            case FlickType.Down:        //下入力
                _audio.PlayOneShot(_seClips[1]);
                break;
            case FlickType.Left:        //左入力
                _audio.PlayOneShot(_seClips[2]);
                break;
            case FlickType.Right:       //右入力
                _audio.PlayOneShot(_seClips[3]);
                break;
        }
    }
}
