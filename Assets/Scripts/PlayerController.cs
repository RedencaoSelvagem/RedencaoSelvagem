using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;

    public float playerSpeed = 0.6f;
    public float currentSpeed;

    public Vector2 playerDirection;

    private bool isRunning;

    private Animator playerAnimator;

    // Player olhando para a direita
    private bool playerFacingRight = true;

    //Variuavel contadora 
    private int punchCount;

    //Tempo de ataque 
    private float timeCross = 1.3f;

    private bool comboControl;

    // Indicar se o Player esta morto
    private bool isDead;

    public int maxHealth = 10;
    public int currentHealt;
    public Sprite playerImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        // Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();

        currentSpeed = playerSpeed;

        currentHealt = maxHealth;
    }

    void Update()
    {
        PlayerMove();
        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.X))
        {


            //Iniciar o temporizador
            if (punchCount < 2)
            {

                PlayerJab();
                punchCount++;

                if (!comboControl)
                {
                    StartCoroutine(CrossController());
                }

            }

            else if (punchCount >= 2)
            {

                PlayerCross();
                punchCount = 0;
            }

            //Parando o temporizador 
            StopCoroutine(CrossController());
        }
    }

    private void FixedUpdate()
    {
        // Verificar se o Player está em movimento
        if (playerDirection.x != 0 || playerDirection.y != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        playerRigidBody.MovePosition(playerRigidBody.position + playerSpeed * Time.fixedDeltaTime * playerDirection);
    }

    void PlayerMove()
    {
        // Pega a entrada do jogador, e cria um Vector2 para usar no playerDirection
        playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Se o player vai para a ESQUERDA e está olhando para a DIREITA
        if (playerDirection.x < 0 && playerFacingRight)
        {
            Flip();
        }

        // Se o player vai para a DIREITA e está olhando para ESQUERDA
        else if (playerDirection.x > 0 && !playerFacingRight)
        {
            Flip();
        }
    }

    void UpdateAnimator()
    {
        // Definir o valor do parâmetro do animator, igual à propriedade isWalking
        playerAnimator.SetBool("isRunning", isRunning);
    }

    void Flip()
    {
        // Vai girar o sprite do player em 180º no eixo Y

        // Inverter o valor da variável playerfacingRight
        playerFacingRight = !playerFacingRight;

        // Girar o sprite do player em 180º no eixo Y
        // X, Y, Z
        transform.Rotate(0, 180, 0);
    }

    void PlayerJab()
    {
        //Acessa a animação do JAb
        //Ativa o gatilho de ataque Jab
        playerAnimator.SetTrigger("isJab");
    }

    void PlayerCross()
    {
        playerAnimator.SetTrigger("isCross");
    }

    IEnumerator CrossController()
    {
        comboControl = true;

        yield return new WaitForSeconds(timeCross);
        punchCount = 0;

        comboControl = false;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealt -= damage;
            playerAnimator.SetTrigger("hitDamage");
            FindFirstObjectByType<UIManager>().UpdatePlayerHealth(currentHealt);
        }
    }
}
