using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMeleeController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    // Variavel que indica se o inimigo está vivo
    public bool isDead;

    // Variaveis para controlar o lado que o inimigo está virado
    public bool facingRight;
    public bool previousDirectionRight;

    // Variavel para armazenar posição do Player
    private Transform target;

    // Variaveis para movimentação do inimigo
    private float enemySpeed = 0.3f;
    private float currentSpeed;

    private bool isWalking;

    private float horizontalForce;
    private float verticalForce;

    // Variavel que vamos usar para controlar o intervalo de tempo que o inimigo ficará andando vertical
    // Isso vai ajudar à dar uma aleatoriedade ao movimento do inimigo
    private float walkTimer;

    // Variáveis para mecânica de ataque
    private float attackRate = 1f;
    private float nextAttack;

    // Variaveis para mecânica de dano
    public int maxHealth;
    public int currentHealth;
    public Sprite enemyImage;

    public float staggerTime = 0.5f;
    private float damageTimer;
    public bool isTakingDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Buscar o Player e armazenar sua posição
        target = FindAnyObjectByType<PlayerController>().transform;

        // Inicializar a velocidade do inimigo
        currentSpeed = enemySpeed;
    }

    void Update()
    {
        // Verificar se o player está para esquerda ou para direira
        // e com isso eterminar o lado que o inimigo ficará virado
        if (target.position.x < this.transform.position.x)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }

        // Se faceRight for true, vamos virar o inimigo em 180 graus no eixo Y,
        // se não, vamos virar o inimigo para a esquerda

        // Se o player à direita e a direção anterior não era direita (estava olhando para esquerda)
        if (facingRight && !previousDirectionRight)
        {
            this.transform.Rotate(0, 180, 0);
            previousDirectionRight = true;
        }

        if (!facingRight && previousDirectionRight)
        {
            this.transform.Rotate(0, -180, 0);
            previousDirectionRight = false;
        }

        // Iniciar o timer do caminhar do inimigo
        walkTimer += Time.deltaTime;

        // Gerenciar a animação do inimigo
        if (horizontalForce == 0 && verticalForce == 0)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        UpdateAnimator();
    }

    void UpdateAnimator ()
    {
        animator.SetBool("IsWalking", isWalking);
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            isTakingDamage = true;

            currentHealth -= damage;

            animator.SetTrigger("HitDamage");

            FindFirstObjectByType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyImage);

            if (currentHealth <= 0)
            {
                isDead = true;

                ZeroSpeed();

                animator.SetTrigger("Dead");
            }
        }
    }

    void ZeroSpeed()
    {
        currentSpeed = 0;
    }
}
