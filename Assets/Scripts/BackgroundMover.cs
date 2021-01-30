using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public MeshRenderer rend;
    public Vector2 moveSpeed;

    private void Update()
    {
        rend.material.SetTextureOffset("_MainTex", moveSpeed * Time.time);
    }
}
