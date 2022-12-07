using UnityEngine;
using UnityEngine.UI;

/// <summary>���U���g�V�[����UI���Ǘ��E�X�V����N���X</summary>
public class ResultUIController : MonoBehaviour
{
    [SerializeField, Header("�w�i(�L�����N�^�[)")]
    Image _backGround = default;

    /// <summary>0=�_ 1=�� 2=���� 3=�� </summary>
    [SerializeField, Header("�]���ʔw�i(�L�����N�^�[)"), ElementNames(new string[] {"�_", "��", "����", "��"})]
    Sprite[] _backGroundSprites = default;

    /// <summary>���ʂɂ���Ĕw�i��ύX���� </summary>
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
