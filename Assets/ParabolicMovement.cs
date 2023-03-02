using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicMovement : MonoBehaviour
{
   
    
    public Transform startMarker;   // Starting position obj
    public Transform endMarker;     // Destination position obj
    public float duration = 2.0f;   // Time in seconds to reach the destination spear oject to cube object
    public float height = 5.0f;     // Height of the parabolic trajectory ...tragactory height 
    public GameObject endPoint;     // Destination Point .... Cube position taken purpose
    public GameObject initialPosition; // Initial position taken Spear object
    private float startTime;        // Time when the movement started...
    private float journeyLength;

    float maxRange = 10f;   // Random Range


    // Total distance between the start and end points

    void Start()
    {
        // Calculate the total distance between the start and end points
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

        // Record the start time of the movement
        startTime = Time.time;
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
           {

            transform.position = initialPosition.transform.position;

            Vector3 maximumBoundry = Camera.main.ScreenToViewportPoint(new Vector3(Screen.height, Screen.width));
            Vector3 minimumBoundry = Camera.main.ScreenToWorldPoint(Vector3.zero);

           
            Vector3 randomPosition = new Vector3(
                Random.Range(maxRange, -maxRange),
                Random.Range(maxRange, -maxRange),
                Random.Range(maxRange, -maxRange)
                );

            endPoint.transform.position = randomPosition;

            Debug.Log(randomPosition);

            // Calculate the total distance between the start (spear)_obj and end (cube)_obj points
            journeyLength = Vector3.Distance(startMarker.position, endMarker.position);

            // Recording the start time of the movement
            startTime = Time.time;


        }


         // Calculate the time elapsed since the movement started
        float elapsedTime = Time.time - startTime;

        // Calculate the percentage of the journey completed
        float journeyPercentage = Mathf.Clamp01(elapsedTime / duration);

        Debug.Log("journeyPercentage" +journeyPercentage);

        // Calculate the vertical offset of the parabolic trajectory
        //take the hight value and use the SIN theeta ot genrate the parabola with rispect to object origin starting time when clicking spacebar
        float verticalOffset = height * Mathf.Sin(journeyPercentage * Mathf.PI);

       // Debug.Log("verticalOffset" + verticalOffset);

        // Calculate the current position of the object along the parabolic trajectory
        Vector3 currentPos = Vector3.Lerp(startMarker.position, endMarker.position, journeyPercentage);
        currentPos.y += verticalOffset;


        //Debug.Log("currentPos" + (currentPos.y += verticalOffset));
        // Move the object towards the current position
        float step = journeyLength / duration * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentPos, step);
    }




}
