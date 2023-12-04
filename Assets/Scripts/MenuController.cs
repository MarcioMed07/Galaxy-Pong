using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame(int gameMode)
    {
        GameInfo.gameMode = (GameMode)gameMode;
        SceneManager.LoadSceneAsync(1);
    }
}
