using UnityEngine;

/// <summary>メニューUIを操作する為のクラス </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>各UIを表示する為のアニメーション名 </summary>
    const string OPEN_UI_ANIME_NAME = "Open";

    /// <summary>各UIを非表示する為のアニメーション名 </summary>
    const string CLOSE_UI_ANIME_NAME = "Close";

    [SerializeField, Header("チュートリアル用アニメーター")]
    Animator _tutorialAnimator = default;

    [SerializeField, Header("クレジット用アニメーター")]
    Animator _creditAnimator = default;

    [SerializeField, Header("メニュー用アニメーター")]
    Animator _menuAnim = default;
 
    /// <summary>メニューUIを操作する関数 </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void MenuOperator(bool flag)
    {
        if (flag)   
        {

            _menuAnim.Play(OPEN_UI_ANIME_NAME);          //メニューを表示
        }
        else
        {
            _menuAnim.Play(CLOSE_UI_ANIME_NAME);        //メニューを非表示
        }

        AudioManager.Instance.PlaySE(7);　
    }


    /// <summary>遊び方UIを操作する </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void TutorialOperator(bool flag)
    {
        if (flag)
        {
            _tutorialAnimator.Play(OPEN_UI_ANIME_NAME);
            
        }
        else
        {
            _tutorialAnimator.Play(CLOSE_UI_ANIME_NAME);
           
        }

        AudioManager.Instance.PlaySE(7);　
    }

    /// <summary>クレジットUIを操作する </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void CreditOperator(bool flag)
    {
        if (flag)
        {
            _creditAnimator.Play(OPEN_UI_ANIME_NAME);
        }
        else
        {
            _creditAnimator.Play(CLOSE_UI_ANIME_NAME);
        }

        AudioManager.Instance.PlaySE(7);　
    }
}
