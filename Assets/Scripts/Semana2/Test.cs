using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    StreeringBehavior _StreeringBehavior;
    PathFollowing _PathFollowing;
    public TypePath TestcurrentPath;
    // Start is called before the first frame update
    void Start()
    {
        _StreeringBehavior = GetComponent<StreeringBehavior>();
        _PathFollowing = GetComponent<PathFollowing>();
        _PathFollowing.ChangePath(TestcurrentPath);
    }

    // Update is called once per frame
    void Update()
    {
        _StreeringBehavior.target = _PathFollowing.NextCurrentPointPath();

        _StreeringBehavior.Arrive();
    }
}
