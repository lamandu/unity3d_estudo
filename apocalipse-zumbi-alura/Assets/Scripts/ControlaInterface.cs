using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador controlaJogador;
    public Slider SliderVidaJogador;
    
    // Start is called before the first frame update
    void Start()
    {
        controlaJogador=GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue=controlaJogador.Vida;   
        AtualizarSliderVidaJogador() ;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AtualizarSliderVidaJogador() 
    { 
        SliderVidaJogador.value=controlaJogador.Vida;       
    }
}
