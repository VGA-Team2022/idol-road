using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class SuperIdolTime : MonoBehaviour
{
    /// <summary>�X�[�p�[�A�C�h���^�C�����̃Q�[�W���}�b�N�X�ɂȂ�܂ł̉�</summary>
    [SerializeField]
    private int _gaugeCountMax = 10;
    /// <summary>�X�[�p�[�A�C�h���^�C���̎�������</summary>
    [SerializeField]
    private float _timeEndSuperIdolTime = 15;
    /// <summary>�X�[�p�[�A�C�h���^�C���̌o�ߎ���</summary>
    [SerializeField]
    private float _elapsed = 0;
    /// <summary>��^�b�v�ŗ��܂�Q�[�W�̕ω��ɂ����鎞��</summary>
    [SerializeField]
    private float _gaugePlusTime = 0.5f;
    [SerializeField]
    private float _imageLange = 5.62f;
    /// <summary>���Ԃ��߂��Ă��邩�̔���</summary>
    [SerializeField]
    private bool _isGaugeMax = false;
    /// <summary>�Q�[�W�����܂肫���Ă��邩�̔���</summary>
    [SerializeField]
    private bool _isTimeMax = false;
    [SerializeField,Tooltip("�Q�[�W�̉摜")]
    private Image _imageGauge = default;
    [SerializeField, Tooltip("�����̉摜")]
    private Image _imageExplosion = default;
    [SerializeField]
    private GameObject _gaugeObject = default;
    /// <summary> �Q�[���}�l�[�W���[</summary>
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

    /// <summary>�Q�[�W�𑝉�������</summary>
    public int GaugeCount
    {
        get => _gaugeCount;
        set
        {
            _gaugeCount = value;
            _gaugeLength = (float)_gaugeCount / _gaugeCountMax;
            if (_imageGauge != null) {
                //�l�b�g�Œ��ׂĎQ�l�ɂ����A�Q�[�W�̒l���Ȃ߂炩�ɕς��鏈��
                var sequence = DOTween.Sequence();
                sequence.Append(_imageGauge.DOFillAmount(_gaugeLength, _gaugePlusTime));
                //GaugeAdvance(_gaugeLength);
            }
            if(_gaugeLength > 1)
            {
                _gaugeLength = 1;
                _isGaugeMax = true;
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
        if (Input.GetButtonDown("Fire1"))
        {
            GaugeCount++;
        }
        if(_isGaugeMax && _isTimeMax)
        {
            EndSuperIdolTime();
            _isGaugeMax= false;
            _gaugeCount = 0;
        }
    }

    private void FixedUpdate()
    {
        _elapsed += Time.deltaTime;
        if(_elapsed > _timeEndSuperIdolTime)
        {
            _isTimeMax = true;
            Debug.Log("�I��");
        }
        Debug.Log($"{(int)_elapsed}�b");
    }

    /// <summary>
    /// ��ؐ搶�̃T���v�����Q�Ƃ����A�Q�[�W�̒l���Ȃ߂炩�ɕς���֐�
    /// </summary>
    /// <param name="value"></param>
    void GaugeAdvance(float value)
    {
        DOTween.To(() => _imageGauge.fillAmount,
            x => _imageGauge.fillAmount = x,
            value,
            _gaugePlusTime);
    }
    public void EndSuperIdolTime()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_imageExplosion.transform.DOScale(new Vector3(_imageLange, _imageLange, _imageLange), 0.5f))
                .Append(_imageExplosion.transform.DOScale(Vector3.zero, 1f));
        Debug.Log("bakuhatu");
        _gaugeObject.SetActive(false);
    }
}
