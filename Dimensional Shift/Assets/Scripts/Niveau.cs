using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Niveau : MonoBehaviour
{
    public string nomdeScene;

    public void AllerAuNiveau()
    {
        SceneManager.LoadScene(nomdeScene);

    }

    public void OnTriggerEnter(Collider other)
    {
        AllerAuNiveau();
    }

}
