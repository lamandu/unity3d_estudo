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

    private  Animator animator;

    private Rigidbody rigidBody;

    public int Vida = 100;

    public ControlaInterface controlaInterface;

   // Start is called before the first frame update
   void Start()
    {
        Time.timeScale = 1;
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
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
            animator.SetBool("movendo", true);
        }
        else
        {
            animator.SetBool("movendo", false);
        }

        if (Vida <=0 )
        {
            if(Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("game");    
            }
        }
    }

    void FixedUpdate()
    {
         rigidBody.MovePosition
         (rigidBody.position + 
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
            rigidBody.MoveRotation(novaRotacao);
        }
    }

    public void TomaDano(int dano) 
    {
        Vida-=dano;
        controlaInterface.AtualizarSliderVidaJogador();
        if (Vida <=0 )
        {
            Time.timeScale = 0;
            TextoGameOver.SetActive(true);
        }
    }
}
