using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    private Renderer rendererGoal;


    // Start is called before the first frame update
    void Start()
    {
        rendererGoal = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {

        //Contact avec une zone de fin de jeu
        if (other.tag == "Player" || other.tag == "Box")
        {
            rendererGoal.material.color = Color.green;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        //Sortie de contact
        if (other.tag == "Player" || other.tag == "Box")
        {
            rendererGoal.material.color = Color.red;
        }

    }
}
