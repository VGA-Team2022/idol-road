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
    [SerializeField, Header("���ʂ�\������e�L�X�g"), ElementNames(new string[] { "�]��","Bad", "Good", "Perfect", "���v�X�R�A"})]
    TextMeshProUGUI[] _resultValueText = default;
    [SerializeField,Tooltip("�l�̑�������")]
    float _increseTime = 1.0f;
    [SerializeField, Tooltip("���U���g�e�L�X�g��\��������܂ł̎���")]
    float _showResultSpan = 1.0f,_scoreResultSpan = 2.0f;
    [SerializeField, Tooltip("���U���g�]�����ꂼ��̃J�E���g�ƃX�R�A")]
    int _countBad, _countGood, _countPerfect,_countScore;
    ResultManager _result;
    [SerializeField]
    ResultSwitcher _reSwitch;
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
                _resultValueText[0].text = "Result:Perfect";
                break;
            case Result.SuperPerfect:
                _backGround.sprite = _backGroundSprites[0];
                break;
        }
    }
    /// <summary>���ʂ̃e�L�X�g��\������֐�</summary>
    public IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[1].gameObject.SetActive(true);
        DOTween.To(() => _countBad, (r) => _countBad = r, /*_result.CountBad*/50, _increseTime)
            .OnUpdate(() => _resultValueText[1].text = $"Bad:{ /*_result.CountBad*/_countBad}");
        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[2].gameObject.SetActive(true);
        DOTween.To(() => _countGood, (r) => _countGood = r,/*_result.CountGood*/100, _increseTime)
            .OnUpdate(() => _resultValueText[2].text = $"Good:{/*_result.CountGood*/_countGood}");
        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[3].gameObject.SetActive(true);
        DOTween.To(() => _countPerfect, (r) => _countPerfect = r, /*_result.CountPerfect*/150, _increseTime)
            .OnUpdate(() => _resultValueText[3].text = $"Perfect:{ /*_result.CountPerfect */_countPerfect}");
        yield return new WaitForSeconds(_scoreResultSpan);
        _resultValueText[4].gameObject.SetActive(true);
        DOTween.To(() => _countScore, (r) => _countScore = r,/*_reSwitch.Score*/1000, _increseTime)
             .OnUpdate(() => _resultValueText[4].text = $"Score:{ /*_result.CountPerfect */_countScore}");
        yield return null;
    }
}
