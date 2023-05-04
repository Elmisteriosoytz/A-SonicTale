using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChangeCage : MonoBehaviour
{
    ChangeBoxSize changeSize;


    public void changeBox()
    {
        changeSize.ChangeSize(new Vector2(1724f, 355));
    }

    public void RestoreBox()
    {
        changeSize.RestoreSize();
    }

    // Start is called before the first frame update
    void Start()
    {
        changeSize = FindObjectOfType<ChangeBoxSize>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
