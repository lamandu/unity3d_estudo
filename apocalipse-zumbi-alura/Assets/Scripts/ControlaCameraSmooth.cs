using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCameraSmooth : MonoBehaviour
{
    public GameObject Jogador;
    public Vector3 Offset;
    private float smoothSpeed = 0.125f;

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position - Jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = Jogador.transform.position + Offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}