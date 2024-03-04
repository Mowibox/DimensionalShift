using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundTransform : MonoBehaviour
{
    private Rigidbody rigidbodyComponentPlatform;
    private Vector3 platformPos3D, platformPos2D;
    public static float originPos2Dto3D;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponentPlatform = GetComponent<Rigidbody>();
        platformPos3D = rigidbodyComponentPlatform.position;
    
    }

    // Update is called once per frame
    void Update()
    {

  
        if (Cubix.mode == Cubix.dim2)
        {
            if (Cubix.rotation == 0)
            {
                platformPos2D.x = platformPos3D.x;
                platformPos2D.y = platformPos3D.y;
                platformPos2D.z = Mathf.Lerp(platformPos3D.z, 0f, Cubix.cameraAnimation);
                if (rigidbodyComponentPlatform.tag == "UnderCubix")
                {
                    originPos2Dto3D = platformPos3D.z;
                }

            }

            else if (Cubix.rotation == 1)
            {
                platformPos2D.x = Mathf.Lerp(platformPos3D.x, 0f, Cubix.cameraAnimation);
                platformPos2D.y = platformPos3D.y;
                platformPos2D.z = platformPos3D.z;
                if (rigidbodyComponentPlatform.tag == "UnderCubix")
                {
                    originPos2Dto3D = platformPos3D.x;
                }

            }

            else if (Cubix.rotation == 2)
            {
                platformPos2D.x = platformPos3D.x;
                platformPos2D.y = platformPos3D.y;
                platformPos2D.z = Mathf.Lerp(platformPos3D.z, 8f, Cubix.cameraAnimation);
                if (rigidbodyComponentPlatform.tag == "UnderCubix")
                {
                    originPos2Dto3D = platformPos3D.z;
                }

            }

            else if (Cubix.rotation == 3)
            {
                platformPos2D.x = Mathf.Lerp(platformPos3D.x, 17f, Cubix.cameraAnimation);
                platformPos2D.y = platformPos3D.y;
                platformPos2D.z = platformPos3D.z;
                if (rigidbodyComponentPlatform.tag == "UnderCubix")
                {
                    originPos2Dto3D = platformPos3D.x;
                }

            }


            //Debug.Log(zPos2Dto3D);

            rigidbodyComponentPlatform.position = platformPos2D;
        }
        else if (Cubix.mode == Cubix.dim3)
        {
            if (Cubix.rotation == 0)
            {
                platformPos2D.z = Mathf.Lerp(0f, platformPos3D.z, Cubix.cameraAnimation);
                rigidbodyComponentPlatform.position = platformPos2D;
            }

            else if (Cubix.rotation == 1)
            {
                platformPos2D.x = Mathf.Lerp(0f, platformPos3D.x, Cubix.cameraAnimation);
                rigidbodyComponentPlatform.position = platformPos2D;
            }

            else if (Cubix.rotation == 2)
            {
                platformPos2D.z = Mathf.Lerp(8f, platformPos3D.z, Cubix.cameraAnimation);
                rigidbodyComponentPlatform.position = platformPos2D;
            }

            else if (Cubix.rotation == 3)
            {
                platformPos2D.x = Mathf.Lerp(17f, platformPos3D.x, Cubix.cameraAnimation);
                rigidbodyComponentPlatform.position = platformPos2D;
            }


        }


    }

    private void FixedUpdate()
    {
        
    }
}
