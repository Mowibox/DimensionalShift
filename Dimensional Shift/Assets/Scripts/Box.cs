using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class Box : MonoBehaviour

{
    private Rigidbody rigidbodyComponentCube;
    private Vector3 positionCube;
    private Renderer rendererBox;
    public static bool cubeIsHolded = false;
    public static RaycastHit raycastCube;

    [SerializeField] private LayerMask cubeMask;
    [SerializeField] private Transform groundCheckTransformCube = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponentCube = GetComponent<Rigidbody>();
        rendererBox = GetComponent<Renderer>();
       

    }

    // Update is called once per frame
    void Update()
    {

        LevelReset();

        if (cubeIsHolded) 
        {
            rigidbodyComponentCube.transform.position = Cubix.rigidbodyComponentCubix.transform.position + new Vector3(0f, 0.45f, 0f);
            Color newColor = new Color(0.5f, 0.2f, 0.6f); 
            rendererBox.material.color = newColor;
        }

        else
        {
            CubeTransform();
        }


        if (Physics.Raycast(transform.position, Vector3.down, out raycastCube, Mathf.Infinity))
        {
            if (raycastCube.collider.CompareTag("Platform") && !(raycastCube.collider.gameObject.tag == "Player"))
            {
                raycastCube.collider.tag = "UnderCube";
            }

            if (Physics.OverlapSphere(groundCheckTransformCube.position, 0.1f, cubeMask).Length == 0 && !(raycastCube.collider.gameObject.tag == "Player"))
            {
                raycastCube.collider.tag = "Platform";
            }

        }



    }

    private void OnCollisionExit(Collision collision)
    {
        if (!(collision.gameObject.tag == "Player"))
        {
            collision.gameObject.tag = "Platform";
        }

    }

    private void OnCollisionStay(Collision collision)
    {   

        if (collision.gameObject.tag == "Player")
        {
            Color newColor = new Color(0.21f, 0.11f, 0.21f);
            rendererBox.material.color = newColor;

            if (Input.GetKeyDown(KeyCode.B))
            {
                Box.cubeIsHolded = true;
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                positionCube.x = rigidbodyComponentCube.position.x;
                positionCube.y = rigidbodyComponentCube.position.y;
                positionCube.z = rigidbodyComponentCube.position.z;
                rigidbodyComponentCube.position = positionCube;
                rigidbodyComponentCube.position = positionCube;
                Box.cubeIsHolded = false;

            }

        }

        else
        {
            Color newColor = new Color(0.11f, 0.11f, 0.11f);
            rendererBox.material.color = newColor;
        }

    }
    private void CubeTransform()
    {
        if (Cubix.mode == Cubix.dim2)
        {
            if (Cubix.cameraAnimation <= 1f)
            {

                if (Cubix.rotation == 0)
                {
                    positionCube.x = rigidbodyComponentCube.position.x;
                    positionCube.y = rigidbodyComponentCube.position.y;
                    positionCube.z = Mathf.Lerp(GroundTransform.originPos2Dto3DCube, 0f, Cubix.cameraAnimation);
                    rigidbodyComponentCube.position = positionCube;

                }
                else if (Cubix.rotation == 1)
                {
                    positionCube.x = Mathf.Lerp(GroundTransform.originPos2Dto3DCube, 0f, Cubix.cameraAnimation);
                    positionCube.y = rigidbodyComponentCube.position.y;
                    positionCube.z = rigidbodyComponentCube.position.z;
                    rigidbodyComponentCube.position = positionCube;

                }
                else if (Cubix.rotation == 2)
                {
                    positionCube.x = rigidbodyComponentCube.position.x;
                    positionCube.y = rigidbodyComponentCube.position.y;
                    positionCube.z = Mathf.Lerp(GroundTransform.originPos2Dto3DCube, 8f, Cubix.cameraAnimation);
                    rigidbodyComponentCube.position = positionCube;

                }
                else if (Cubix.rotation == 3)
                {
                    positionCube.x = Mathf.Lerp(GroundTransform.originPos2Dto3DCube, 17f, Cubix.cameraAnimation);
                    positionCube.y = rigidbodyComponentCube.position.y;
                    positionCube.z = rigidbodyComponentCube.position.z;
                    rigidbodyComponentCube.position = positionCube;

                }

            }

            else if (Cubix.mode == Cubix.dim3)
            {
                if (Cubix.rotation == 0)
                {
                    positionCube.x = rigidbodyComponentCube.position.x;
                    positionCube.y = rigidbodyComponentCube.position.y;
                    positionCube.z = Mathf.Lerp(0f, GroundTransform.originPos2Dto3DCube, Cubix.cameraAnimation);
                    rigidbodyComponentCube.position = positionCube;

                }
                else if (Cubix.rotation == 1)
                {
                    positionCube.x = Mathf.Lerp(0f, GroundTransform.originPos2Dto3DCube, Cubix.cameraAnimation);
                    positionCube.y = rigidbodyComponentCube.position.y;
                    positionCube.z = rigidbodyComponentCube.position.z;
                    rigidbodyComponentCube.position = positionCube;

                }
                else if (Cubix.rotation == 2)
                {
                    positionCube.x = rigidbodyComponentCube.position.x;
                    positionCube.y = rigidbodyComponentCube.position.y;
                    positionCube.z = Mathf.Lerp(8f, GroundTransform.originPos2Dto3DCube, Cubix.cameraAnimation);
                    rigidbodyComponentCube.position = positionCube;

                }
                else if (Cubix.rotation == 3)
                {
                    positionCube.x = Mathf.Lerp(17f, GroundTransform.originPos2Dto3DCube, Cubix.cameraAnimation);
                    positionCube.y = rigidbodyComponentCube.position.y;
                    positionCube.z = rigidbodyComponentCube.position.z;
                    rigidbodyComponentCube.position = positionCube;

                }
            }
        }
    }

    private void LevelReset()
    {
        positionCube.y = rigidbodyComponentCube.position.y;
        if (positionCube.y <= -10 || Input.GetKeyDown(KeyCode.R))
        {
            if (Cubix.mode == Cubix.dim2)
            {
                if (Cubix.rotation == 0)
                {
                    positionCube.x = 4f;
                    positionCube.y = 6f;
                    positionCube.z = 0f;

                }

                else if (Cubix.rotation == 1)
                {
                    positionCube.x = 0f;
                    positionCube.y = 6f;
                    positionCube.z = 0f;

                }

                else if (Cubix.rotation == 2)
                {
                    positionCube.x = 4f;
                    positionCube.y = 6f;
                    positionCube.z = 8f;

                }
                else if (Cubix.rotation == 3)
                {
                    positionCube.x = 17f;
                    positionCube.y = 6f;
                    positionCube.z = 0f;

                }

            }
            else if (Cubix.mode == Cubix.dim3)
            {
                positionCube.x = 4f;
                positionCube.y = 6f;
                positionCube.z = 0f;
            }

            rigidbodyComponentCube.position = positionCube;
        }
    }


       



}
