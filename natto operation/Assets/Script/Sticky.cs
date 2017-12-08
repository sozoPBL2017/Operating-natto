using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * beta 0.0.0
 * wrirtten by yasu80
 * 20170626
 * 
 * not yet dilatant force added
 */

public class Sticky : MonoBehaviour {
    public GameObject me;
    public GameObject you;
    Vector3 stuckked_me = new Vector3(0, 0, 0);
    Vector3 stuckked_you = new Vector3(0, 0, 0);
    Vector3 base_collision = new Vector3(0, 0, 0);
    
    int k = 20;
    int i,sum;
    //GameObject[] tagobjs;

    public Rigidbody myRigidBody;
    public Rigidbody yourRigidBody;

    void OnCollisionEnter(Collision collision) {
            ContactPoint contact = collision.contacts[0];//Easy_Plus
            Quaternion rotate_me = Quaternion.FromToRotation(Vector3.up, contact.point);//Poop
            stuckked_you = contact.point;
            base_collision = stuckked_you;
            you = collision.gameObject;
            if (you.tag == "Untagged")
            {
                you.tag = "been";
            }
            
            yourRigidBody = you.GetComponent<Rigidbody>();
        
       

        //Get Collision Object
        //yourRigidBody = you.GetComponent<Rigidbody>();
        
        
        
    }

    // Use this for initialization
    void Start () {
         myRigidBody  = GetComponent<Rigidbody>();
    }
	
    void FixedUpdate(){

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis ("Vertical");

		myRigidBody.AddForce (x, 0, z);

        int i;
        GameObject[] tagobjs = GameObject.FindGameObjectsWithTag("been");
        /*
        var direction = tagobjs[0].transform.position - me.transform.position;
        var distance = direction.magnitude;
        var gravity = k / distance;
        */
        /*
        for (i = 0; tagobjs[i] != null; i++)
        {
            direction = tagobjs[i].transform.position - me.transform.position;
            myRigidBody.AddForce(gravity * direction.normalized, ForceMode.Force);
        } 
        */
        foreach  (GameObject tagobj in tagobjs)
        {
            var direction = tagobj.transform.position - me.transform.position;
            var distance = direction.magnitude;
            var gravity = k / distance;
            myRigidBody.AddForce(gravity * direction.normalized, ForceMode.Force);
        }
    }

    double fruidForce(double dist, double sigma, double velo_relative, double length_bond, double length_friction,
        double k_bond, double k_friction)
    {
        double force = 0;
        force = k_friction * (1 / Mathf.Sqrt(2 * Mathf.PI * Mathf.Pow((float)length_friction, 2.0f)) * Mathf.Exp(-(Mathf.Pow((float)dist, 2.0f)) / (2 * Mathf.Pow((float)length_friction, 2.0f))) * velo_relative
                + k_bond * (1 / Mathf.Sqrt(2 * Mathf.PI * Mathf.Pow((float)length_bond, 2.0f)) * Mathf.Exp(-(Mathf.Pow((float)dist, 2.0f)) / (2 * Mathf.Pow((float)length_bond, 2.0f))) * dist));
        Debug.Log("Force:");
        Debug.Log(force);
        return force;
    }

    bool IsBreak(float limit, float force)
    {
        if (limit > force) return true;
        else return false;
    }
}
