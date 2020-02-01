using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform playerObject;

    // Update is called once per frame
    void Update()
    {
        Vector3 lookOnObject = playerObject.position - transform.position;
        lookOnObject = playerObject.position - transform.position;
        transform.forward = lookOnObject.normalized;
    }
}
