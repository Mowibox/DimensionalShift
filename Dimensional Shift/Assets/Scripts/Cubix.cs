using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

//Classe du personnage : Cubix
public class Cubix : MonoBehaviour
{
    //Objet et position de Cubix
    private Rigidbody rigidbodyComponentCubix;
    private Vector3 positionCubix;
    public static RaycastHit raycastCubix;

    //Gestion des collisions
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask cubixMask;

    //Sauts et deplacements
    private float horizontalInput, verticalInput;
    private float movingSpeed = 2.5f;
    private bool jumpKeyWasPressed;
    private float jumpPower = 4.7f;

    //Gestion des dimensions
    public static int dim2 = 0;
    public static int dim3 = 1;

    public static int mode = dim2;

    //Gestion de la camera
    public static float cameraAnimation = 1f;
    private Vector3 cameraGoal2D = new Vector3(8.6f, 3.7f, -5.84f);
    private Vector3 cameraRotationGoal2D = new Vector3(0f, 0f, 0f);
    private Vector3 cameraGoal3D = new Vector3(0f, 9.6f, -3f);
    private Vector3 cameraRotationGoal3D = new Vector3(51f, 50f, 11f);

    //Gestion de la rotation
    public static float rotationAnimation = 1f;
    public static int rotation = 0;
    private static float cubixRotation = 0f;
    private static float cubixRotationGoal;
    private Vector3[] cameraRotationPosition = new Vector3[]
    {
        new Vector3(0f, 9.6f, -3f),
        new Vector3(18f, 11f, -3f),
        new Vector3(18f, 11f, 10f),
        new Vector3(-2.5f, 11f, 8f)
    };

