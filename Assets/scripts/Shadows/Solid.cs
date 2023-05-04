using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solid : MonoBehaviour
{
    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;

            }
            else
            {
                //Generate a ghost
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 0.6f);
            }
        }
    }
}
