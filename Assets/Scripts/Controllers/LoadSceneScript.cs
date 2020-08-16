using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneScript : MonoBehaviour
{
    public void LoadSceneStory()
    {
        SceneManager.LoadScene("Story");
    }

    public void LoadSceneText()
    {
        SceneManager.LoadScene("TextIO");
    }
}
