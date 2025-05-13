using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Items : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    [SerializeField] private GameObject _scaleUP;
    [SerializeField] private GameObject _hpRecovery;
    [SerializeField] private GameObject _speedUP;

    public List<ParallaxHandle> parallaxHandles = new List<ParallaxHandle>();

    bool isItem = false;

    private float maxSpeed = 5f;
    private float duration = 3f;
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float Duration { get { return duration; } set { duration = value; } }

    private void Update()
    {
        float move = 2f;
        transform.position += Vector3.left * move * Time.deltaTime;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void HpRecovery(CharacterController _player)
    {
        // 플레이어 hp받아서 증가
        if (_player.CurrentHp > 0 && _player.CurrentHp < 3)
        {
            _player.CurrentHp += 1;
            Destroy(_hpRecovery);
        }
        else
        {
            Destroy(_hpRecovery);
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

        Destroy(_scaleUP);

        yield return new WaitForSeconds(_duration);

        _player.transform.localScale = originalScale;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    private IEnumerator SpeedUpCoroutine(CharacterController _player, float _speedUp, float _duration)
    {
        
        foreach (ParallaxHandle phUp in parallaxHandles)
        {
            phUp.SetMoveSpeed(_player.CurrentSpeed + _speedUp);
        }

        Destroy(_speedUP);

        yield return new WaitForSeconds(_duration);

        foreach (ParallaxHandle phDown in parallaxHandles)
        {
            phDown.SetMoveSpeed(_player.CurrentSpeed);
        }
    }
    private void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("BackGroundLayers");
        foreach (GameObject go in gameObjects)
        {
            parallaxHandles.Add(go.GetComponent<ParallaxHandle>());
        }
    }
}
