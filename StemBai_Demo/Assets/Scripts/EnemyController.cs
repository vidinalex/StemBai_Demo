using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float timer = 0.5f;
    [SerializeField]
    private MatrixPosition matrixPosition;
    private int posX = 16, posY = 8;
    [SerializeField]
    private Sprite[] directionSprites;
    [SerializeField]
    private Sprite[] directionSpritesDirty;
    private SpriteRenderer spriteRenderer;
    private Direction direction;
    private Vector2 target;
    private float startX = -9.4f, startY = -4f, minDistToInteract = 0.5f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = transform.position;
    }

    private void UpdateSprite()
    {
        spriteRenderer.sprite = directionSprites[(int)direction];
    }

    private void ChooseDirection()
    {
        direction = (Direction)Random.Range(0, 4);
        UpdateSprite();

        Vector2 target = matrixPosition.CalcDestination(startX, startY, posX, posY, direction);
        if (target.x != MatrixPosition.Blocker)
        {
            Move(target);
        }
    }
    private void Move(Vector2 target)
    {
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
                if (bomb != null && Vector2.Distance(bomb.transform.position, target) < minDistToInteract)
                {
                    Destroy(bomb);
                    matrixPosition.UpdateBombsOnMap();
                    directionSprites = directionSpritesDirty;
                    UpdateSprite();
                }
            }
    }

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target, 0.1f);

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = 0.5f;
            ChooseDirection();
        }
    }
}
