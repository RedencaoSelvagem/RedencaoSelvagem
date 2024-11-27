using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 playerDirection;
    private bool playerFacingRight = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerMove();
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
        
    }
}
