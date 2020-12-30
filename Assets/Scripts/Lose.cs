using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
