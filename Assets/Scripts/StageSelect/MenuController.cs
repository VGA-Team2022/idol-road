using UnityEngine;

/// <summary>メニューUIを操作する為のクラス </summary>
[RequireComponent(typeof(Animator))]
public class MenuController : MonoBehaviour
{
    [SerializeField, Header("チュートリアル用アニメーター")]
    Animator _tutorialAnimator = default;

    [SerializeField, Header("クレジット用アニメーター")]
    Animator _creditAnimator = default;

    /// <summary>メニュー用アニメーター </summary>
    Animator _menuAnim =>GetComponent<Animator>();

    /// <summary>メニューUIを操作する関数 </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void MenuOperator(bool flag)
    {
        if (flag)   
        {

            _menuAnim.Play("MenuOpen");          //メニューを表示
        }
        else
        {
            _menuAnim.Play("MenuClose");        //メニューを非表示
        }

        AudioManager.Instance.PlaySE(7);　
    }


    /// <summary>遊び方UIを操作する </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void TutorialOperator(bool flag)
    {
        if (flag)
        {
            _tutorialAnimator.Play("TutorialOpen");
            
        }
        else
        {
            _tutorialAnimator.Play("TutorialClose");
           
        }

        AudioManager.Instance.PlaySE(7);　
    }

    /// <summary>クレジットUIを操作する </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void CreditOperator(bool flag)
    {
        if (flag)
        {
            _creditAnimator.Play("CreditOpen");
        }
        else
        {
            _creditAnimator.Play("CreditClose");
        }

        AudioManager.Instance.PlaySE(7);　
    }
}
