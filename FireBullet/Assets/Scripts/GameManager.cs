using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events

    public delegate void OnPlayerDeath();
    
    public event OnPlayerDeath playerDeathEvent;
    #endregion

    #region Public Fields

    public int enemyCount;

    #endregion

    #region Prefabs

    [SerializeField] private GameObject EnemySpawner;
    
    [SerializeField] private GameObject UIContainer;
    
    [SerializeField] private GameObject Player;
    
    [SerializeField] private GameObject Level;
    
    [SerializeField] private GameObject GameplayManager;


    #endregion

    #region Singleton Implementation

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }
    #endregion

    #region MonoBehavior Methods

    private void Awake()
    {
        _instance = this;
        Player = Instantiate(Player);
        Level = Instantiate(Level);
        GameplayManager = Instantiate(GameplayManager);
        UIContainer = Instantiate(UIContainer);
        Player.SetActive(false);
    }

    private void Start()
    {
        EnemySpawner = Instantiate(EnemySpawner);
        EnemySpawner.SetActive(false);
        
        
    }

    #endregion

    #region Start,Restart and Next level Methods

    public void Restart()
    {
        
        
    }

    public void PlayerDeath()
    {
        EnemySpawner.SetActive(false);
        playerDeathEvent.Invoke();
        Player.SetActive(false);
    }

    public void StartGame()
    {
        
        
        Player.GetComponent<PlayerManager>().InitializePlayer();
        EnemySpawner.SetActive(true);
        Player.SetActive(true);
        
    }
    

    #endregion
}
