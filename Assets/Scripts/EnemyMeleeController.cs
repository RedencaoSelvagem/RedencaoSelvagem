using UnityEngine;

public class EnemyMeleeController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;

    // Variavel que indica se o inimigo est� vivo
    public bool isDead;

    // Variaveis para controlar o lado que o inimigo est� virado
    public bool facingRight;
    public bool previousDirectionRight;

    // Variavel para armazenar posi��o do Player
    private Transform target;

    // Variaveis para movimenta��o do inimigo
    private float enemySpeed = 0.3f;
    private float currentSpeed;

    private bool isWalking;

    private float horizontalForce;
    private float verticalForce;

    // Variavel que vamos usar para controlar o intervalo de tempo que o inimigo ficar� andando vertical
    // Isso vai ajudar � dar uma aleatoriedade ao movimento do inimigo
    private float walkTimer;

    // Vari�veis para mec�nica de ataque
    private float attackRate = 1f;
    private float nextAttack;

    // Variaveis para mec�nica de dano
    public int maxHealth;
    public int currentHealth;
    public Sprite enemyImage;

    public float staggerTime = 0.5f;
    private float damageTimer;
    public bool isTakingDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
