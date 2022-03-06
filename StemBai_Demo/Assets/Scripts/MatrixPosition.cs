using UnityEngine;

public class MatrixPosition : MonoBehaviour
{
    public const int Blocker = -100;

    [SerializeField]
    private int matrixSizeX, matrixSizeY;
    private float dynamicOffsetX = 0.2f, dynamicOffsetY = 0.25f;

    private GameObject[] bombs = null;

    public Vector2 CalcDestination(float startX, float startY, int matrixX, int matrixY, Direction dir)
    {
        matrixX += (int)Offset(dir).x;
        matrixY += (int)Offset(dir).y;

        if (matrixX < matrixSizeX && matrixX > -1 && //avoid borders
            matrixY < matrixSizeY && matrixY > -1)
        {
            if (!(matrixX % 2 == 1 && matrixY % 2 == 1)) //avoid stones
                return new Vector2(startX + matrixX + ((int)matrixX / 2) * dynamicOffsetX + ((int)matrixY / 2) * dynamicOffsetY, //calc destination position, in order not to create a matrix of possible positions
                        startY + matrixY);
        }
        return new Vector2(Blocker, 0);
    }

    public Vector2 Offset(Direction dir) => dir switch //find out which cell to move to
    {
        Direction.Right => new Vector2(1, 0),
        Direction.Down => new Vector2(0, -1),
        Direction.Left => new Vector2(-1, 0),
        Direction.Up => new Vector2(0, 1)
    };

    public void UpdateBombsOnMap()
    {
        bombs = GameObject.FindGameObjectsWithTag("Bomb");
    }

    public GameObject[] GetBombsOnMap()
    {
        return bombs;
    }
}
