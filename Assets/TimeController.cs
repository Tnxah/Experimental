using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    private float startRecordingtTime;
    public float startReversingtTime;

    public List<Vector3> recordedPosition; 
    public List<Quaternion> recordedRotation; 
    public List<float> recordedTime;

    public bool recording = false;
    public bool reversing = false;

    public bool test = false;

    private void Awake()
    {
        recordedTime = new List<float>();

        recordedPosition = new List<Vector3>();
        recordedRotation = new List<Quaternion>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Record();
    }

    // Update is called once per frame
    void Update()
    {
        if (recording)
        {
            var time = Time.time - startRecordingtTime;

            recordedTime.Add(time);
            recordedPosition.Add(transform.position);
            recordedRotation.Add(transform.rotation);
        }


        if (test)
        {
            StartCoroutine(Reverse());
            //Reverse();
            test = false;
        }
    }


    private void Record()
    {
        startRecordingtTime = Time.time;
        recording = true;
    }

    private IEnumerator Reverse()
    {
        GetComponent<Rigidbody>().useGravity = false;
        startReversingtTime = Time.time;
        reversing = true;


        for (int i = recordedTime.Count-1; i >= 0 ; i--)
        {
            yield return new WaitUntil(() => (Time.time - startReversingtTime >= recordedTime[recordedTime.Count-1] - recordedTime[i]));

            transform.position = recordedPosition[i];
            transform.rotation = recordedRotation[i];
            
        }

        reversing = false;


        GetComponent<Rigidbody>().velocity = Vector3.zero;
        yield return null;
    }
}
