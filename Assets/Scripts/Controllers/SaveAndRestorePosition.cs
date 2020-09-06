using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveAndRestorePosition : MonoBehaviour
{
    void Start() // Check if we've saved a position for this scene; if so, go there.
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (GameModel.savedPositions.ContainsKey(sceneIndex))
        {
            transform.position = GameModel.savedPositions[sceneIndex];
        }
    }

    void OnDestroy() // Unloading scene, so save position.
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        GameModel.savedPositions[sceneIndex] = transform.position;
    }
}