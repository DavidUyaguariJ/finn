using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public int vida = 3;
    public float timeByStep = 0.25f;
    float cont = 0f;
    public bool stepOne = false;
    public bool fall = false;

    public float fuerzaSalto = 10f; 
    public float fuerzaRebote = 6f; 
    public float longitudRaycast = 0.1f; 
    public LayerMask capaSuelo; 

    private bool enSuelo; 
    private bool recibiendoDanio;
    private bool atacando;
    public bool muerto;

    public PlayerSoundController playerSoundController;

    private Rigidbody2D rb; 

    public Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!muerto)
        {
            if (!atacando)
            {
                Movimiento();
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
                enSuelo = hit.collider != null;

                if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDanio)
                {
                    fall = true;
                    playerSoundController.jumpPlayer();
                    rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
                }
                if (enSuelo) 
                {
                    fall = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.F) && !atacando && enSuelo)
            {
                Atacando();
            }
        }
        
        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDanio", recibiendoDanio);
        animator.SetBool("Atacando", atacando);
        animator.SetBool("muerto", muerto);
    }
       
    public void Movimiento()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        if (velocidadX !=0 && enSuelo && !recibiendoDanio && !atacando && !fall) 
        {
            cont  += Time.deltaTime;
            if (cont >= timeByStep)
            {
                cont = 0f;
                if (stepOne)
                {
                    playerSoundController.stepOnePlayer();
                }
                else
                {
                    playerSoundController.stepTwoPlayer();
                }
                stepOne = !stepOne;
            }   
        }
        animator.SetFloat("movement", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 posicion = transform.position;

        if (!recibiendoDanio)
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
    }
    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if(!recibiendoDanio)
        {
            playerSoundController.damagePlayer();
            recibiendoDanio = true;
            vida -= cantDanio;
            if (vida<=0)
            {
                playerSoundController.deadPlayer();
                muerto = true;
            }
            if (!muerto)
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    public void Atacando()
    {
        playerSoundController.attackPlayer();
        atacando = true;
    }

    public void DesactivaAtaque()
    {
        atacando = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
}
