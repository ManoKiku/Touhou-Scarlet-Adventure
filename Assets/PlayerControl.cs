using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigedbody2D rb;
    public int speed {get; private set;}

    private void Start() {
        rb = GetComponent<Rigedbody2D>();
    }

    void Update()
    {
    }


}
