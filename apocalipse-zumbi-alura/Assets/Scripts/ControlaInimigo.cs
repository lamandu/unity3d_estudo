using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{

    public GameObject Jogador;
    public float Velocidade = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() { 
         // calculo distancia entre o inimigo e o meu jogador
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);
        // distancia para perseguir o jogador
        if(distancia > 2.5) // considero o Radius dos colisores, 1 do inimigo e 1 do jogador
        {
            var direcao = Jogador.transform.position - transform.position;
                GetComponent<Rigidbody>().MovePosition
                (GetComponent<Rigidbody>().position + direcao.normalized * Velocidade * Time.deltaTime);
            // calculo rotacao baseada na direcao que o inimigo tem que ir.
            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }
    }
}