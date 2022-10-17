using UnityEngine;

/// <summary>SE���Đ�����N���X </summary>
public class SEPlay : MonoBehaviour
{
    [SerializeField, Header("���͂Ŗ炷SE"), FlickSENames(new string[] { "����͎�", "�����͎�", "�����͎�", "�E���͎�" })]
    AudioClip[] _seClips = default;
    /// <summary>����炷�R���|�[�l���g</summary>
    AudioSource _audio => GetComponent<AudioSource>();

    /// <summary>���͂��ꂽ�����ɂ�����SE��炷 </summary>
    /// <param name="playerInput">�v���C���[�̓���</param>
    public void SEShot(FlickType playerInput)
    {
        _audio.Stop();

        switch (playerInput)
        {
            case FlickType.Up:          //�����
                _audio.PlayOneShot(_seClips[0]);
                break;
            case FlickType.Down:        //������
                _audio.PlayOneShot(_seClips[1]);
                break;
            case FlickType.Left:        //������
                _audio.PlayOneShot(_seClips[2]);
                break;
            case FlickType.Right:       //�E����
                _audio.PlayOneShot(_seClips[3]);
                break;
        }
    }
}
