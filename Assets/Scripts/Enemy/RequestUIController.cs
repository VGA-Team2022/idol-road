using UnityEngine;

/// <summary>�t�@���T�C���X�g�Ɛ����o���C���X�g��ύX����N���X </summary>
public class RequestUIController : MonoBehaviour
{
    [SerializeField, Header("�����o����\������Image")]
    SpriteRenderer _requestWindow = default;
    [SerializeField, Header("�t�@���T�v����\������Image")]
    SpriteRenderer _requestContent = default;
    [Tooltip("�Y���� 0=BAD, 1=GOOD, 2=PERFECT")]
    [SerializeField, Header("�]���ʐ����o���C���X�g"), ElementNames(new string[] { "BAD", "GOOD", "PERFECT" })]
    Sprite[] _requestWindowSprites = default;
    [Tooltip("�Y���� 0=�|�[�Y, 1=�E�B���N, 2=�����L�X, 3=�T�C��")]
    [SerializeField, Header("�t�@���T�v���C���X�g"), ElementNames(new string[] { "�|�[�Y", "�E�B���N", "�����L�X", "�T�C��" })]
    Sprite[] _requestSprites = default;

    /// <summary>�]���ɂ���Đ����o���C���X�g��ύX����</summary>
    public void ChangeRequestWindow(TimingResult result)
    {
        switch (result)
        {
            case TimingResult.Bad:
                _requestWindow.sprite = _requestWindowSprites[0];
                break;
            case TimingResult.Good:
                _requestWindow.sprite = _requestWindowSprites[1];
                break;
            case TimingResult.Perfect:
                _requestWindow.sprite = _requestWindowSprites[2];
                break;
        }
    }

    /// <summary>�t�@���T�v���̃C���X�g��ύX���� </summary>
    public void ChangeRequestImage(FlickType request)
    {
        switch (request)
        {
            case FlickType.Up:      //�|�[�Y
                _requestContent.sprite = _requestSprites[0];
                break;
            case FlickType.Right:   //�E�B���N
                _requestContent.sprite = _requestSprites[1];
                break;
            case FlickType.Down:    //�����L�X
                _requestContent.sprite = _requestSprites[2];
                break;
            case FlickType.Left:    //�T�C��
                _requestContent.sprite = _requestSprites[3];
                break;
        }
    }
}

/// <summary>�t�@���𐁂���΂����^�C�~���O�̕]��</summary>
public enum TimingResult
{
    Bad,
    Good,
    Perfect,
    Out,
}
