using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class ControlaJogador : MonoBehaviour
{
    public float velocidade = 10;
    Vector3 direcao;

    public LayerMask MascaraChao;

   // Start is called before the first frame update
   void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float eixoX = Input.GetAxisRaw("Horizontal");
        float eixoZ = Input.GetAxisRaw("Vertical");

        direcao = new Vector3(eixoX, 0, eixoZ);

        // Verifica se a magnitude da dire��o � maior que o limiar de movimento
        if (direcao != Vector3.zero)
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
         GetComponent<Rigidbody>().MovePosition
         (GetComponent<Rigidbody>().position + 
         (direcao * velocidade * Time.deltaTime));
    
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);
        
        RaycastHit impacto;
        if(Physics.Raycast(raio,out impacto, 100, MascaraChao)) 
        {
            // ponto de impacto baseado na posicao do meu jogador (impacto.point - transform.position;)
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            // igualando altura com o jogador.
            posicaoMiraJogador.y = transform.position.y; 
            // calculo da rotacao
            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);
            // adiciona rotacao ao personagem.
            GetComponent<Rigidbody>().MoveRotation(novaRotacao);
        }


    }
}
