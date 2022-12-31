using UnityEngine;

/// <summary>ステージ選択を行う為のクラス </summary>
public class SelectButtonController : MonoBehaviour
{
    [SerializeField, Header("スタートボタンのアニメーター")]
    Animator _startAnimator = default;

    /// <summary>選択されているステージ選択ボタンのアニメーター </summary>
    Animator _currentButtonAnimator = default;

    /// <summary>レベル選択ボタンが押された時のアニメーションを再生する </summary>
    public void StartSelectLevelAnime(Animator selectAnimator)
    {
        if (_currentButtonAnimator == null)     //初めて難易度が選択された時の処理
        {
            _startAnimator.gameObject.SetActive(true);
            _startAnimator.Play("In");

            _currentButtonAnimator = selectAnimator;
            _currentButtonAnimator.Play("Select");
            return;
        }

        _currentButtonAnimator.Play("Deselect");
        _currentButtonAnimator = selectAnimator;
        _currentButtonAnimator.Play("Select");
    }
}
