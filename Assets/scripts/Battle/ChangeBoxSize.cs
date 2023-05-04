using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBoxSize : MonoBehaviour
{
    public Image heartBorder;
    public BoxCollider2D[] borders;
    public heartControl heart;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeSize(Vector2 newSize)
    {
        StartCoroutine(Change(newSize));
    }

    public IEnumerator Change(Vector2 newSize)
    {
        heartBorder.rectTransform.sizeDelta = newSize;

        borders[1].transform.position = new Vector3(-9.664f, -1.349657f, 0.1559448f);
        borders[0].transform.position = new Vector3(6.253f, -1.349657f, 0.1559448f);
        borders[2].size = new Vector2(16f, 0.3474879f);
        borders[3].size = new Vector2(16f, 0.3474879f);

        heart.SetHeart();
        yield return null;
    }

    public void RestoreSize()
    {
        heartBorder.rectTransform.sizeDelta = new Vector2(355, 355);

        borders[1].transform.position = new Vector3(-3.305556f, -1.349657f, 0.1559448f);
        borders[0].transform.position = new Vector3(-0.1079386f, -1.349657f, 0.1559448f);
        borders[2].size = new Vector2(3.321233f, 0.3474879f);
        borders[3].size = new Vector2(3.321233f, 0.2214708f);

        heart.SetHeart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
