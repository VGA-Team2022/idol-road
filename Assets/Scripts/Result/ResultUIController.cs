using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
/// <summary>���U���g�V�[����UI���Ǘ��E�X�V����N���X</summary>
public class ResultUIController : MonoBehaviour
{
    [SerializeField, Header("�w�i(�L�����N�^�[)")]
    Image _backGround = default;

    /// <summary>0=�_ 1=�� 2=���� 3=�� </summary>
    [SerializeField, Header("�]���ʔw�i(�L�����N�^�[)"), ElementNames(new string[] {"�_", "��", "����", "��"})]
    Sprite[] _backGroundSprites = default;
    [SerializeField, Header("���ʂ�\������e�L�X�g"), ElementNames(new string[] { "�]��","Bad", "Good", "Perfect", "SperPerfect" })]
    TextMeshProUGUI[] _resultValueText = default;
    [SerializeField,Tooltip("�l�̑�������")]
    float _increseTime = 1.0f;
    [SerializeField, Tooltip("���U���g�e�L�X�g��\��������܂ł̎���")]
    float _showResultSpan = 1.0f;
    ResultManager _result;
    private void Start()
    {
        _result = ResultManager.Instance;
        StartCoroutine(ShowResult());
    }
    /// <summary>���ʂɂ���Ĕw�i��ύX���� </summary>
    /// <param name="result"></param>
    public void ChangeResultImage(Result result)
    {
        switch (result)
        {
            case Result.Bad:
                _backGround.sprite = _backGroundSprites[3];
                _resultValueText[0].text = "Result:Bad";
                break;
            case Result.Good:
                _backGround.sprite = _backGroundSprites[2];
                _resultValueText[0].text = "Result:Good";
                break;
            case Result.Perfect:
                _backGround.sprite = _backGroundSprites[1];
                break;
            case Result.SuperPerfect:
                _backGround.sprite = _backGroundSprites[0];
                _resultValueText[0].text = "Result:Perfect";
                break;
        }
    }
    /// <summary>���ʂ̃e�L�X�g��\������֐�</summary>
    public IEnumerator ShowResult()
    {
        
        int count = 0;
        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[1].gameObject.SetActive(true);
        DOTween.To(() => count, (r) => count = r, /*_result.CountBad*/50, _increseTime)
            .OnUpdate(() => _resultValueText[1].text = $"Bad:{ /*_result.CountBad*/count}");
        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[2].gameObject.SetActive(true);
        DOTween.To(() => count, (r) => count = r,/*_result.CountGood*/100, _increseTime)
            .OnUpdate(() => _resultValueText[2].text = $"Good:{/*_result.CountGood*/count}");
        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[3].gameObject.SetActive(true);
        DOTween.To(() => count, (r) => count = r, /*_result.CountPerfect*/150, _increseTime)
            .OnUpdate(() => _resultValueText[3].text = $"Perfect:{ /*_result.CountPerfect */count}");
        /* DOTween.To(() => count, (r) => count = r, _result.CountSperPerfect, _increseTime)
             .OnUpdate(() => _resultValueText[4].text = $"Bad:{ _result.CountBad}");*/
        yield return null;
    }
}
