using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SuperIdolTimeParamater : ScriptableObject
{
    [SerializeField, Header("�Q�[�W���}�b�N�X�ɂȂ��")]
    private int _gaugeCountMax = 10;
    [SerializeField, Header("�X�[�p�[�A�C�h���^�C���̎�������")]
    private float _timeEndSuperIdolTime = 15;
    [SerializeField, Header("�������̃X�R�A��")]
    private float _successScore = 5000;
    /// <summary>�Q�[�W���}�b�N�X�ɂȂ��</summary>
    public int GaugeCountMax => _gaugeCountMax;
    /// <summary>�X�[�p�[�A�C�h���^�C���̎�������</summary>
    public float TimeEndSuperIdolTime => _timeEndSuperIdolTime;
    /// <summary>�������̃X�R�A��</summary>
    public float SuccessScore => _successScore;
}
