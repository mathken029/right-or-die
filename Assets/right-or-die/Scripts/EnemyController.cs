using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//NavMeshAgent���g�����߂ɃC���|�[�g���܂�
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    //��������n�b�V���Ƃ��������ɗ\�ߕϊ����Ă������ƂŁA�����̓x�ɕ����񉻂��s�Ȃ��ł悢�悤�ɂ��ĕ��ׂ��y�����܂�
    //�܂��A������̑ł��ԈႢ�����Ȃ��悤�ɂ��܂�
    private static readonly int AnimationGotHitHash = Animator.StringToHash("GotHit");
    private static readonly int AnimationMovingHash = Animator.StringToHash("Moving");
    private static readonly int AnimationAttackingHash = Animator.StringToHash("Attacking");
    private static readonly int AnimationDeadHash = Animator.StringToHash("Dead");

    /// <Summary>
    /// �ǂ̉����Đ����邩��ݒ肵�܂�
    /// </Summary>
    [SerializeField] private AudioClip _se_attack_hit;
    [SerializeField] private AudioClip _se_death;

    //�����Đ����邽�߂̃R���|�[�l���g�̏����i�[����ϐ��ł�
    private AudioSource _audioSource;

    /// <Summary>
    /// ���̕ϐ��ɑ΂���Unity�̉�ʏ�Ńv���C���[��ݒ肷�邱�ƂŁA�G���v���C���[�Ɍ������܂�
    /// </Summary>
    [SerializeField] private Transform _target;

    /// <Summary>
    /// ���̕ϐ��ɑ΂��ēG�̕����ݒ肷�邱�ƂŁA����ɕt�^���ꂽ�X�N���v�g�̊֐����Ăяo����悤�ɂȂ�܂�
    /// </Summary>
    [SerializeField] private EnemyWeaponController _enemyWeaponController;

    /// <Summary>
    /// �G�̃q�b�g�|�C���g��0�ȉ��ɂȂ邱�ƂœG���|��ă��X�|�[�����܂�
    /// �G�̃q�b�g�|�C���g�𔼌������鏈���Ȃǂ�z�肵�ď����_��������^�ɂ��܂�
    /// </Summary>
    [SerializeField] private float _enemyHitPoint;

    /// <Summary>
    /// ���̕ϐ��ɑ΂��ă^�[�Q�b�g�Ƃ��ăv���C���[���w�肷�邱�ƂœG���v���C���[�Ɍ������܂�
    /// </Summary>
    private NavMeshAgent _agent;

    /// <Summary>
    /// ���̕ϐ��̒��̒l��ύX���邱�ƂőΉ������A�j���[�V�������Đ�����܂�
    /// </Summary>
    private Animator _animator;


    private void Start()
    {
        //�ϐ��Ɏq�R���|�[�l���g�̉����Đ�����R���|�[�l���g���擾���܂�
        //�q�R���|�[�l���g�ɂ��邱�ƂōĐ�������W��ύX���邱�Ƃ��ł��܂�
        _audioSource = GetComponentInChildren<AudioSource>();

        //Animator�̕ϐ��Ɏ������g�i�G�j��ݒ肵�܂�
        _animator = GetComponent<Animator>();

        //NavMeshAgent�̕ϐ��Ɏ������g�i�G�j��ݒ肵�܂�
        _agent = GetComponent<NavMeshAgent>();
    }


    /// <Summary>
    /// �v���C���[�̕��킪�G�{�̂ɐݒ肵��Collider�ɐG���Ƃ̂�����A�j���[�V�������I���ɂ���
    /// </Summary>
    private void OnTriggerEnter(Collider other)
    {
        //���������̂��v���C���[�̕��킩�ǂ����𔻒肵�܂�
        if (other.gameObject.CompareTag("PlayerWeapon"))
        {
            //�G�ɍU�����q�b�g��������炵�܂�
            _audioSource.PlayOneShot(_se_attack_hit);

            //�G�̃q�b�g�|�C���g�����炵�܂�
            _enemyHitPoint--;

            //�G�̃q�b�g�|�C���g�������Ȃ�����|��ă��X�|�[�����܂�
            if (_enemyHitPoint <= 0)
            {
                //�G���|��郂�[�V�������Đ����܂�
                _animator.SetTrigger(AnimationDeadHash);

                //�|��郂�[�V������҂��Ă���G�����ł����܂�
                Destroy(gameObject, 1.3f);
            }

            //�G�̍U���������������Ƃ������p�����[�^�[���I���ɂ��܂�
            _animator.SetTrigger(AnimationGotHitHash);

        }


    }

    /// <Summary>
    /// �U�����[�V�����̓r���ŌĂяo����āA�����蔻��𖳌�������֐����Ăяo���܂�
    /// private�ł��Ăяo����邱�Ƃ͉\�ł�
    /// </Summary>
    private void OnAttackStart()
    {
        _enemyWeaponController.EnableAttack();
    }

    /// <Summary>
    /// �U�����[�V�����̓r���ŌĂяo����āACollider�𖳌������邱�Ƃœ����蔻��������܂�
    /// </Summary>
    private void OnAttackEnd()
    {
        _enemyWeaponController.DisableAttack();
    }

    /// <Summary>
    /// �G���|�ꂽ�Ƃ��ɃA�j���[�V��������Ăяo����鏈�����`���܂�
    /// </Summary>
    private void OnDeath()
    {
        _audioSource.PlayOneShot(_se_death);
    }


    /// <Summary>
    /// �Q�[���̋N�����p�����Ď��s����鏈���ł�
    /// </Summary>
    private void Update()
    {        
        //�v���C���[�̈ʒu�܂ňړ����܂�
        _agent.SetDestination(_target.position);

        //�G������������s�A�j���[�V�������Đ����܂�
        //NavMeshAgent�̕ϐ��̃p�����[�^�ł���velocity.magnitude�����x��\���̂ŁA���ꂪ�����ł���������Ƃ����̂�> 0.1f�Ƃ����`�ŕ\���܂�
        if (_agent.velocity.magnitude > 0.1f)
        {
            _animator.SetBool(AnimationMovingHash, true);
        }
        else
        {
            _animator.SetBool(AnimationMovingHash, false);
        }

        //�v���C���[�ƓG�̋�����NavMeshAgent�Őݒ肵�Ă����~������菭���߂��Ȃ�����G���U�����J�n���܂�
        if (Vector3.Distance(_target.position, _agent.transform.position) < _agent.stoppingDistance + 0.5f)
        {
            //�v���C���[�̈ʒu���玩���̈ʒu���������ƂŁA�G���猩���v���C���[�̈ʒu���Z�o���܂��i���������悭�킩���Ă��Ȃ��j https://gomafrontier.com/unity/2883
            //y�����Œ肷�邱�ƂœG����������Ȃ��悤�ɂ��܂�
            //��transform���ϐ��錾�����Ŏg���闝�R���������
            var direction = _target.position - transform.position;
            direction.y = 0;

            //�G���v���C���[�̕����������悤�ɂ���
            //�U�����������Lerp()�̑�O�����Œ�������
            var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);

            //�U���t���O���I���ɂ���
            _animator.SetBool(AnimationAttackingHash, true);

        }
        else
        {
            //�U���t���O���I�t�ɂ���
            _animator.SetBool(AnimationAttackingHash, false);
        }
    }
}