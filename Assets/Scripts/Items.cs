using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Items : MonoBehaviour
{
    CharacterController _player;
    Rigidbody2D _rigidbody2D;

    [SerializeField] private SpriteRenderer _scaleUp;
    [SerializeField] private SpriteRenderer _heathREcovery;
    [SerializeField] private SpriteRenderer _speedUp;

    private float maxSpeed = 5f;
    private float duration = 2f;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    private void HeathRecovery()
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
    private void SpeedUp(float _speedUp, float _duration)
    {
        StartCoroutine(SpeedUpCoroutine(_speedUp, _duration));
    }
    private void ScaleUp(float _duration)
    {
        StartCoroutine(ScaleUpCoroutine(_duration));
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    private IEnumerator ScaleUpCoroutine(float _duration)
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

    private IEnumerator SpeedUpCoroutine(float _speedUp, float _duration)
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

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _player = collision.gameObject.GetComponent<CharacterController>();

        if (_player == null) return;

        if (collision.gameObject.CompareTag("HeathRecovery"))
        {
            HeathRecovery();
        }
        else if (collision.gameObject.CompareTag("ScaleUp"))
        {
            ScaleUp(duration);
        }
        else if (collision.gameObject.CompareTag("SpeedUp"))
        {
            SpeedUp(maxSpeed, duration);
        }
        Debug.Log("충돌");
    }
}
