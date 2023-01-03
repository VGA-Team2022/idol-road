using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>遊び方UIを管理するクラス </summary>
public class TutorialManager : MonoBehaviour
{
    [SerializeField, Tooltip("キャンバスを閉じる為のボタン")]
    Button _closeButton = default;

    Animator _anim => GetComponent<Animator>();

    /// <summary>
    /// ストーリーUIを操作する
    /// ボタンから呼び出す
    /// </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void TutorialOperator(bool flag)
    {
        if (flag)
        {
            _anim.Play("Open");
        }
        else
        {
            _anim.Play("Close");
        }

        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>閉じるボタンを押した時に実行したい処理を追加する関数 </summary>
    /// <param name="action"></param>
    public void CloseButtonAddListener(UnityAction action)
    {
        _closeButton.onClick.AddListener(action);
        _closeButton.onClick.AddListener(() => _closeButton.onClick.RemoveAllListeners());  //一度だけ実行させる為に、ボタンが押されたら登録されている処理を全て削除する
    }
}
