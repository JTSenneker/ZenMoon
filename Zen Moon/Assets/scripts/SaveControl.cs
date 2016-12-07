using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Controls the buttons that speak to the saveLoadController
/// </summary>
public class SaveControl : MonoBehaviour
{
    /// <summary>
    /// Selects the first save file and loads the game
    /// </summary>
    public void Save1()
    {
        SaveLoadController.save1 = true;
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Selects the second save file and loads the game
    /// </summary>
    public void Save2()
    {
        SaveLoadController.save2 = true;
        SceneManager.LoadScene(2);
    }

    /// <summary>
    /// Selects a new game
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Selects a loaded game 
    /// </summary>
    public void LoadGame()
    {
        SaveLoadController.contin = true;
        SceneManager.LoadScene(1);
    }
}
