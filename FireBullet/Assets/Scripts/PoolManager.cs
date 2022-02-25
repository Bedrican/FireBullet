using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    #region Private Fields
    
    // We have list of queues, this is preferred to increase flexibility of the code.
    // If we want to increase our asteroid types, code will automatically incrase list
    private List<Queue<GameObject>> enemiesCreated = new List<Queue<GameObject>>();

    private Queue<GameObject> bulletsCreated = new Queue<GameObject>();
    
    
    //Two parent objects for pools created to have a better organization in hierarchy
    private GameObject enemyParentObject;
    private GameObject bulletParentObject;
  

    #endregion
    
    private static PoolManager _instance;

    public static PoolManager Instance { get { return _instance; } }
    
    void Awake() {
        _instance = this;
    }
    private void Start() {
        int enemyObjectCount = DataManager.Instance._GameDesignSO.enemies.Length;
        int eachEnemyCount = DataManager.Instance._GameDesignSO.eachEnemyCount;
        for (int i = 0; i < enemyObjectCount; i++) {
            //here we are initializing our queues.
            enemiesCreated.Add(new Queue<GameObject>());
        }
        
        enemyParentObject = new GameObject("EnemyPool Parent"); 
        bulletParentObject = new GameObject("BulletPool Parent");
        GameObject tempObject;
        for (int i = 0; i < enemyObjectCount*eachEnemyCount; i++) {

            //we are spawning 10 of each asteroid objects
            tempObject = Instantiate(DataManager.Instance._GameDesignSO.enemies[i/eachEnemyCount].Enemy);
            tempObject.SetActive(false);
            tempObject.transform.SetParent(enemyParentObject.transform);
            enemiesCreated[i/eachEnemyCount].Enqueue(tempObject);
            for (int j=0 ; j< 2 ; j++) {
                tempObject = Instantiate(DataManager.Instance._GameDesignSO.Bullet);
                tempObject.SetActive(false);
                tempObject.transform.SetParent(bulletParentObject.transform);
                bulletsCreated.Enqueue(tempObject);
            }

        }
    }
    public GameObject GetEnemyFromPool(int astIndex = -1) {
        if (astIndex >= DataManager.Instance._GameDesignSO.enemies.Length || astIndex<-1) {
            Debug.Assert((astIndex<DataManager.Instance._GameDesignSO.enemies.Length && astIndex>-1),"Invalid index requested.");
            return null;
        }
        int randIndex = (astIndex==-1) ? Random.Range(0, DataManager.Instance._GameDesignSO.enemies.Length) : astIndex;
        if (enemiesCreated[randIndex].Count==0) {
            ExpandEnemyPool(5);
        }
        return enemiesCreated[randIndex].Dequeue();

    }

    private void ExpandEnemyPool(int expandAmount, int astIndex = -1) {
        if (astIndex >= DataManager.Instance._GameDesignSO.enemies.Length || astIndex<-1) {
            Debug.Assert((astIndex<DataManager.Instance._GameDesignSO.enemies.Length && astIndex>-1),"Invalid index requested.");
            return;
        }
        
        GameObject tempObject;
        if (astIndex == -1) {
            for (int i = 0; i < DataManager.Instance._GameDesignSO.enemies.Length*expandAmount; i++) {
                
                tempObject = Instantiate(DataManager.Instance._GameDesignSO.enemies[i/expandAmount].Enemy);
                tempObject.SetActive(false);
                tempObject.transform.SetParent(enemyParentObject.transform);
                enemiesCreated[i/expandAmount].Enqueue(tempObject);

            }
        }
        else {
            for (int i = 0; i < expandAmount; i++) {
                
                tempObject = Instantiate(DataManager.Instance._GameDesignSO.enemies[astIndex].Enemy);
                tempObject.SetActive(false);
                tempObject.transform.SetParent(enemyParentObject.transform);
                enemiesCreated[astIndex].Enqueue(tempObject);

            }
        }
        return;
            
        
    }
    public void ReturnEnemyToPool(GameObject enemy, int astIndex) {
        /*if (enemy.GetComponent<EnemyController>() == null) {
            Debug.LogError("Non-asteroid objects cannot return to the pool.");
            return;
        }*/
        
        enemy.SetActive(false);
        enemiesCreated[astIndex].Enqueue(enemy);
    }
    public GameObject GetBulletFromPool() {
        /*if (bulletsCreated.Count<=0) {
            ExpandBulletPool(10);
        }*/
        return bulletsCreated.Dequeue();
    }

    public void ReturnBulletToPool(GameObject bullet) {
        /*if (bullet.GetComponent<BulletController>() == null) {
            Debug.LogError("Non-bullet components cant return to bullet pool.");
            return;
        }*/
        bullet.SetActive(false);
        bullet.transform.position = Vector3.zero;
        bullet.transform.eulerAngles = Vector3.zero;
        bulletsCreated.Enqueue(bullet);
        return;
    }
}
