using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public void Restart()
    {  
        Application.LoadLevel("game");
    }

    public void Menu()
    {
        Application.LoadLevel("mainmenu");
    }
}
