using System.Collections.Generic;
using UnityEngine;
using CriWare;

/// <summary>サウンド関連を管理するクラス　シングルトンパターンを使用</summary>
public class AudioManager : MonoBehaviour
{
    static AudioManager _instance = default; 

    [SerializeField, Header("BGM")]
    CriAtomSource _bgmSource = default;
    [SerializeField, Header("SE")]
    CriAtomSource _seSource = default;
    [SerializeField, Header("Voice")]
    CriAtomSource _voiceSource = default;

    /// <summary>再生中のBGMを管理するディクショナリ key=キューID, value=状態</summary>
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

    /// <summary>SEを再生する </summary>
    /// <param name="cueID">再生したいSEのID</param>
    /// <param name="volume">音量</param>
    public void PlaySE(int cueID, float volume = 1f)
    {
        ChangeSEVolume(volume);  //音量を調整する
        _seSource.Play(cueID);
    }

    /// <summary>ボイスを再生する </summary>
    /// <param name="cueID">再生したいボイスのID</param>
    /// <param name="volume">音量</param>
    public void PlayVoice(int cueID, float volume = 1f)
    {
        ChangeVoiceVolume(volume);  //音量を調整する
        _voiceSource.Play(cueID);
    }

    /// <summary>BGNを再生する </summary>
    /// <param name="cueID">再生したいSEのID</param>
    /// <param name="volume">音量</param>
    public void PlayBGM(int cueID, float volume = 1f)
    {
        ChangeBGMVolume(volume);

        if (_playingBGMs.ContainsKey(cueID))    //既に再生中
        {
            StopBGM(cueID);            //一度再生を止める
            var bgm = _bgmSource.Play(cueID);     //再度再生を開始する
            _playingBGMs.Add(cueID, bgm);
            return;
        }

        var playBGM = _bgmSource.Play(cueID);
        _playingBGMs.Add(cueID, playBGM);  //ディクショナリに追加
    }

    /// <summary>BGMを止める </summary>
    /// <param name="cueID">止めたいBGMのキューID</param>
    public void StopBGM(int cueID)
    {
        if (_playingBGMs.ContainsKey(cueID))
        {
            var bgm = _playingBGMs[cueID];
            bgm.Stop();
            _playingBGMs.Remove(cueID);     //ディクショナリから削除
        }
        else
        {
            Debug.Log($"指定されたCueIDは再生されていません。 CueID {cueID}");
        }
    }

    /// <summary>ファンサを行った時SEを再生する </summary>
    /// <param name="flickType">ファンサ</param>
    /// <param name="volum">音量</param>
    public void PlayRequestSE(FlickType flickType, float volum = 1f)
    {
        switch (flickType)
        {
            case FlickType.Up:  //ポーズ
                PlaySE(3, volum);
                break;          //ウィンク
            case FlickType.Right:
                PlaySE(5, volum);
                break;          //投げキス
            case FlickType.Down:
                PlaySE(1, volum);
                break;          //サイン
            case FlickType.Left:
                PlaySE(4, volum);
                break;
                    
        }
    }

    /// <summary>全てのBGM音量を調整する</summary>
    /// <param name="volume">音量</param>
    public void ChangeBGMVolume(float volume)
    {
        _bgmSource.volume = volume;
    }

    /// <summary>SEの音量を調整する</summary>
    /// <param name="volume">音量</param>
    public void ChangeSEVolume(float volume)
    {
        _seSource.volume = volume;
    }

    /// <summary>Voiceの音量を調整する</summary>
    /// <param name="volume">音量</param>
    public void ChangeVoiceVolume(float volume)
    {
        _voiceSource.volume = volume;
    }

    /// <summary>BGMの再生を一時停止する</summary>
    public void PauseBGM()
    {
        _bgmSource.Pause(true);
    }

    /// <summary>BGMの再生を再開する</summary>
    public void ResumeBGM()
    {
        _bgmSource.Pause(false);
    }
}