    private Vector3[] cameraRotationAngle = new Vector3[]
    {
        new Vector3(51f, 50f, 11f),
        new Vector3(52f, 324f, 12f),
        new Vector3(52f, 228f, 12f),
        new Vector3(52f, 121f, 12f)
    };



    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponentCubix = GetComponent<Rigidbody>();
        positionCubix = rigidbodyComponentCubix.position;

    }

    // Update is called once per frame
    void Update()

    {

        LevelReset();
        CameraMode(mode);
        CubixAnimation();

        horizontalInput = Input.GetAxis("Horizontal");

        //Le deplacement en profondeur est uniquement actif en 3D
        if (mode == dim3)
        {
            verticalInput = Input.GetAxis("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        //Identification de la plateforme sous Cubix
        if (Physics.Raycast(transform.position, Vector3.down, out raycastCubix, Mathf.Infinity))
        {
            if (raycastCubix.collider.CompareTag("Platform"))
            {
                raycastCubix.collider.tag = "UnderCubix";
            }

            if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, cubixMask).Length == 0)
            {
                raycastCubix.collider.tag = "Platform";
            }

        }

       
        if (Input.GetKeyDown(KeyCode.X))
        {
            DimensionSwap();

        }

        if (Input.GetKeyDown(KeyCode.P) && mode == dim3)
        {
            cubixRotation = cubixRotationGoal;
            cubixRotationGoal += 90f;
            SpaceRotation();

        }

        Debug.Log(rotation);

    }


    //FixedUpdate is called once every physic update
    private void FixedUpdate()
    {

        if (mode == dim2)
        {
            if (rotation == 0)
            {
                rigidbodyComponentCubix.velocity = new Vector3(movingSpeed * horizontalInput, rigidbodyComponentCubix.velocity.y, 0);
            }

            else if (rotation == 1)
            {
                rigidbodyComponentCubix.velocity = new Vector3(0, rigidbodyComponentCubix.velocity.y, -movingSpeed * horizontalInput);
            }

            else if (rotation == 2)
            {
                rigidbodyComponentCubix.velocity = new Vector3(-movingSpeed * horizontalInput, rigidbodyComponentCubix.velocity.y, 0);
            }

            else if (rotation == 3)
            {
                rigidbodyComponentCubix.velocity = new Vector3(0, rigidbodyComponentCubix.velocity.y, movingSpeed * horizontalInput);
            }

        }
        else if (mode == dim3)
        {
            if (rotation == 0)
            {
                rigidbodyComponentCubix.velocity = new Vector3(movingSpeed * horizontalInput, rigidbodyComponentCubix.velocity.y, movingSpeed * verticalInput);
            }

            else if (rotation == 1)
            {
                rigidbodyComponentCubix.velocity = new Vector3(movingSpeed * verticalInput, rigidbodyComponentCubix.velocity.y,- movingSpeed * horizontalInput);
            }

            else if (rotation == 2)
            {
                rigidbodyComponentCubix.velocity = new Vector3(-movingSpeed * horizontalInput, rigidbodyComponentCubix.velocity.y, -movingSpeed * verticalInput);

            }

            else if (rotation == 3)
            {
                rigidbodyComponentCubix.velocity = new Vector3(-movingSpeed * verticalInput, rigidbodyComponentCubix.velocity.y, movingSpeed * horizontalInput);
            }


        }

        if (jumpKeyWasPressed)
        {
            Jump();
        }
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        //Contact avec une boite
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);

        }

        //Contact avec une zone de fin de jeu
        if (other.gameObject.layer == 8)
        {   
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            renderer.material.color = Color.green;
        }


    }

    private void OnTriggerExit(Collider other)
    {
        //Sortie de contact
        if (other.gameObject.layer == 8)
        {
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            renderer.material.color = Color.red;
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.tag = "Platform";
        
    }


    //Changement de dimension lorsque X est presse
    private void DimensionSwap()
    {
        cameraAnimation = 0f;
        mode = (mode + 1) % 2;
    }

    private void SpaceRotation()
    {
        rotationAnimation = 0f;
        rotation = (rotation + 1) % 4;
    }

    //Conditions de reinitialisation du niveau
    private void LevelReset()
    {
        positionCubix.y = rigidbodyComponentCubix.position.y;
        if (positionCubix.y <= -10 || Input.GetKeyDown(KeyCode.R))
        {
            positionCubix.x = 0f;
            positionCubix.y = 5f;
            positionCubix.z = 0f;
            rigidbodyComponentCubix.position = positionCubix;
        }
    }

    //Deformation du cube personnage pour animer ses mouvements
    private void CubixAnimation()
    {
        //Animation de saut
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, cubixMask).Length == 0)
        {
            transform.localScale = new Vector3(
            Mathf.Lerp(0.5f, 0.55f, Mathf.Abs(rigidbodyComponentCubix.velocity.y) / 5f),
            Mathf.Lerp(0.5f, 0.8f, Mathf.Abs(rigidbodyComponentCubix.velocity.y) / 5f),
            transform.localScale.z);
        }

        //Animation de deplacement
        else
        {
            transform.localScale = new Vector3(
            Mathf.Lerp(0.5f, 0.55f, Mathf.Abs(rigidbodyComponentCubix.velocity.x) * Mathf.Sin(Time.time * 10f)),
            Mathf.Lerp(0.5f, 0.55f, Mathf.Abs(rigidbodyComponentCubix.velocity.x) * Mathf.Sin(Time.time * 10f)),
            transform.localScale.z);
        }

        //Rotation et translation dimensionnelle
        if (cameraAnimation <= 1f)
        {
            if (mode == dim2)
            {
                positionCubix.x = rigidbodyComponentCubix.position.x;
                positionCubix.y = rigidbodyComponentCubix.position.y;
                positionCubix.z = Mathf.Lerp(GroundTransform.zPos2Dto3D, 0f, cameraAnimation);
                rigidbodyComponentCubix.position = positionCubix;
                rigidbodyComponentCubix.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(360f, 0f, cameraAnimation), 0);
            }

            else if (mode == dim3)
            {
                positionCubix.x = rigidbodyComponentCubix.position.x;
                positionCubix.y = rigidbodyComponentCubix.position.y;
                positionCubix.z = Mathf.Lerp(0f, GroundTransform.zPos2Dto3D, cameraAnimation);
                rigidbodyComponentCubix.position = positionCubix;
                rigidbodyComponentCubix.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(0f, 360f, cameraAnimation), 0);
            }
        }

        if (rotationAnimation <= 1f)
        {
            rigidbodyComponentCubix.transform.rotation = Quaternion.Euler(0, Mathf.Lerp(cubixRotation, cubixRotationGoal, rotationAnimation), 0);

        }

        rotationAnimation += 0.015f;
    }

    private void Jump()
    {
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, cubixMask).Length != 0)
        {
            rigidbodyComponentCubix.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }
           
    }

    //Gestion de la camera
    private void CameraMode(int mode)
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        if (cameraAnimation <= 1f)
        {
            if (mode == dim2)
            {
                cameraPosition.x = Mathf.Lerp(cameraGoal3D.x, cameraGoal2D.x, cameraAnimation);
                cameraPosition.y = Mathf.Lerp(cameraGoal3D.y, cameraGoal2D.y, cameraAnimation);
                cameraPosition.z = Mathf.Lerp(cameraGoal3D.z, cameraGoal2D.z, cameraAnimation);
                Camera.main.transform.position = cameraPosition;

                Camera.main.transform.rotation = Quaternion.Euler(
                    Mathf.Lerp(cameraRotationGoal3D.x, cameraRotationGoal2D.x, cameraAnimation),
                    Mathf.Lerp(cameraRotationGoal3D.y, cameraRotationGoal2D.y, cameraAnimation),
                    Mathf.Lerp(cameraRotationGoal3D.z, cameraRotationGoal2D.z, cameraAnimation));

                Camera.main.orthographic = true;
                Camera.main.orthographicSize = 3.9f;

            }

            else if (mode == dim3)
            {

                cameraPosition.x = Mathf.Lerp(cameraGoal2D.x, cameraGoal3D.x, cameraAnimation);
                cameraPosition.y = Mathf.Lerp(cameraGoal2D.y, cameraGoal3D.y, cameraAnimation);
                cameraPosition.z = Mathf.Lerp(cameraGoal2D.z, cameraGoal3D.z, cameraAnimation);
                Camera.main.transform.position = cameraPosition;

                Camera.main.transform.rotation = Quaternion.Euler(
                    Mathf.Lerp(cameraRotationGoal2D.x, cameraRotationGoal3D.x, cameraAnimation),
                    Mathf.Lerp(cameraRotationGoal2D.y, cameraRotationGoal3D.y, cameraAnimation),
                    Mathf.Lerp(cameraRotationGoal2D.z, cameraRotationGoal3D.z, cameraAnimation));
                
                Camera.main.orthographic = false;

                
            }

            cameraAnimation += 0.015f;
        }

        if (mode == dim3 && rotationAnimation <= 1f)
        {
            cameraPosition.x = Mathf.Lerp(cameraRotationPosition[rotation].x, cameraRotationPosition[(rotation + 1) % 4].x, rotationAnimation);
            cameraPosition.y = Mathf.Lerp(cameraRotationPosition[rotation].y, cameraRotationPosition[(rotation + 1) % 4].y, rotationAnimation);
            cameraPosition.z = Mathf.Lerp(cameraRotationPosition[rotation].z, cameraRotationPosition[(rotation + 1) % 4].z, rotationAnimation);
            Camera.main.transform.position = cameraPosition;

            Camera.main.transform.rotation = Quaternion.Euler(
                Mathf.Lerp(cameraRotationAngle[rotation].x, cameraRotationAngle[(rotation + 1) % 4].x, rotationAnimation),
                Mathf.Lerp(cameraRotationAngle[rotation].y, cameraRotationAngle[(rotation + 1) % 4].y, rotationAnimation),
                Mathf.Lerp(cameraRotationAngle[rotation].z, cameraRotationAngle[(rotation + 1) % 4].z, rotationAnimation));

        }
    }

    


}
