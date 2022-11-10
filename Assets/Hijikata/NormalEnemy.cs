using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("�t�@������������"),Header("�t�@���֌W")]
    Vector3 _enemySpped;
    [SerializeField, Tooltip("�t�@���T��v�����鐔")]
    int _fansaNum = 1;
    [SerializeField, Tooltip("���Y����������邽�߂̎���(�f�o�b�O�p)"), Header("���Y���֌W")]
    float _time = default;
    [SerializeField, Tooltip("���Y������̕b��")]
    float _perfect, _good, _bad , _out;


    /// <summary>�Ƃ肠�����{�^���Ŕ���m�F�ł���悤�ɂ������</summary>
    bool _knockDownEnemy;
    bool _flickJudge;

    Rigidbody _rb;
    List<FlickType> _flickType = new List<FlickType>();
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_enemySpped);�@//�t�@����O�ɓ������i���j

        _knockDownEnemy = false;
        _flickJudge = true;
    }
    /// <summary> ���Y�����肷�����</summary>
    void Update()
    {
        FlickNum();
        if(_knockDownEnemy) 
        {
            _time -= Time.deltaTime;
            if(_time <= _out)
            {
                Debug.Log("out");
            }
            else if (_time <= _perfect)
            {
                Debug.Log($"perfect { _time:F1}");
            }
            else if(_time <= _good)
            {
                Debug.Log($"good { _time:F1}");
            }
            else if(_time <= _bad)
            {
                Debug.Log($"bad { _time:F1}");
            }
        }
    }

    private void FlickNum()
    {
        if(_flickJudge)
        {
            for (int i = 0; i < _fansaNum; i++)�@//�ǂ̃t�@���Ƃ��̂��߂ɔO�̂���for���ŉ�����Ƃ��悤�ɐݒ�(��)
            {
                _flickType.Add((FlickType)Random.Range(2, 5)); //ScreenInput�ŎQ�Ƃ���\��
                Debug.Log(_flickType[i]);
            }
            _flickJudge = false;
        }
    }
    public void DebugOnBottom()
    {
        _knockDownEnemy = true;
    }
}
