using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CriWare;

/// <summary>サウンド関連を管理するクラス　シングルトンパターンを使用</summary>
public class AudioManager : MonoBehaviour
{
    static AudioManager _instance = default;

    /// <summary>使用するCriAtomSourceの数 </summary>
    const int SOURCE_COUNT = 3;
    /// <summary>AISAC値の増減値 </summary>
    const float AISAC_CHANGE_VALUE = 0.1f;
    /// <summary>AISAC変更の待ち時間 </summary>
    const float AISAC_WAIT_TIME = 0.1f;

    [ElementNames(new string[] { "BGM", "SE", "VOICE" })]
    [SerializeField, Header("各サウンドソース"), Tooltip("0=BGM, 1=SE, 2=VOICE")]
    CriAtomSource[] _sources = new CriAtomSource[SOURCE_COUNT];

    /// <summary>各ソースのACBファイル 0=BGM 1=SE 2=VOICE </summary>
    CriAtomExAcb[] _exAcbs = new CriAtomExAcb[SOURCE_COUNT];

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
            _exAcbs[0] = CriAtom.GetAcb(_sources[(int)Sources.BGM].cueSheet);
            _exAcbs[1] = CriAtom.GetAcb(_sources[(int)Sources.SE].cueSheet);
            _exAcbs[2] = CriAtom.GetAcb(_sources[(int)Sources.VOICE].cueSheet);

            DontDestroyOnLoad(gameObject);
        }
    }
    /// <summary>特定のサウンドの再生終了時間を取得する </summary>
    /// <param name="sourceIndex">取得したいサウンドのソース</param>
    /// <param name="cueID">取得したいサウンドのID</param>
    /// <returns>再生終了時間 -1は取得失敗</returns>
    long GetAudioPlayEndTime(Sources sourceIndex, int cueID)
    {
        CriAtomEx.CueInfo cueInfo;

        if (!_exAcbs[(int)sourceIndex].GetCueInfo(cueID, out cueInfo))     // CueInfoが読み込めなかった場合は-1を返す
        {
            return -1;
        }

        return cueInfo.length;
    }

    /// <summary>サウンド再生が終了するまで待機し終了後、処理を実行する </summary>
    /// <param name="waitTime">待機時間</param>
    /// <param name="action">再生後の処理</param>
    /// <returns></returns>
    IEnumerator SoundEndAfterExecution(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action?.Invoke();
    }

    /// <summary>サウンド再生後に処理を実行する為の関数 </summary>
    /// <param name="source">再生させるソース</param>
    /// <param name="cueID">再生するサウンドのID</param>
    /// <param name="action">再生後の処理</param>
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

        var endTime = (float)GetAudioPlayEndTime(source, cueID) / 1000; //ミリ秒を秒に変換する為に1000で割る

        if (endTime == -1)
        {
            Debug.Log("再生に失敗しました");
            return;
        }

        StartCoroutine(SoundEndAfterExecution(endTime, action));
    }
    

    /// <summary>SEを再生する </summary>
    /// <param name="cueID">再生したいSEのID</param>
    /// <param name="volume">音量</param>
    public void PlaySE(int cueID, float volume = 1f)
    {
        ChangeSEVolume(volume);  //音量を調整する
        _sources[(int)Sources.SE].Play(cueID);
    }

    /// <summary>ボイスを再生する </summary>
    /// <param name="cueID">再生したいボイスのID</param>
    /// <param name="volume">音量</param>
    public void PlayVoice(int cueID, float volume = 1f)
    {
        ChangeVoiceVolume(volume);  //音量を調整する
        _sources[(int)Sources.VOICE].Play(cueID);
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
            var bgm = _sources[(int)Sources.BGM].Play(cueID);     //再度再生を開始する
            _playingBGMs.Add(cueID, bgm);
            return;
        }

        var playBGM = _sources[(int)Sources.BGM].Play(cueID);
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

    /// <summary>AISAC値を変更する（実行する） </summary>
    public void AISACChangeRun(bool flag, int index)
    {
        AISACValueChange(flag, index);
    }

    /// <summary>AISAC値を変更する </summary>
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

    /// <summary>全てのBGM音量を調整する</summary>
    /// <param name="volume">音量</param>
    public void ChangeBGMVolume(float volume)
    {
        _sources[(int)Sources.BGM].volume = volume;
    }

    /// <summary>SEの音量を調整する</summary>
    /// <param name="volume">音量</param>
    public void ChangeSEVolume(float volume)
    {
        _sources[(int)Sources.SE].volume = volume;
    }

    /// <summary>Voiceの音量を調整する</summary>
    /// <param name="volume">音量</param>
    public void ChangeVoiceVolume(float volume)
    {
        _sources[(int)Sources.VOICE].volume = volume;
    }

    /// <summary>BGMの再生を一時停止する</summary>
    public void PauseBGM()
    {
        _sources[(int)Sources.BGM].Pause(true);
    }

    /// <summary>BGMの再生を再開する</summary>
    public void ResumeBGM()
    {
        _sources[(int)Sources.BGM].Pause(false);
    }
}

/// <summary>各サウンドソース </summary>
public enum Sources
{
    BGM = 0,
    SE = 1,
    VOICE = 2,
}