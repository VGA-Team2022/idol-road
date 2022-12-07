using UnityEngine;
using UnityEngine.UI;

/// <summary>リザルトシーンのUIを管理・更新するクラス</summary>
public class ResultUIController : MonoBehaviour
{
    [SerializeField, Header("背景(キャラクター)")]
    Image _backGround = default;

    /// <summary>0=神 1=良 2=普通 3=悪 </summary>
    [SerializeField, Header("評価別背景(キャラクター)"), ElementNames(new string[] {"神", "良", "普通", "悪"})]
    Sprite[] _backGroundSprites = default;

    /// <summary>結果によって背景を変更する </summary>
    /// <param name="result"></param>
    public void ChangeResultImage(Result result)
    {
        switch (result)
        {
            case Result.Bad:
                _backGround.sprite = _backGroundSprites[3];
                break;
            case Result.Good:
                _backGround.sprite = _backGroundSprites[2];
                break;
            case Result.Excellent:
                _backGround.sprite = _backGroundSprites[1];
                break;
            case Result.Perfect:
                _backGround.sprite = _backGroundSprites[0];
                break;
        }
    }
}
