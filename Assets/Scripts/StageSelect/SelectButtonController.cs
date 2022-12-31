using UnityEngine;

/// <summary>�X�e�[�W�I�����s���ׂ̃N���X </summary>
public class SelectButtonController : MonoBehaviour
{
    [SerializeField, Header("�X�^�[�g�{�^���̃A�j���[�^�[")]
    Animator _startAnimator = default;

    /// <summary>�I������Ă���X�e�[�W�I���{�^���̃A�j���[�^�[ </summary>
    Animator _currentButtonAnimator = default;

    /// <summary>���x���I���{�^���������ꂽ���̃A�j���[�V�������Đ����� </summary>
    public void StartSelectLevelAnime(Animator selectAnimator)
    {
        if (_currentButtonAnimator == null)     //���߂ē�Փx���I�����ꂽ���̏���
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
