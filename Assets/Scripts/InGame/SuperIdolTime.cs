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
using UnityEngine.Rendering;

public class SuperIdolTime : MonoBehaviour
{
    SuperIdolTimeParamater _paramater => LevelManager.Instance.CurrentLevel.IdolTimeParamater;
    PlayResult _result => PlayResult.Instance;
    [SerializeField, Header("GameManager")]
    GameManager _manager;
    [SerializeField,Header("FadeController")]
    FadeController _fadeController;
    [SerializeField, Header("�X�[�p�[�A�C�h���^�C���̌o�ߎ���")]
    private float _elapsed = 0;
    [SerializeField, Header("��^�b�v�ŗ��܂�Q�[�W�̕ω��ɂ����鎞��")]
    private const float _gaugePlusTime = 0.5f;

    [SerializeField, Header("�����̉摜�̍ő�g��T�C�Y")]
    private float _imageLange = 5.62f;
    [SerializeField,Header("�Q�[�W�����^�����ǂ���")]
    private bool _isGaugeMax = false;

    [SerializeField, Header("�ʏ펞�̃I�u�W�F�N�g"), ElementNames(new string[] {
        "EnemySpawner","CenterSpawnPoint","RightSpawnPoint","LeftSpawnPoint","BossSpawnPoint",
        "ItemGanarator","LeftGeneratePoint","LeftArrivalPoint","RightGeneratePoint","RightArrivalPoint",
        "BackGround","Player","StageObject","BlowingSpawnPosition","InGameCanvas"
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
    private float _downFansValue = 0.3f;
    [SerializeField, Header("���i�̃t�@��"), Tooltip("�Q�[�W�ɍ��킹�ďo�Ă���t�@���̒��i")]
    private GameObject _BackMiddleFans = default;
    [SerializeField, Header("���i�̃t�@�����o�Ă���l")]
    private float _middleFansValue = 0.6f;
    [SerializeField, Header("��i�̃t�@��"), Tooltip("�Q�[�W�ɍ��킹�ďo�Ă���t�@���̉��i")]
    private GameObject _BackUpFans = default;
    [SerializeField, Header("��i�̃t�@�����o�Ă���l")]
    private float _upFansValue = 0.9f;
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
    private IState _currentState = default;
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
    public IState CurrentState
    {
        get => _currentState;
        set 
        {
            _currentState = value;
            Debug.Log(_currentState);
        }
    }
    public bool IsSuperIdolTime => isSuperIdolTime;
    
    private void OnEnable()
    {
        _superIdolTimeBackUI.SetActive(true);
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.Play();
        foreach (var obj in _normalObjects)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (isSuperIdolTime == true)
        //{
        //    //�f�o�b�O�p
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        GaugeCount++;
        //    }
        //}
        _videoPlayer.loopPointReached += EndVideo;
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(_manager.CurrentGameState);
        }
    }
    private void FixedUpdate()
    {
        if (isSuperIdolTime == true)
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _paramater.TimeEndSuperIdolTime)
            {
                _elapsed = 0;
                CheckGauge();
                isSuperIdolTime = false;
                _manager.ChangeGameState(_currentState);
                Debug.Log(_currentState);
            }
        }
    }
    /// <summary>�J�n���ɌĂяo������</summary>
    public void StartSuperIdolTime()
    {
        _gaugeLength = 0;
        _elapsed = 0;
        _gaugeCount = 0;
        _imageGauge.fillAmount = 0;
        _superIdolTimeBackUI.SetActive(true);
        _videoPlayer.Play();
        foreach (var obj in _normalObjects)
        {
            obj.SetActive(false);
        }
    }
    /// <summary>�I�����ɌĂяo������</summary>
    public void InitializationProcess()
    {
        _dancingIdolImage.gameObject.SetActive(false);
        _backGroundPanel.gameObject.SetActive(false);
        _BackDownFans.GetComponent<Animator>().Play("Idol");
        isDownEnemy = false;
        _BackMiddleFans.GetComponent<Animator>().Play("Idol");
        isMiddleEnemy = false;
        _BackUpFans.GetComponent<Animator>().Play("Idol");
        isUpEnemy = false; 
        _isGaugeMax = false;
    }
    /// <summary>�X�[�p�[�A�C�h���^�C���̏I�����ɏ����𔻒f</summary>
    private void CheckGauge()
    {
        if (_isGaugeMax)
        {
            GorgeousEndProcess();
            _result.ValueSuperIdleTimePerfect += _paramater.SuccessScore;
        }
        else if (!_isGaugeMax)
        {
            PlainEndProcess();
        }
    }
    /// <summary>�X�[�p�[�A�C�h���^�C���̃Q�[�W���}�b�N�X�ŏI�������ۂ̍��؂ȏ���</summary>
    private void GorgeousEndProcess()
    {
        //���̃t�@�����΂�
        _BackDownFans.GetComponent<Animator>().Play("Burst");
        _BackMiddleFans.GetComponent<Animator>().Play("Burst");
        _BackUpFans.GetComponent<Animator>().Play("Burst");
        var sequence = DOTween.Sequence();
        sequence.Append(_imageExplosion.transform.DOScale(new Vector3(_imageLange, _imageLange, _imageLange), 0.5f))
                .Append(_imageExplosion.transform.DOScale(Vector3.zero, 1f))
                .OnComplete(() => { SwitchDisplayObject(); });
    }
    /// <summary>�X�[�p�[�A�C�h���^�C���̃Q�[�W�����܂�؂�Ȃ������ۂ̊ȑf�ȏ���</summary>
    private void PlainEndProcess()
    {
        SwitchDisplayObject();
    }
    /// <summary>�I�����̐؂�ւ�����</summary>
    private void SwitchDisplayObject()
    {
        _superIdolTimeFrontUI.SetActive(false);
        _shiningParticle.gameObject.SetActive(false);
        _fadeController.FadeOut(() => 
        {
            _superIdolTimeBackUI.SetActive(false);
            _videoPlayer.gameObject.SetActive(true);
            InitializationProcess();
            this.gameObject.SetActive(false);
            foreach (var obj in _normalObjects)
            {
                obj.SetActive(true);
                var enemies = FindObjectsOfType<EnemyBase>();
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<EnemyBase>().Destroyed();
                }
            }
            _fadeController.FadeIn();
        });
    }
    /// <summary>�^�b�v�Q�[�W�̑����Ɋւ��鏈��</summary>
    private void GaugeIncrease()
    {
        _gaugeLength = (float)_gaugeCount / _paramater.GaugeCountMax;

        if (_imageGauge != null)
        {
            //�Q�[�W�̒l���Ȃ߂炩�ɕς��鏈��
            var sequence = DOTween.Sequence();
            sequence.Append(_imageGauge.DOFillAmount(_gaugeLength, _gaugePlusTime));
        }
        if (_gaugeLength > _downFansValue && isDownEnemy == false)
        {
            isDownEnemy = true;
            _BackDownFans.GetComponent<Animator>().Play("SlideIn");
        }
        if (_gaugeLength > _middleFansValue && isMiddleEnemy == false)
        {
            isMiddleEnemy = true;
            _BackMiddleFans.GetComponent<Animator>().Play("SlideIn");
        }
        if (_gaugeLength > _upFansValue && isUpEnemy == false)
        {
            isUpEnemy = true;
            _BackUpFans.GetComponent<Animator>().Play("SlideIn");
        }
        if (_gaugeLength > 1)
        {
            _gaugeLength = 1;
            _isGaugeMax = true;
        }
    }
    /// <summary>�J�b�g�C���r�f�I���I������u�Ԃ̏���</summary>
    /// <param name="vp"></param>
    private void EndVideo(VideoPlayer vp)
    {
        _superIdolTimeFrontUI.SetActive(true);
        isSuperIdolTime = true;
        _videoPlayer.gameObject.SetActive(false);
        _dancingIdolImage.gameObject.SetActive(true);
        _backGroundPanel.gameObject.SetActive(true);
        _shiningParticle.gameObject.SetActive(true);
        _shiningParticle.Play();
    }
}
