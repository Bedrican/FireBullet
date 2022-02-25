using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private bool move;
    private Transform enemy;
    public float returnTime = 2f;
    public float startTime = 0f;
    void Start()
    {
        enemy = PlayerManager.target;
    }

    
    void Update()
    {
        startTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<SimpleEnemyMove>().GetDamage(DataManager.Instance._GameDesignSO.bulletDamage);
            PoolManager.Instance.ReturnBulletToPool(this.gameObject);
            move = false;
            StopAllCoroutines();
        }
        
    }

    public void MoveBullet(Transform attackpoint, Transform target,Vector3 directiona)
    { move = true;
        gameObject.transform.position = attackpoint.transform.position;
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        gameObject.transform.rotation = rotation;
        StartCoroutine(MoveCoroutine(directiona));
    }
    IEnumerator MoveCoroutine(Vector3 direction) {
        while (move) {
            gameObject.transform.Translate(direction*Time.deltaTime*DataManager.Instance._GameDesignSO.bulletSpeed);
            if (startTime >returnTime)
            {
                PoolManager.Instance.ReturnBulletToPool(this.gameObject);
                move = false;
                startTime = 0;
                StopAllCoroutines();
            }
            yield return null;
        }
    }
    
}
