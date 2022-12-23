using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{

    /// <Summary>
    /// �A�j���[�V�����̃p�����[�^�̑ł��ԈႢ��h�����߁A�ϐ��Ɋi�[����SetTrigger�ɓn���܂�
    /// </Summary>
    private static readonly int AnimationRepelledHash = Animator.StringToHash("Repelled");

    /// <Summary>
    /// ���̕ϐ��̒��̒l��ύX���邱�Ƃł��̕�������G�̑Ή������A�j���[�V�������Đ�����܂�
    /// </Summary>
    private Animator _animator;

    /// <Summary>
    /// ���̕ϐ��̒��̒l��ύX���邱�Ƃŕ���̓����蔻��̗L���𑀍삵�܂�
    /// </Summary>
    private BoxCollider _boxCollider;

    private void Start()
    {
        //Animator�̕ϐ��ɐe�I�u�W�F�N�g�̓G���g��ݒ肵�܂�
        _animator = GetComponentInParent<Animator>();

        _boxCollider = GetComponentInParent<BoxCollider>();

        //�U�����[�V�������n�܂�܂ł͓����蔻��𖳌������܂�
        DisableAttack();
    }

    /// <Summary>
    /// �����Collider��L���ɂ��܂��B
    /// �F�X�ȃV�`���G�[�V�����Ŏg����悤�ɑ��̃X�N���v�g����Ăяo����悤��public�ɂ��܂��B
    /// </Summary>
    public void EnableAttack()
    {
        _boxCollider.enabled = true;
    }

    /// <Summary>
    /// �����Collider�𖳌��ɂ��܂��B
    /// �F�X�ȃV�`���G�[�V�����Ŏg����悤�ɑ��̃X�N���v�g����Ăяo����悤��public�ɂ��܂��B
    /// </Summary>
    public void DisableAttack()
    {
        _boxCollider.enabled = false;
    }

    /// <Summary>
    /// �v���C���[�̕��킪�G�̕���ɐݒ肵��Collider�ɐG���ƓG���̂�����A�j���[�V�������I���ɂ��܂�
    /// </Summary>
    private void OnCollisionEnter(Collision other)
    {
        //���������̂��v���C���[�̕��킩�ǂ����𔻒肵�܂�
        if (other.gameObject.tag == "PlayerWeapon")
        {
            //�G�̍U���������������Ƃ������p�����[�^�[���I���ɂ��܂�
            //Trigger�̏ꍇ�͎����ŃI�t�ɂȂ邽�߁ABool�̂悤��false�ɂ��鏈���͕K�v����܂���
            _animator.SetTrigger(AnimationRepelledHash);

            //�G�̍U����������I������̂ŁA����̓����蔻����I�t�ɂ��܂�
            DisableAttack();
        }
    }
}
