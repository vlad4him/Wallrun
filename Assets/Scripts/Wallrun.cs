using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Wallrun : MonoBehaviour
{
    private bool isWallR = false;
    private bool isWallL = false;
    private RaycastHit hitR;
    private RaycastHit hitL;
    private int jumpCount = 0;
    private RigidbodyFirstPersonController cc;
    private Rigidbody rb;
    public float runTime = 0.75f;
    Vector3 velocity;
    public float jumpHeight = 3f;
    


    void Start()
    {
        cc = GetComponent<RigidbodyFirstPersonController> ();
        rb = GetComponent<Rigidbody> ();
        cc.advancedSettings.airControl = false;
    }

    void Update()
    {
        //wallrun
        if (cc.Grounded)
        {
            jumpCount = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !cc.Grounded && jumpCount <= 1)
        {
            if (Physics.Raycast(transform.position, transform.right, out hitR, 1))
            {
                if (hitR.transform.tag == "Wall")
                {
                    isWallR = true;
                    isWallL = false;
                    jumpCount += 1;
                    rb.useGravity = false;
                    if (Input.GetKeyDown(KeyCode.Space) && cc.Grounded)
                    {
                        velocity.y = Mathf.Sqrt(jumpCount * -2f);
                    }
                    StartCoroutine(afterRun());
                    
                }
            }

            if (Physics.Raycast(transform.position, -transform.right, out hitL, 1))
            {
                if (hitL.transform.tag == "Wall")
                {
                    
                    isWallL = true;
                    isWallR = false;
                    jumpCount += 1;
                    rb.useGravity = false;
                    if (Input.GetKeyDown(KeyCode.Space) && cc.Grounded)
                    {
                        velocity.y = Mathf.Sqrt(jumpCount * -2f);
                    }
                    StartCoroutine(afterRun());

                }
            }
        }
    }

    IEnumerator afterRun(){
        yield return new WaitForSeconds(runTime);
        isWallL = false;
        isWallR = false;
        rb.useGravity = true;
    }
}