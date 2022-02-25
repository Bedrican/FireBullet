using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    #region Prefab Objects
    public GameObject EndGameUI;

    public GameObject MainMenuUI;

    public Text NumberOfEnemiesText;
    
    #endregion
    
    #region MonoBehaviour Methods

    void Start() {
        GameManager.Instance.playerDeathEvent += RestartMenu;
        MainMenuUI.SetActive(true);
        NumberOfEnemiesText.gameObject.SetActive(false);
        EndGameUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (NumberOfEnemiesText.gameObject.activeInHierarchy) {
            NumberOfEnemiesText.text =;
           
        }*/
        
    }

    #endregion

    #region Custom Methods
    
    public void OnRestartButtonPressed() {
        MainMenuUI.SetActive(true);
        EndGameUI.SetActive(false);
        
       
    }
    public void RestartMenu() {
        MainMenuUI.SetActive(false);
        EndGameUI.SetActive(true);
        NumberOfEnemiesText.gameObject.SetActive(false);
    }

    public void OnPlayButtonPress() {
        NumberOfEnemiesText.gameObject.SetActive(true);
        
        Debug.Log("bastim");
        MainMenuUI.SetActive(false);
        EndGameUI.SetActive(false);
        
        GameManager.Instance.StartGame();
    }
    

    #endregion
   
}
