using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{
    public float velocidade = 10;
    private float limiarMovimento = 0.1f; // Limiar para considerar como movimento
    Vector3 direcao;

   // Start is called before the first frame update
   void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoZ = Input.GetAxis("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        // Verifica se a magnitude da direção é maior que o limiar de movimento
        if (direcao.magnitude > limiarMovimento)
        {
           // transform.Translate(direcao * velocidade * Time.deltaTime);
            GetComponent<Animator>().SetBool("movendo", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("movendo", false);
        }
    }

    void FixedUpdate()
    {
        if (direcao.magnitude > limiarMovimento)
        {
            GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + (direcao * velocidade * Time.deltaTime));
        }
    }
}
