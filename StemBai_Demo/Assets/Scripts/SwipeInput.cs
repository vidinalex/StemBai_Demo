using UnityEngine;

[RequireComponent(typeof(PigController))]
public class SwipeInput : MonoBehaviour
{
    private PigController pigController;

    private Vector2 initialCord;
    private Vector2 releaseCord;

    private float minDist = 50f;

    private void Start()
    {
        pigController = GetComponent<PigController>();
    }

    private void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                initialCord = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                releaseCord = touch.position;
                CalcVector();
            }
        }
    }

    private void CalcVector()
    {
        float
            tempX = initialCord.x - releaseCord.x,
            tempY = initialCord.y - releaseCord.y;
        if (Mathf.Abs(tempX) + Mathf.Abs(tempY) > minDist) // check min distance to prevent accidental move
            if (Mathf.Abs(tempX) > Mathf.Abs(tempY)) // calc direction
            {
                if (tempX >= 0)
                {
                    pigController.ChangeDirection(Direction.Left);
                    //Debug.Log("Left");
                }
                else
                {
                    pigController.ChangeDirection(Direction.Right);
                    //Debug.Log("Right");
                }
            }
            else
            {
                if (tempY >= 0)
                {
                    pigController.ChangeDirection(Direction.Down);
                    //Debug.Log("Down");
                }
                else
                {
                    pigController.ChangeDirection(Direction.Up);
                    //Debug.Log("Up");
                }
            }
    }
}
