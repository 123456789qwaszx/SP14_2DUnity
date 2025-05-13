using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Items : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    [SerializeField] private SpriteRenderer _scaleUp;
    [SerializeField] private SpriteRenderer _heathREcovery;
    [SerializeField] private SpriteRenderer _speedUp;

    private float maxSpeed = 5f;
    private float duration = 2f;
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float Duration { get { return duration; } set { duration = value; } }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public void HpRecovery(CharacterController _player)
    {
        // 플레이어 hp받아서 증가
        if (_player.CurrentHp > 0 && _player.CurrentHp < 3)
        {
            _player.CurrentHp += 1;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    public void SpeedUp(CharacterController _player, float _speedUp, float _duration)
    {
        StartCoroutine(SpeedUpCoroutine(_player, _speedUp, _duration));
    }
    public void ScaleUp(CharacterController _player, float _duration)
    {
        StartCoroutine(ScaleUpCoroutine(_player, _duration));
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    private IEnumerator ScaleUpCoroutine(CharacterController _player, float _duration)
    {
        Vector3 originalScale = _player.transform.localScale;

        _player.transform.localScale = originalScale + new Vector3(1.0f, 1.0f, 0f);

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(_duration);

        _player.transform.localScale = originalScale;

        Destroy(gameObject);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    private IEnumerator SpeedUpCoroutine(CharacterController _player, float _speedUp, float _duration)
    {
        // x 값 속도 상승
        _player.CurrentSpeed = 5f;
        float originSpeed = _player.CurrentSpeed;

        _player.CurrentSpeed += _speedUp;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(_duration);

        _player.CurrentSpeed = originSpeed;

        Destroy(gameObject);
    }
}
