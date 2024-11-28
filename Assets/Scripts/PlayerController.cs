using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 playerDirection;

    private bool playerFacingRight = true;

    private Animator playerAnimator;

    private bool isRunning;

    public float playerSpeed = 1f;

    private Rigidbody2D playerRigidBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Obtem e inicializa as propriedades do RigidBody2D
        playerRigidBody = GetComponent<Rigidbody2D>();

        // Obtem e inicializa as propriedades do animator
        playerAnimator = GetComponent<Animator>();
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

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        UpdateAnimator();
    }
}
