using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Rigidbody rb;


    //float G = 6.67f  * Mathf.Pow(10,-11); //this is the real gravitational constant
    //const float G = 0.0000000000674f; //same number w/ zeros
    const float G = 667.4f; //

    //list of Attractors for optimization in order to prevent excess searching of objects
    public static List<Orbit> Attractors;

    void FixedUpdate()
    {
        //runs the script for every object with the orbit script
        Orbit[] attractors = FindObjectsOfType<Orbit>();
        foreach (Orbit attractor in attractors)
        {
            if (attractor != this)
                ApplyForce(attractor);
        }
    }
    void OnEnable()
    {
        //initializes the array of attractors
        if (Attractors != null)
            Attractors = new List<Orbit>();

        Attractors.Add(this);
        
    }
    void OnDisable()
    {
        //removes object form list on disable
        Attractors.Remove(this); 
    }
    void ApplyForce(Orbit targetObject)
    {
        //the main math to calculate the force of an object 
        
        Rigidbody rbTarget = targetObject.rb;

        //subtracts the positions to get the distance
        Vector3 direction = rb.position - rbTarget.position;

        //defines distance ^ 2
        float distance = direction.sqrMagnitude;

        //checks if two objects have the same cords prevent a crash (thanks google)
        if (distance == 0f)
            return;

        //force = G (m1 * m2) / distance ^ 2
        float forceMagnitude = G * (rb.mass * rbTarget.mass) / distance;

        //sets a new Vector3 equal to the force found above
        Vector3 force = direction.normalized * forceMagnitude;
        print(force);
        rbTarget.AddForce(force);
    }
}
