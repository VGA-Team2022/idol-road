using System.Collections.Generic;
using UnityEngine;
using CriWare;

/// <summary>�T�E���h�֘A���Ǘ�����N���X�@�V���O���g���p�^�[�����g�p</summary>
public class AudioManager : MonoBehaviour
{
    static AudioManager _instance = default; 

    [SerializeField, Header("BGM")]
    CriAtomSource _bgmSource = default;
    [SerializeField, Header("SE")]
    CriAtomSource _seSource = default;
    [SerializeField, Header("Voice")]
    CriAtomSource _voiceSource = default;

    /// <summary>�Đ�����BGM���Ǘ�����f�B�N�V���i�� key=�L���[ID, value=���</summary>
    Dictionary<int, CriAtomExPlayback> _playingBGMs = new Dictionary<int, CriAtomExPlayback>();

    public static AudioManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>SE���Đ����� </summary>
    /// <param name="cueID">�Đ�������SE��ID</param>
    /// <param name="volume">����</param>
    public void PlaySE(int cueID, float volume = 1f)
    {
        ChangeSEVolume(volume);  //���ʂ𒲐�����
        _seSource.Play(cueID);
    }

    /// <summary>�{�C�X���Đ����� </summary>
    /// <param name="cueID">�Đ��������{�C�X��ID</param>
    /// <param name="volume">����</param>
    public void PlayVoice(int cueID, float volume = 1f)
    {
        ChangeVoiceVolume(volume);  //���ʂ𒲐�����
        _voiceSource.Play(cueID);
    }

    /// <summary>BGN���Đ����� </summary>
    /// <param name="cueID">�Đ�������SE��ID</param>
    /// <param name="volume">����</param>
    public void PlayBGM(int cueID, float volume = 1f)
    {
        ChangeBGMVolume(volume);

        if (_playingBGMs.ContainsKey(cueID))    //���ɍĐ���
        {
            StopBGM(cueID);            //��x�Đ����~�߂�
            var bgm = _bgmSource.Play(cueID);     //�ēx�Đ����J�n����
            _playingBGMs.Add(cueID, bgm);
            return;
        }

        var playBGM = _bgmSource.Play(cueID);
        _playingBGMs.Add(cueID, playBGM);  //�f�B�N�V���i���ɒǉ�
    }

    /// <summary>BGM���~�߂� </summary>
    /// <param name="cueID">�~�߂���BGM�̃L���[ID</param>
    public void StopBGM(int cueID)
    {
        if (_playingBGMs.ContainsKey(cueID))
        {
            var bgm = _playingBGMs[cueID];
            bgm.Stop();
            _playingBGMs.Remove(cueID);     //�f�B�N�V���i������폜
        }
        else
        {
            Debug.Log($"�w�肳�ꂽCueID�͍Đ�����Ă��܂���B CueID {cueID}");
        }
    }

    /// <summary>�t�@���T���s������SE���Đ����� </summary>
    /// <param name="flickType">�t�@���T</param>
    /// <param name="volum">����</param>
    public void PlayRequestSE(FlickType flickType, float volum = 1f)
    {
        switch (flickType)
        {
            case FlickType.Up:  //�|�[�Y
                PlaySE(3, volum);
                break;          //�E�B���N
            case FlickType.Right:
                PlaySE(5, volum);
                break;          //�����L�X
            case FlickType.Down:
                PlaySE(1, volum);
                break;          //�T�C��
            case FlickType.Left:
                PlaySE(4, volum);
                break;
                    
        }
    }

    /// <summary>�S�Ă�BGM���ʂ𒲐�����</summary>
    /// <param name="volume">����</param>
    public void ChangeBGMVolume(float volume)
    {
        _bgmSource.volume = volume;
    }

    /// <summary>SE�̉��ʂ𒲐�����</summary>
    /// <param name="volume">����</param>
    public void ChangeSEVolume(float volume)
    {
        _seSource.volume = volume;
    }

    /// <summary>Voice�̉��ʂ𒲐�����</summary>
    /// <param name="volume">����</param>
    public void ChangeVoiceVolume(float volume)
    {
        _voiceSource.volume = volume;
    }

    /// <summary>BGM�̍Đ����ꎞ��~����</summary>
    public void PauseBGM()
    {
        _bgmSource.Pause(true);
    }

    /// <summary>BGM�̍Đ����ĊJ����</summary>
    public void ResumeBGM()
    {
        _bgmSource.Pause(false);
    }
}