using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachTest : MonoBehaviour
{
    Joint2D joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && joint != null)
        {
            Destroy(joint);
        }
    }
}
