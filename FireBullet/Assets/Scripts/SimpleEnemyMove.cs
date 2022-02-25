using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMove : MonoBehaviour
{
    public GameObject target;
    public float health;
    public int a;
    void Start()
    {
        GameManager.Instance.playerDeathEvent += PlayerDeath;
        target = GameObject.Find("Player(Clone)");
        if (gameObject.name == "BigEnemy(Clone)")
        {
            a = 1;
        }
        else
        {
            a = 0;
        }
        health = DataManager.Instance._GameDesignSO.enemies[a].health;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position,target.transform.position,3f*Time.deltaTime);
    }

    public void GetDamage(float damage)
    {
        if ((health - damage) >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        AmIDead();
    }
    void AmIDead()
    {
        if (health <= 0)
        {
            PoolManager.Instance.ReturnEnemyToPool(this.gameObject,a);
        }
    }
    public void PlayerDeath() {
        if (this.gameObject.activeSelf) {
            PoolManager.Instance.ReturnEnemyToPool(this.gameObject , a);
        }
        
    }
}
