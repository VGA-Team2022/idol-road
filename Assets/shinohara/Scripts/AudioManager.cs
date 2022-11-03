using UnityEngine;
using CriWare;
using TMPro;

/// <summary>BGM・SEを管理するクラス</summary>
[RequireComponent(typeof(CriAtomSource))]
public class AudioManager : MonoBehaviour
{
    CriAtomSource _atomSource => GetComponent<CriAtomSource>();

    /// <summary>サウンドを操作する </summary>
    public void PlayAndStopSound()
    {
        CriAtomSource.Status status = _atomSource.status;

        if ((status == CriAtomSource.Status.Stop) || (status == CriAtomSource.Status.PlayEnd))
        {
            _atomSource.Play();     //再生する  
        }
        else
        {
            _atomSource.Stop();     //停止する
        }
    }
}
