using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    private float _startRecordingTime;
    private float _startReversingtTime;

    public List<Vector3> recordedPosition; 
    public List<Quaternion> recordedRotation; 
    public List<float> recordedTime;

    public bool recording = false;
    public bool test = false;

    public bool infiniteRecording;
    public float recordingSecondsLimit = 4f;

    private void Awake()
    {
        recordedTime = new List<float>();

        recordedPosition = new List<Vector3>();
        recordedRotation = new List<Quaternion>();
    }

    void Start()
    {
        Record();
    }

    void Update()
    {
        if (recording)
        {
            var time = Time.time - _startRecordingTime;

            recordedTime.Add(time);
            recordedPosition.Add(transform.position);
            recordedRotation.Add(transform.rotation);


            if (recordedTime[recordedTime.Count - 1] - recordedTime[0] > recordingSecondsLimit)
            {
                if (infiniteRecording)
                {
                    recordedTime.RemoveAt(0);
                    recordedPosition.RemoveAt(0);
                    recordedRotation.RemoveAt(0);
                }
                else
                {
                    recording = false;
                }
            }
        }


        if (test)
        {
            StartCoroutine(Reverse());
            test = false;
        }
    }


    private void Record()
    {
        _startRecordingTime = Time.time;
        recording = true;
    }

    private IEnumerator Reverse()
    {
        GetComponent<Rigidbody>().useGravity = false;
        _startReversingtTime = Time.time;


        for (int i = recordedTime.Count-1; i >= 0 ; i--)
        {
            yield return new WaitUntil(() => (Time.time - _startReversingtTime >= recordedTime[recordedTime.Count-1] - recordedTime[i])); //проблема в том что для бесконечности время реверсии неправильное

            transform.position = recordedPosition[i];
            transform.rotation = recordedRotation[i];
            
        }

        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        yield return null;
    }
}
