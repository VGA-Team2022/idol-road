using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("�t�@������������"),Header("�t�@���֌W")]
    Vector3 _enemySpped;
    [SerializeField, Tooltip("���Y����������邽�߂̎���(�f�o�b�O�p)"), Header("���Y���֌W")]
    float _time = default;
    [SerializeField, Tooltip("���Y������̕b��")]
    float _perfect, _good, _bad , _out;

    /// <summary>�Ƃ肠�����{�^���Ŕ���m�F�ł���悤�ɂ������</summary>
    bool _knockDownEnemy;

    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_enemySpped);�@//�t�@����O�ɓ������i���j

        _knockDownEnemy = false;
    }
    /// <summary> ���Y�����肷�����</summary>
    void Update()
    {
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

    public void DebugOnBottom()
    {
        _knockDownEnemy = true;
    }
}
