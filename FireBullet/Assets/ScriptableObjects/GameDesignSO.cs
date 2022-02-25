using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameDesignPrefs", order = 1)]
public class GameDesignSO : ScriptableObject
{
    [Tooltip("Bullet prefab")]
    public GameObject Bullet;

    
    [Tooltip("Asteroid Objects")]
    public EnemySO[] enemies;

    public float attackRange=5f;
    
    public float playerSpeed= 1.2f;
    [InspectorName("Player rotational speed:")]
    public float playerAngularSpeed = 2f;
    
    public float bulletDamage = 100f;

    public float bulletSpeed = 10f;

    public int eachEnemyCount = 50;

}
