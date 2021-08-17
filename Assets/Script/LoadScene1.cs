using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene1 : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(GameDataManager.Instance.gameDataScrObj.level);
    }
}
