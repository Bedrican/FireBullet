using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemySpawner : MonoBehaviour
{
    
    public float xPos, zPos;
    public int eachEnemyCount;
    
    void OnEnable()
    {
        ActivateEnemies();
    }



    public void ActivateEnemies() {
        GameObject tempObject;
        eachEnemyCount = DataManager.Instance._GameDesignSO.eachEnemyCount;
        for (int i = 0; i < DataManager.Instance._GameDesignSO.enemies.Length*eachEnemyCount; i++) {
            tempObject = PoolManager.Instance.GetEnemyFromPool();
            tempObject.SetActive(true);
            xPos = Random.Range(-25, 25);
            zPos = Random.Range(-20, 25);
            tempObject.transform.position = new Vector3(xPos,1.5f,zPos);
            
            
        }
    }
}
