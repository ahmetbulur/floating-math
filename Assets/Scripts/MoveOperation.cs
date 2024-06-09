using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOperation : MonoBehaviour
{
    RectTransform myRectTransform;
    void Start()
    {
        myRectTransform = this.gameObject.GetComponent<RectTransform>();
    }
    void Update()
    {
        myRectTransform.anchoredPosition += Vector2.down * Time.deltaTime * GameControl.SharedInstance.GetSlidingSpeed();
    }
}
