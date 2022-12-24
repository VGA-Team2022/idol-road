using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;
using System.Net;
using Unity.Burst;
using System.Linq;

public class SuperIdolTime : MonoBehaviour
{
    [SerializeField, Header("�Q�[�W���}�b�N�X�ɂȂ��")]
    private int _gaugeCountMax = 10;
    [SerializeField, Header("�X�[�p�[�A�C�h���^�C���̎�������")]
    private float _timeEndSuperIdolTime = 15;
    [SerializeField, Header("�X�[�p�[�A�C�h���^�C���̌o�ߎ���")]
    private float _elapsed = 0;
    [SerializeField, Header("��^�b�v�ŗ��܂�Q�[�W�̕ω��ɂ����鎞��")]
    private float _gaugePlusTime = 0.5f;

    [SerializeField, Header("�����̉摜�̍ő�g��T�C�Y")]
    private float _imageLange = 5.62f;
    /// <summary>���Ԃ��߂��Ă��邩�̔���</summary>
    [SerializeField,Header("�Q�[�W�����^�����ǂ���")]
    private bool _isGaugeMax = false;
    /// <summary>�Q�[�W�����܂肫���Ă��邩�̔���</summary>
    [SerializeField,Header("�������Ԃ𒴂������ǂ���")]
    private bool _isTimeMax = false;

    [SerializeField, Header("�ʏ펞�̃I�u�W�F�N�g"), ElementNames(new string[] {
        "EnemySpawner","CenterSpawnPoint","RightSpawnPoint","LeftSpawnPoint","BossSpawnPoint",
        "ItemGanarator","LeftGeneratePoint","LeftArrivalPoint","RightGeneratePoint","RightArrivalPoint",
        "BackGround","Player","StageObject","BlowingSpawnPosition","InGameCanvas",
        "WarningTimeLine"
    })]
    private GameObject[] _normalObjects = default;

