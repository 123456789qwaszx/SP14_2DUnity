using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Items : MonoBehaviour
{
    //[SerializeField] private 

    [SerializeField] private SpriteRenderer _scaleUp;
    [SerializeField] private SpriteRenderer _heathREcovery;

    bool posstop = true;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어 찾기
    }

    private void HeathRecovery()
    {
        // 플레이어 hp받아서 증가
    }

    private void ScaleUp(float _duration)
    {
        StartCoroutine(ScaleUpCoroutine(_duration));
    }

    private IEnumerator ScaleUpCoroutine(float _duration)
    {
        //Vector3 originalScale = _target.transform.localScale;

        //_target.transform.localScale = originalScale + new Vector3(1.0f, 1.0f, 0f);

        yield return new WaitForSeconds(_duration);

        //_target.transform.localScale = originalScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        posstop = false;

        if (collision.gameObject.CompareTag("Strawberry"))
        {
            HeathRecovery();
        }
        else if (collision.gameObject.CompareTag("Pineapple"))
        {
            ScaleUp(3);
        }

        Debug.Log("충돌");
    }
}
