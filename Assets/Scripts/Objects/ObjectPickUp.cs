using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ThatAnnoyyingColleagueInput.Manager;

public class ObjectPickUp : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    public float throwForce = 1f;
    public float pickUpRange = 5f;
    //object which we pick up
    private GameObject heldObj;
    //rigidbody of object we pick up
    private Rigidbody heldObjRb; 
    private bool canDrop = true;
    private int LayerNumber;
    private bool eButtonPressed = false;
    private bool PumpForceButtonPressed = false;
    private InputManager inputManager;
    public UIManager uiManager;


    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");

        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        if (inputManager.PickUp == 0f) eButtonPressed = false;

        if (inputManager.PumpPower == 0f) PumpForceButtonPressed = false;

        // PickUp Button is E-Keyboard or X-XboxController set in InputManager
        if (inputManager.PickUp > 0.1f && eButtonPressed == false) 
        {

            //Debug.Log("E pressed");
            eButtonPressed = true;

            //if no Object holding
            if (heldObj == null) 
            {
                //raycast check if object within pickuprange and looked at
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    //check pickup tag
                    if (hit.transform.gameObject.tag == ObjectManager.Instance.throwableObjectTag)
                    {
                        //pass object hit to PickUpObject function
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    //stop object clipping through walls
                    StopClipping();
                    DropObject();
                }
            }
        }

        //if player is holding object
        if (heldObj != null) 
        {
            //hold object position at holdPos
            MoveObject(); 
            if (inputManager.PumpPower > 0.1f && PumpForceButtonPressed == false) pumpPower();

            //ThrowObject
            if (inputManager.ThrowObject && canDrop == true) 
            {
                Debug.Log("ObjectThrow Pressed");
                StopClipping();
                ThrowObject();
            }
        }
    }

    private void pumpPower()
    {
        if (throwForce <= 1000f) throwForce += 100f;
        PumpForceButtonPressed = true;
        float energyBarValue = throwForce / 10;
        setEnergyBar(energyBarValue);
    }

    private void setEnergyBar(float energyValue)
    {
        uiManager.PlayerUI.energyBar.SetEnergy(energyValue);
    }

    void PickUpObject(GameObject pickUpObj)
    {
        //check object has a RigidBody
        if (pickUpObj.GetComponent<Rigidbody>()) 
        {
            //set heldObj to the object that was hit by the raycast
            heldObj = pickUpObj;
            //set Rigidbody
            heldObjRb = pickUpObj.GetComponent<Rigidbody>(); 
            heldObjRb.isKinematic = true;
            //object parent to holdposition
            heldObjRb.transform.parent = holdPos.transform;
            //set object layer to holdLayer
            heldObj.layer = LayerNumber; 
            //check object collision with player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }

    void DropObject()
    {
        //reenable collision with player
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        //object assign to default layer
        heldObj.layer = 0; 
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;

        throwForce = 1f;
        setEnergyBar(throwForce);
    }

    void MoveObject()
    {
        //set object position same as the hold position
        heldObj.transform.position = holdPos.transform.position;
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
        throwForce = 1f;
        setEnergyBar(throwForce);
    }

    //only call if dropping or throwing
    void StopClipping() 
    {
        //distance from holdPos to the camera
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        //RaycastAll -> blocks raycast because object in center screen
        //RaycastAll -> array of all colliders hit in the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if > 1 -> hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //set object position to camera position 
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}