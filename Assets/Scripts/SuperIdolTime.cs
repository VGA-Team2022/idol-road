using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class SuperIdolTime : MonoBehaviour
{
    /// <summary>�X�[�p�[�A�C�h���^�C�����̃Q�[�W���}�b�N�X�ɂȂ�܂ł̉�</summary>
    [SerializeField]
    private int _gaugeCountMax = 10;
    [SerializeField]
    private Image _imageGauge = default;
    [SerializeField]
    private GameManager _manager = default;

    /// <summary>�X�[�p�[�A�C�h���^�C�����̃^�b�v���ꂽ��</summary>
    private int _gaugeCount = 0;
    /// <summary></summary>
    private float _gaugeLength = 0;

    /// <summary>�Q�[�W�̍ő�l</summary>
    public int GaugeCountMax
    {
        get => _gaugeCountMax;
        set => _gaugeCountMax = value;
    }

    /// <summary>�I�I�IScreenInput�N���X�ŌĂяo�����ƁI�I�I</summary>
    public int GaugeCount
    {
        get => _gaugeCount;
        set
        {
            _gaugeCount = value;
            _gaugeLength = (float)_gaugeCount / _gaugeCountMax;
            if (_imageGauge != null) {
                _imageGauge.fillAmount = _gaugeLength;
            }
            if(_gaugeLength > 1)
            {
                _gaugeLength = 1;
                //Debug.Log("Full");
            }
            //Debug.Log($"max:{_gaugeCountMax},count:{_gaugeCount},gauge:{_gaugeLength}");
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�f�o�b�O�p
        //if (Input.GetButtonDown("Fire1"))
        //{
        //    GaugeCount++;
        //}
    }
    
    public void EndSuperIdolTime()
    {

    }
}
