using UnityEngine;
using System.Collections;

/// <summary>
/// Attach this to a camera to follow 2d subject
/// </summary>
public class FollowSubject : MonoBehaviour
{

    public GameObject subject;

    void Update()
    {
        Vector3 subjectPos = subject.transform.position;
        transform.position = new Vector3(subjectPos.x, subjectPos.y, -10);
    }
}
