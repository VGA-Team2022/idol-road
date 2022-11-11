using UnityEngine;
using CriWare;
using TMPro;

/// <summary>BGM�ESE���Ǘ�����N���X</summary>
[RequireComponent(typeof(CriAtomSource))]
public class AudioManager : MonoBehaviour
{
    CriAtomSource _atomSource => GetComponent<CriAtomSource>();

    /// <summary>�T�E���h�𑀍삷�� </summary>
    public void PlayAndStopSound()
    {
        CriAtomSource.Status status = _atomSource.status;

        if ((status == CriAtomSource.Status.Stop) || (status == CriAtomSource.Status.PlayEnd))
        {
            _atomSource.Play();     //�Đ�����  
        }
        else
        {
            _atomSource.Stop();     //��~����
        }
    }
}
