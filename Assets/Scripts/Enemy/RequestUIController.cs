using UnityEngine;

/// <summary>ファンサイラストと吹き出しイラストを変更するクラス </summary>
public class RequestUIController : MonoBehaviour
{
    [SerializeField, Header("吹き出しを表示するImage")]
    SpriteRenderer _requestWindow = default;
    [SerializeField, Header("ファンサ要求を表示するImage")]
    SpriteRenderer _requestContent = default;
    [Tooltip("添え字 0=BAD, 1=GOOD, 2=PERFECT")]
    [SerializeField, Header("評価別吹き出しイラスト"), ElementNames(new string[] { "BAD", "GOOD", "PERFECT" })]
    Sprite[] _requestWindowSprites = default;
    [Tooltip("添え字 0=ポーズ, 1=ウィンク, 2=投げキス, 3=サイン")]
    [SerializeField, Header("ファンサ要求イラスト"), ElementNames(new string[] { "ポーズ", "ウィンク", "投げキス", "サイン" })]
    Sprite[] _requestSprites = default;

    /// <summary>評価によって吹き出しイラストを変更する</summary>
    public void ChangeRequestWindow(TimingResult result)
    {
        switch (result)
        {
            case TimingResult.Bad:
                _requestWindow.sprite = _requestWindowSprites[0];
                break;
            case TimingResult.Good:
                _requestWindow.sprite = _requestWindowSprites[1];
                break;
            case TimingResult.Perfect:
                _requestWindow.sprite = _requestWindowSprites[2];
                break;
        }
    }

    /// <summary>ファンサ要求のイラストを変更する </summary>
    public void ChangeRequestImage(FlickType request)
    {
        switch (request)
        {
            case FlickType.Up:      //ポーズ
                _requestContent.sprite = _requestSprites[0];
                break;
            case FlickType.Right:   //ウィンク
                _requestContent.sprite = _requestSprites[1];
                break;
            case FlickType.Down:    //投げキス
                _requestContent.sprite = _requestSprites[2];
                break;
            case FlickType.Left:    //サイン
                _requestContent.sprite = _requestSprites[3];
                break;
        }
    }
}

/// <summary>ファンを吹き飛ばしたタイミングの評価</summary>
public enum TimingResult
{
    Bad,
    Good,
    Perfect,
    Out,
}
