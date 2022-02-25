using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public GameDesignSO _GameDesignSO;
    private static DataManager _instance;
    public static DataManager Instance {
        get { return _instance; }
    }

    private void Awake() {
        _instance = this;
    }
}
