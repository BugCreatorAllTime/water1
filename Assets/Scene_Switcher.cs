
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Switcher : MonoBehaviour
{
  public static Scene_Switcher InstanceOfScene_Switcher;
  public void playGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void BackScene1(){
      SceneManager.LoadScene("Scene1");
    }
  public void quitgame()
  {
        Debug.Log("Quit game");
        Application.Quit();
        
  }
  public PlayerDataObject playerDataObject1;
  public bool NewGameReset = false;
  public LevelEntry CurrentLevelEntry2 { get; private set; }
  public bool IsRepeatedLevel1 { get; private set; }
  public bool resetgame=false;
  public void NewGame(){
    
    var playerData1 = Entry.Instance.playerDataObject.Data;
    playerData1.lastLoadedlevel=0;
    resetgame=true;
    playerData1.SetCurrentLevel(0);
    int lastLevel=0;
    int _level = PlayerPrefs.GetInt("playerData.Level");
    PlayerPrefs.SetInt("playerData.Level", 0);
    PlayerPrefs.Save();
    
    
    SceneManager.LoadScene("Main");
    
    
    
  }
  static void DeleteAllPlayPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
  public void DeletePlayerData(){
    PlayerPrefs.DeleteAll();
    Debug.Log("Deleted Data");
  }
}