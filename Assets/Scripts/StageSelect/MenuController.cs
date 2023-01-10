using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>メニューUIを操作する為のクラス (アニメーションを再生する)</summary>
public class MenuController : MonoBehaviour
{
    [SerializeField, Header("ストーリーを強制表示させるまでの待機時間")]
    float _stroyWaitTime = 2f;
    [SerializeField, Header("遊び方を強制表示させるまでの待機時間")]
    float _tutorialWaitTime = 1f;

    [SerializeField, Header("ストーリーを表示するキャンバス")]
    StoryPrinter _storyPrinter = default;
    [SerializeField, Header("クレジットを表示するキャンバス")]
    Animator _creditAnimator = default;
    [SerializeField, Header("チュートリアル用アニメーター")]
    TutorialManager _tutorialManager = default;
    [SerializeField, Header("メニュー用アニメーター")]
    Animator _menuAnimator = default;
    [SerializeField, Tooltip("フェード行うクラス")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("最初にストーリーと遊び方を表示している間の背景")]
    Image _firstBackGround = default;
    /// <summary>初めてゲームをプレイしたかどうか </summary>
    static bool _firstPlay = true;

    private IEnumerator Start()
    {
        if (!_firstPlay)
        {
            AudioManager.Instance.PlayBGM(14, 0.5f);
            _storyPrinter.CurrentMode = ShowMode.Title;
            _fadeController.FadeIn();
            yield break;
        }

        _firstBackGround.gameObject.SetActive(true);
        yield return new WaitForSeconds(_stroyWaitTime);
        _storyPrinter.StroyOperator(true);

        _storyPrinter.StorySkip += () => StartCoroutine(WaitTutorialUI());      //スキップ処理を追加       
    }

    /// <summary>遊び方を強制表示させる </summary>
    /// <returns></returns>
    IEnumerator WaitTutorialUI()
    {
        yield return new WaitForSeconds(_tutorialWaitTime);
        _tutorialManager.TutorialOperator(true);
        _tutorialManager.CloseButtonAddListener(() =>
        {
            _firstPlay = false;
            _firstBackGround.gameObject.SetActive(false);
            _fadeController.FadeIn();
        });
    }

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
}
