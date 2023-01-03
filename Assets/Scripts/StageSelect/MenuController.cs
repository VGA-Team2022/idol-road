using UnityEngine;

/// <summary>メニューUIを操作する為のクラス (アニメーションを再生する)</summary>
public class MenuController : MonoBehaviour
{
    [SerializeField, Header("ストーリーを表示するキャンバス")]
    Animator _storyAnimator = default;
    [SerializeField, Header("クレジットを表示するキャンバス")]
    Animator _creditAnimator = default;
    [SerializeField, Header("チュートリアル用アニメーター")]
    Animator _tutorialAnimator = default;
    [SerializeField, Header("メニュー用アニメーター")]
    Animator _menuAnimator = default;

    ///====以下関数はボタンから呼び出す====

    /// <summary>メニューUIを操作する関数 </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void MenuOperator(bool flag)
    {
        if (flag)   
        {

            _menuAnimator.Play("Open");          //メニューを表示
        }
        else
        {
            _menuAnimator.Play("Close");        //メニューを非表示
        }

        AudioManager.Instance.PlaySE(7);　
    }

    /// <summary>遊び方UIを操作する </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void TutorialOperator(bool flag)
    {
        if (flag)
        {
            _tutorialAnimator.Play("Open");
            
        }
        else
        {
            _tutorialAnimator.Play("Close");
           
        }

        AudioManager.Instance.PlaySE(7);　
    }

    /// <summary>クレジットUIを操作する </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void CreditOperator(bool flag)
    {
        if (flag)
        {
           _creditAnimator.Play("Open");
        }
        else
        {
            _creditAnimator.Play("Close");
        }

        AudioManager.Instance.PlaySE(7);　
    }

    /// <summary>ストーリーUIを操作する </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void StroyOperator(bool flag)
    {
        if (flag)
        {
            _storyAnimator.Play("Open");
        }
        else
        {
            _storyAnimator.Play("Close");
        }

        AudioManager.Instance.PlaySE(7);
    }
}
