using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class PlayerWeaponController : MonoBehaviour
{
    /// <Summary>
    /// どの音を再生するかを設定します
    /// </Summary>
    [SerializeField] private AudioClip _se_sword_collision;

    //音を再生するためのコンポーネントの情報を格納する変数です
    [SerializeField] private AudioSource _audioSource;

    //パーティクルを発生するためのコンポーネントの情報を格納する変数です
    //[SerializeField] private ParticleSystem _particle;

    //Colliderの操作を行うための変数です
    [SerializeField] private Collider _collider;

    //コントローラーを振動させる際に使用する変数です
    private InputDevice _inputDevice;

    private void Start()
    {

    }

    private void Reset()
    {
        //変数にコンポーネントの情報を取得します
        _audioSource = GetComponent<AudioSource>();

        //パーティクルはプレイヤーの武器のルートコンポーネントではなく子コンポーネントについているため、子から取得する関数を使います
        //_particle = GetComponentInChildren<ParticleSystem>();

        _collider = GetComponent<Collider>();
    }

    /// <Summary>
    /// プレイヤーが武器を持とうとするときに武器の重力を無くして、プレイヤーが勝手に移動しないようにします
    /// </Summary>
    public void OnSelectEnter()
    {
        _collider.isTrigger = true;
    }


    /// <Summary>
    /// 左手のXR Ray InteractorのHover Enteredから呼び出され、武器を持ったのが左手であると設定します
    /// </Summary>
    public void AttachXRNodeLeftHand()
    {
        _inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }

    /// <Summary>
    /// 右手のXR Ray InteractorのHover Enteredから呼び出され、武器を持ったのが右手であると設定します
    /// </Summary>
    public void AttachXRNodeRightHand()
    {
        _inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

    }

    /// <Summary>
    /// 両手のXR Ray InteractorのHover Exitedから呼び出され、武器を離したと設定します
    /// </Summary>
    public void DetachXRNodeHand()
    {
        //LeftEyeがXRNode変数の初期値なので初期値に戻します
        _inputDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftEye);
    }

    /// <Summary>
    /// プレイヤーの武器が敵の武器に設定したColliderに触れると火花を散らせて衝突音を鳴らします
    /// プレイヤーの武器が敵本体に設定したColliderに触れるとコントローラーを振動させます
    /// 移動時にも武器がブレないようにIs Triggerをオンにしているため、衝突判定を取るにはOnTriggerEnterを使用します
    /// </Summary>
    private void OnTriggerEnter(Collider other)
    {
        //当たったのが敵の武器かどうかを判定します
        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            //武器が衝突する音を鳴らします
            _audioSource.PlayOneShot(_se_sword_collision);

            //火花を散らせます
            //_particle.Play();
        }

        //当たったのが敵かどうかを判定します
        if (other.gameObject.CompareTag("Enemy"))
        {
            //コントローラーを振動させます。3つ目の引数が振動させる時間です
            _inputDevice.SendHapticImpulse(0, 0.5f, 0.1f);
        }

    }


}