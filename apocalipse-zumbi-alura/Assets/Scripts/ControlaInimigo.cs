using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour
{

    public GameObject Jogador;
    public float Velocidade = 5;

    private Rigidbody rigidBody;

    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        int geraTipoZumbi = Random.Range(1,28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);

        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() { 
         // calculo distancia entre o inimigo e o meu jogador
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        var direcao = Jogador.transform.position - transform.position;
        // calculo rotacao baseada na direcao que o inimigo tem que ir.
         Quaternion novaRotacao = Quaternion.LookRotation(direcao);
         rigidBody.MoveRotation(novaRotacao);
        // distancia para perseguir o jogador
        if(distancia > 2.5) // considero o Radius dos colisores, 1 do inimigo e 1 do jogador
        {
            // perseguindo
            rigidBody.MovePosition
                (rigidBody.position + direcao.normalized * Velocidade * Time.deltaTime);
            
           animator.SetBool("Atacando", false);
        }
        else 
        {
           animator.SetBool("Atacando", true);    
        }
    }

    void AtacaJogador()
    {
        Jogador.GetComponent<ControlaJogador>().TomaDano();
        // Time.timeScale = 0;
        // Jogador.GetComponent<ControlaJogador>().TextoGameOver.SetActive(true);
        // Jogador.GetComponent<ControlaJogador>().Vivo = false;
    }
}
