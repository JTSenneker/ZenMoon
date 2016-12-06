using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveControl : MonoBehaviour
{
    public void Save1()
    {
        SaveLoadController.save1 = true;
        SceneManager.LoadScene(2);
    }

    public void Save2()
    {
        SaveLoadController.save2 = true;
        SceneManager.LoadScene(2);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SaveLoadController.contin = true;
        SceneManager.LoadScene(1);
    }
}
