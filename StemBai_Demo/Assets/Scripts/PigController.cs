using UnityEngine;
using UnityEngine.UI;

public class PigController : MonoBehaviour
{
    [SerializeField]
    private MatrixPosition matrixPosition;
    [SerializeField]
    private Sprite[] directionSprites;
    [SerializeField]
    private GameObject Bomb;
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private CameraShake shake;
    private float barFillRate = 0.1f;
    private SpriteRenderer spriteRenderer;
    private Direction direction;
    private Vector2 target;
    private GameObject enemy;
    private int posX = 0, posY = 0;
    private float startX = -9.4f, startY = -4.25f, minDistToInteract = 0.5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = transform.position;
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void ChangeDirection(Direction dir)
    {
        direction = dir;
        spriteRenderer.sprite = directionSprites[(int)direction];

        Vector2 target = matrixPosition.CalcDestination(startX, startY, posX, posY, dir);
        if (target.x != MatrixPosition.Blocker) Move(target); //target.x == blocker only if we cant move target cell
    }

    private void Move(Vector2 target)
    {
        //Debug.Log(target);
        Vector2 tempOffset = matrixPosition.Offset(direction);
        posX += (int)tempOffset.x;
        posY += (int)tempOffset.y;

        this.target = target;
        CheckTargetPositionForBombs();
    }

    private void CheckTargetPositionForBombs()
    {
        GameObject[] bombs = matrixPosition.GetBombsOnMap();
        if (bombs != null)
            foreach (GameObject bomb in bombs)
            {
                if (bomb != null && Vector2.Distance(bomb.transform.position, target) < minDistToInteract && bomb.GetComponent<BombTimer>().GetTimer())
                {
                    Destroy(bomb);
                    matrixPosition.UpdateBombsOnMap();
                    shake.Shake();
                }
            }
    }

    private void CheckForEnemy()
    {
        if (Vector2.Distance(enemy.transform.position, transform.position) < minDistToInteract)
        {
            shake.Shake();
        }
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target, 0.1f);
        progressBar.fillAmount += Time.deltaTime * barFillRate;
        CheckForEnemy();
    }

    public void CreateBomb()
    {
        if (progressBar.fillAmount == 1)
        {
            Instantiate(Bomb, transform.position, Quaternion.identity);
            matrixPosition.UpdateBombsOnMap();
            progressBar.fillAmount = 0;
        }
    }
}
