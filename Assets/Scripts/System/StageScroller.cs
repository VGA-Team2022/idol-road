using UnityEngine;

/// <summary>�X�e�[�W���X�N���[��������N���X </summary>
public class StageScroller : MonoBehaviour
{
    [SerializeField, Header("�X�N���[���X�s�[�h"), Range(0.01f, 1f)]
    float _scrollSpeed = 1f;
    /// <summary>�X�N���[�����Ă��邩 </summary>
    bool _isScroll = false;

    void Update()
    {
        if (_isScroll)
        {
            var pos = transform.position;
            pos.z -= _scrollSpeed;
            transform.position = pos;
        }
    }

    /// <summary>�X�N���[�����J�n������A�~�߂��肷�� </summary>
    public void ScrollOperation()
    {
        _isScroll = !_isScroll;
    }

    /// <summary>�X�N���[�����蓮�ő��삷�� </summary>
    /// <param name="flag">true=�ړ� false=��~</param>
    public void ScrollOperation(bool flag)
    {
        _isScroll = flag;
    }
}
