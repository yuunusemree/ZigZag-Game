using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineTutorial : MonoBehaviour
{
    public Text messageText;
    string message = "hi from unity, this is me";
    string[] words;
    public Transform target;
    public Transform[] waypoints;
    Vector3[] waypointsPos;


    // Start is called before the first frame update
    void Start()
    {
        waypointsPos = new Vector3[waypoints.Length];

        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointsPos[i] = waypoints[i].position;
        }
        
        words = message.Split(' ');
        
        StartCoroutine(Move(waypointsPos));
        
    }

    IEnumerator current;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        if (current != null)
    //        {
    //            StopCoroutine(current);
    //        }
    //        current = MoveToTarget(Random.insideUnitCircle * 5, 2);
    //        StartCoroutine(current);           
    //    }
    //}

    IEnumerator PrintDialog()
    {
        foreach (var word in words)
        {
            //Debug.Log(word);
            messageText.text += word + " ";
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator DoSomething()
    {
        while (true)
        {
            Debug.Log("Hi");
            yield return new WaitForSeconds(0.5f);
        }
    }


    IEnumerator MoveToTarget(Vector3 target, float moveSpeed)
    {
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
            yield return null;
        }
        Debug.Log("Done!");
    }
   
    IEnumerator Move(Vector3[] waypoints)
    {
        foreach (var waypoint in waypoints)
        {
            yield return MoveToTarget(waypoint, 2);
        }
    }

}
