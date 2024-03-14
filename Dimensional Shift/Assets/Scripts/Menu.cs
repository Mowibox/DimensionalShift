using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public string nomdeScene;

    public void ChangeScene(string _nomlevel)
    {
        SceneManager.LoadScene(_nomlevel);

    }
    }

