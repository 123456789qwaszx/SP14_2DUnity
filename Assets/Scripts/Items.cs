using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class Items : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;

    [SerializeField] private GameObject _scaleUP;
    [SerializeField] private GameObject _hpRecovery;
    [SerializeField] private GameObject _speedUP;
    [SerializeField] private GameObject _scoreUP;
    [SerializeField] private GameObject _bigScoreUP;

    public List<ParallaxHandle> parallaxHandles = new List<ParallaxHandle>();

    private float maxSpeed = 8f;
    private float duration = 3f;

    private Coroutine scaleUpCoroutine = null;
    public float MaxSpeed { get { return maxSpeed; } set { maxSpeed = value; } }
    public float Duration { get { return duration; } set { duration = value; } }

    ///////////////////////////////////////////////////////////////////////////////////////////////////

    public void HpRecovery(CharacterController _player, float _duration)
    {
        StartCoroutine(HpRecoveryCoroutine(_player, _duration));
    }
    public void SpeedUp(CharacterController _player, float _speedUp, float _duration)
    {
        StartCoroutine(SpeedUpCoroutine(_player, _speedUp, _duration));
    }

    public void ScaleUp(CharacterController _player, float _duration)
    {
        if (scaleUpCoroutine != null)
        {
            StopCoroutine(scaleUpCoroutine);
        }

        scaleUpCoroutine = StartCoroutine(ScaleUpCoroutine(_player, _duration));
    }
    public void ScoreUp(CharacterController _player, int score)
    {
        StartCoroutine(ScoreUpCoroutine(_player, score));
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////
   
    private IEnumerator HpRecoveryCoroutine(CharacterController _player, float _duration)
    {
        Debug.Log("Hp");
        if (_player.CurrentHp > 0 && _player.CurrentHp < 3)
        {
            Debug.Log(_player.CurrentHp);
            _player.CurrentHp += 1;
            Debug.Log(_player.CurrentHp);
            _hpRecovery.SetActive(false);
        }
        else
        {
            _hpRecovery.SetActive(false);
        }

        yield return new WaitForSeconds(_duration);

        _hpRecovery.SetActive(true);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private IEnumerator ScaleUpCoroutine(CharacterController _player, float _duration)
    {
        _player.transform.localScale = new Vector3(2.0f, 2.0f, 0f);

        // _scaleUP.SetActive(false);

        yield return new WaitForSeconds(_duration);

        ScaleDown(_player);

        scaleUpCoroutine = null;

        _scaleUP.SetActive(true);
    }

    private void ScaleDown(CharacterController _player)
    {
        _player.transform.localScale = new Vector3(1.0f, 1.0f, 0f);
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    private IEnumerator SpeedUpCoroutine(CharacterController _player, float _speedUp, float _duration)
    {
        foreach (ParallaxHandle phUp in parallaxHandles)
        {
            phUp.SetMoveSpeed(_player.CurrentSpeed + _speedUp);
        }
        _speedUP.SetActive(false);

        yield return new WaitForSeconds(_duration);

        foreach (ParallaxHandle phDown in parallaxHandles)
        {
            phDown.SetMoveSpeed(_player.CurrentSpeed);
        }
        _speedUP.SetActive(true);
    }
     private IEnumerator ScoreUpCoroutine(CharacterController _player, int score)
    {
        _player.Score += score;

        if (score == 10)
        {
            _scoreUP.SetActive(false);

            yield return new WaitForSeconds(duration);

            _scoreUP.SetActive(true);
        }
        else if (score == 50)
        {
            _bigScoreUP.SetActive(false);

            yield return new WaitForSeconds(duration);

            _bigScoreUP.SetActive(true);
        }
    }

    private void Start()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("BackGroundLayers");
        foreach (GameObject go in gameObjects)
        {
            parallaxHandles.Add(go.GetComponent<ParallaxHandle>());
        }
        //GameObject[] obObjects = GameObject.FindGameObjectsWithTag("Obstacle");
        //foreach (GameObject go in obObjects)
        //{
        //    ob.Add(go.GetComponent<ObstacleBaseController>());
        //}
    }
}