    //�X�[�p�[�A�C�h���^�C�����̃I�u�W�F�N�g
    [SerializeField, Header("�Q�[�W��Image")]
    private Image _imageGauge = default;
    [SerializeField, Header("������Image")]
    private Image _imageExplosion = default;
    [SerializeField, Header("�t�F�[�h�p�̃p�l��")]
    private Image _fadePanel = default;
    [SerializeField, Header("�X�[�p�[�A�C�h���^�C���̌�ʂ�UI")]
    private GameObject _superIdolTimeBackUI = default;
    [SerializeField, Header("�X�[�p�[�A�C�h���^�C���̑O�ʂ�UI")]
    private GameObject _superIdolTimeFrontUI = default;
    [SerializeField, Header("�X�[�p�[�A�C�h���^�C����ʂ̔w�i�G")]
    private GameObject _backGroundPanel = default;
    [SerializeField, Header("���i�̃t�@��"),Tooltip("�Q�[�W�ɍ��킹�ďo�Ă���t�@���̉��i")]
    private GameObject _BackDownFans = default;
    [SerializeField, Header("���i�̃t�@�����o�Ă���l")]
    private float _downFansValue = 0.9f;
    [SerializeField, Header("���i�̃t�@��"), Tooltip("�Q�[�W�ɍ��킹�ďo�Ă���t�@���̒��i")]
    private GameObject _BackMiddleFans = default;
    [SerializeField, Header("���i�̃t�@�����o�Ă���l")]
    private float _middleFansValue = 0.6f;
    [SerializeField, Header("��i�̃t�@��"), Tooltip("�Q�[�W�ɍ��킹�ďo�Ă���t�@���̉��i")]
    private GameObject _BackUpFans = default;
    [SerializeField, Header("��i�̃t�@�����o�Ă���l")]
    private float _upFansValue = 0.3f;
    [SerializeField, Header("�J�b�g�C���p�r�f�I�v���[���[")]
    private VideoPlayer _videoPlayer = default;
    [SerializeField,Header("�A�C�h���̗x���Ă���Image")]
    private Image _dancingIdolImage = default;
    [SerializeField,Header("�L���L���̃G�t�F�N�g")]
    private ParticleSystem _shiningParticle = default;
    /// <summary>�X�[�p�[�A�C�h���^�C�����̃^�b�v���ꂽ��</summary>
    private int _gaugeCount = 0;
    /// <summary>�Q�[�W�̗��܂�</summary>
    private float _gaugeLength = 0;
    private bool isDownEnemy = false;
    private bool isMiddleEnemy = false;
    private bool isUpEnemy = false;
    /// <summary> ���͎�t����</summary>
    private bool isSuperIdolTime = false;
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
            GaugeIncrease();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        _superIdolTimeBackUI.SetActive(true);
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.Play();
        foreach(var obj in _normalObjects)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSuperIdolTime == true)
        {
            //�f�o�b�O�p
            if (Input.GetButtonDown("Fire1"))
            {
                GaugeCount++;
            }
            if (_isGaugeMax && _isTimeMax)
            {
                EndSuperIdolTime();
                _isGaugeMax = false;
                _gaugeCount = 0;
            }
        }
        _videoPlayer.loopPointReached += EndVideo;
    }

  

    private void FixedUpdate()
    {
        if (isSuperIdolTime == true)
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _timeEndSuperIdolTime)
            {
                _isTimeMax = true;
                //Debug.Log("�I��");
            }
            //Debug.Log($"{(int)_elapsed}�b");
        }
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
        _superIdolTimeFrontUI.SetActive(false);
        var sequence = DOTween.Sequence();
        sequence.Append(_imageExplosion.transform.DOScale(new Vector3(_imageLange, _imageLange, _imageLange), 0.5f))
                .Append(_imageExplosion.transform.DOScale(Vector3.zero, 1f))
                .OnComplete(() => { SwitchDisplayObject(); });
        isSuperIdolTime = false;
        //���̃t�@�����΂�
        _BackDownFans.GetComponent<Animator>().Play("Burst");
        _BackMiddleFans.GetComponent<Animator>().Play("Burst");
        _BackUpFans.GetComponent<Animator>().Play("Burst");
        this.gameObject.SetActive(false);
    }
    public void SwitchDisplayObject()
    {
        //_normalCanvas.SetActive(true);
        //_normalPlayer.SetActive(true);
        //_normalRoad.SetActive(true);
        //_normalFan.SetActive(true);
        foreach (var obj in _normalObjects)
        {
            obj.SetActive(true);
        }
        _superIdolTimeBackUI.SetActive(false);
        _fadePanel.DOFade(0, 0.5f);
    }
    public void GaugeIncrease()
    {
        _gaugeLength = (float)_gaugeCount / _gaugeCountMax;
        if (_imageGauge != null)
        {
            //�l�b�g�Œ��ׂĎQ�l�ɂ����A�Q�[�W�̒l���Ȃ߂炩�ɕς��鏈��
            var sequence = DOTween.Sequence();
            sequence.Append(_imageGauge.DOFillAmount(_gaugeLength, _gaugePlusTime));
            //GaugeAdvance(_gaugeLength);
        }
        if (_gaugeLength > _downFansValue && isDownEnemy == false)
        {
            isDownEnemy = true;
            _BackDownFans.GetComponent<Animator>().Play("SlideIn");
            Debug.Log("Down");
        }
        if (_gaugeLength > _middleFansValue && isMiddleEnemy == false)
        {
            isMiddleEnemy = true;
            _BackMiddleFans.GetComponent<Animator>().Play("SlideIn");
            Debug.Log("Middle");
        }
        if (_gaugeLength > _upFansValue && isUpEnemy == false)
        {
            isUpEnemy = true;
            _BackUpFans.GetComponent<Animator>().Play("SlideIn");
            Debug.Log("Up");
        }
        if (_gaugeLength > 1)
        {
            _gaugeLength = 1;
            _isGaugeMax = true;
            //Debug.Log("Full");
            
        }
        //Debug.Log($"max:{_gaugeCountMax},count:{_gaugeCount},gauge:{_gaugeLength}");
    }
    private void EndVideo(VideoPlayer vp)
    {

        _superIdolTimeFrontUI.SetActive(true);
        isSuperIdolTime = true;
        _videoPlayer.gameObject.SetActive(false);
        //_danceIdolPlayer.gameObject.SetActive(true);
        //_danceIdolPlayer.Play();
        _dancingIdolImage.gameObject.SetActive(true);
        _backGroundPanel.gameObject.SetActive(true);
        _shiningParticle.gameObject.SetActive(true);
        _shiningParticle.Play();
        //Debug.Log("endvideo");
    }
}
