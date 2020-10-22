using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameModel.UpdateDisplay();
        GameModel.menuController.exitText();
    }

}
