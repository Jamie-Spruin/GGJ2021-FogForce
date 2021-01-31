using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutoutMover : MonoBehaviour
{
    public RawImage renderImage;
    public void Move(Vector2 target)
    {
        transform.localPosition = target;
        // move opposite to make it look like it's staying in place
        renderImage.transform.localPosition = -target;
    }
}
