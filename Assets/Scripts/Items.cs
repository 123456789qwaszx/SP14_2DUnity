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

    bool posstop = true;

    float originSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
        _player = GetComponent<CharacterController>();
    }

    private void HeathRecovery()
    {
        // 플레이어 hp받아서 증가
        if (_player.CurrentHp > 0 && _player.CurrentHp < 3)
        {
            _player.CurrentHp += 1;
        }
        else
        {
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
    private IEnumerator ScaleUpCoroutine(float _duration)
    {
        Vector3 originalScale = _player.transform.localScale;

        _player.transform.localScale = originalScale + new Vector3(1.0f, 1.0f, 0f);

        yield return new WaitForSeconds(_duration);

        _player.transform.localScale = originalScale;
    }
    private IEnumerator SpeedUpCoroutine(float _speedUp, float _duration)
    {
        _player.CurrentSpeed = 5f;
        originSpeed = _player.CurrentSpeed;

        _player.CurrentSpeed += _speedUp;

        yield return new WaitForSeconds(_duration);

        _player.CurrentSpeed = originSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        posstop = false;

        if (collision.gameObject.CompareTag("HeathRecovery"))
        {
            HeathRecovery();
        }
        else if (collision.gameObject.CompareTag("ScaleUp"))
        {
            ScaleUp(3);
        }
        else if (collision.gameObject.CompareTag("ScaleUp"))
        {
            SpeedUp(5, 3);
        }

        Debug.Log("충돌");
    }
}
