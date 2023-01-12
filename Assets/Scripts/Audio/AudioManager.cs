using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

/// <summary>�T�E���h�֘A���Ǘ�����N���X�@�V���O���g���p�^�[�����g�p</summary>
public class AudioManager : MonoBehaviour
{
    static AudioManager _instance = default;

    /// <summary>�g�p����CriAtomSource�̐� </summary>
    const int SOURCE_COUNT = 3;
    /// <summary>AISAC�l�̑����l </summary>
    const float AISAC_CHANGE_VALUE = 0.1f;
    /// <summary>AISAC�ύX�̑҂����� </summary>
    const float AISAC_WAIT_TIME = 0.1f;

    [ElementNames(new string[] { "BGM", "SE", "VOICE" })]
    [SerializeField, Header("�e�T�E���h�\�[�X"), Tooltip("0=BGM, 1=SE, 2=VOICE")]
    CriAtomSource[] _sources = new CriAtomSource[SOURCE_COUNT];

    /// <summary>�e�\�[�X��ACB�t�@�C�� 0=BGM 1=SE 2=VOICE </summary>
    CriAtomExAcb[] _exAcbs = new CriAtomExAcb[SOURCE_COUNT];

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
            _exAcbs[0] = CriAtom.GetAcb(_sources[(int)Sources.BGM].cueSheet);
            _exAcbs[1] = CriAtom.GetAcb(_sources[(int)Sources.SE].cueSheet);
            _exAcbs[2] = CriAtom.GetAcb(_sources[(int)Sources.VOICE].cueSheet);

            DontDestroyOnLoad(gameObject);
        }
    }
    /// <summary>����̃T�E���h�̍Đ��I�����Ԃ��擾���� </summary>
    /// <param name="sourceIndex">�擾�������T�E���h�̃\�[�X</param>
    /// <param name="cueID">�擾�������T�E���h��ID</param>
    /// <returns>�Đ��I������ -1�͎擾���s</returns>
    long GetAudioPlayEndTime(Sources sourceIndex, int cueID)
    {
        CriAtomEx.CueInfo cueInfo;

        if (!_exAcbs[(int)sourceIndex].GetCueInfo(cueID, out cueInfo))     // CueInfo���ǂݍ��߂Ȃ������ꍇ��-1��Ԃ�
        {
            return -1;
        }

        return cueInfo.length;
    }

    /// <summary>�T�E���h�Đ����I������܂őҋ@���I����A���������s���� </summary>
    /// <param name="waitTime">�ҋ@����</param>
    /// <param name="action">�Đ���̏���</param>
    /// <returns></returns>
    IEnumerator SoundEndAfterExecution(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action?.Invoke();
    }

    /// <summary>�T�E���h�Đ���ɏ��������s����ׂ̊֐� </summary>
    /// <param name="source">�Đ�������\�[�X</param>
    /// <param name="cueID">�Đ�����T�E���h��ID</param>
    /// <param name="action">�Đ���̏���</param>
    public void PlaySoundAfterExecution(Sources source, int cueID, Action action)
    {
        if (source == Sources.BGM)
        {
            PlayBGM(cueID);
        }
        else
        {
            _sources[(int)source].Play(cueID);
        }

        var endTime = (float)GetAudioPlayEndTime(source, cueID) / 1000; //�~���b��b�ɕϊ�����ׂ�1000�Ŋ���

        if (endTime == -1)
        {
            Debug.Log("�Đ��Ɏ��s���܂���");
            return;
        }

        StartCoroutine(SoundEndAfterExecution(endTime, action));
    }
    

    /// <summary>SE���Đ����� </summary>
    /// <param name="cueID">�Đ�������SE��ID</param>
    /// <param name="volume">����</param>
    public void PlaySE(int cueID, float volume = 1f)
    {
        ChangeSEVolume(volume);  //���ʂ𒲐�����
        _sources[(int)Sources.SE].Play(cueID);
    }

    /// <summary>�{�C�X���Đ����� </summary>
    /// <param name="cueID">�Đ��������{�C�X��ID</param>
    /// <param name="volume">����</param>
    public void PlayVoice(int cueID, float volume = 1f)
    {
        ChangeVoiceVolume(volume);  //���ʂ𒲐�����
        _sources[(int)Sources.VOICE].Play(cueID);
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
            var bgm = _sources[(int)Sources.BGM].Play(cueID);     //�ēx�Đ����J�n����
            _playingBGMs.Add(cueID, bgm);
            return;
        }

        var playBGM = _sources[(int)Sources.BGM].Play(cueID);
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

    /// <summary>AISAC�l��ύX����i���s����j </summary>
    public void AISACChangeRun(bool flag, int index)
    {
        AISACValueChange(flag, index);
    }

    /// <summary>AISAC�l��ύX���� </summary>
    IEnumerator AISACValueChange(bool flag, int index)
    {
        if (flag)
        {
            for (var i = 0f; i <= 1f; i += AISAC_CHANGE_VALUE)
            {
                yield return new WaitForSeconds(AISAC_WAIT_TIME);
                _sources[index].SetAisacControl("AisacControl_00", i);
            }
        }
        else
        {
            for (var i = 1f; i <= 0f; i -= AISAC_CHANGE_VALUE)
            {
                yield return new WaitForSeconds(AISAC_WAIT_TIME);
                _sources[index].SetAisacControl("AisacControl_00", i);
            }
        }
    }

    /// <summary>�S�Ă�BGM���ʂ𒲐�����</summary>
    /// <param name="volume">����</param>
    public void ChangeBGMVolume(float volume)
    {
        _sources[(int)Sources.BGM].volume = volume;
    }

    /// <summary>SE�̉��ʂ𒲐�����</summary>
    /// <param name="volume">����</param>
    public void ChangeSEVolume(float volume)
    {
        _sources[(int)Sources.SE].volume = volume;
    }

    /// <summary>Voice�̉��ʂ𒲐�����</summary>
    /// <param name="volume">����</param>
    public void ChangeVoiceVolume(float volume)
    {
        _sources[(int)Sources.VOICE].volume = volume;
    }

    /// <summary>BGM�̍Đ����ꎞ��~����</summary>
    public void PauseBGM()
    {
        _sources[(int)Sources.BGM].Pause(true);
    }

    /// <summary>BGM�̍Đ����ĊJ����</summary>
    public void ResumeBGM()
    {
        _sources[(int)Sources.BGM].Pause(false);
    }
}

/// <summary>�e�T�E���h�\�[�X </summary>
public enum Sources
{
    BGM = 0,
    SE = 1,
    VOICE = 2,
}