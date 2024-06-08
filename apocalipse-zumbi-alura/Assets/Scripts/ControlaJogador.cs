using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaJogador : MonoBehaviour
{
    public float Velocidade = 10;
    private Vector3 direcao;

    public LayerMask MascaraChao;

    public GameObject TextoGameOver;

    public bool Vivo = true;


   // Start is called before the first frame update
   void Start()
    {
        Time.timeScale = 1;
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

        if(!Vivo)
        {
            if(Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("game");    
            }
        }
    }

    void FixedUpdate()
    {
         GetComponent<Rigidbody>().MovePosition
         (GetComponent<Rigidbody>().position + 
         (direcao * Velocidade * Time.deltaTime));
    
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
