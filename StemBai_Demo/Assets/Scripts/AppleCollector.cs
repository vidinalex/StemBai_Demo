using UnityEngine;
using UnityEngine.UI;

public class AppleCollector : MonoBehaviour
{
    [SerializeField]
    private MatrixPosition matrixPosition;
    [SerializeField]
    private GameObject applePrefab;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Text scoreGUI;
    private GameObject apple;
    private float startX = -9.4f, startY = -4.4f;
    private float minDist = 0.5f;
    private float score = 0;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Create();
        DistanceToCollect();
    }

    private void Create()
    {
        if (apple == null)
        {
            Vector2 spawnTarget = matrixPosition.CalcDestination(startX, startY, Random.Range(0, 17), Random.Range(0, 9), (Direction)Random.Range(0, 4));
            if (spawnTarget.x != MatrixPosition.Blocker)
                apple = Instantiate(applePrefab, spawnTarget, Quaternion.identity);
        }
    }

    private void DistanceToCollect()
    {
        if (apple != null && Vector2.Distance(Player.transform.position, apple.transform.position) <= minDist)
        {
            Destroy(apple);
            score++;
            UpdateScore();
        }
    }

    private void UpdateScore()
    {
        scoreGUI.text = score.ToString();
    }
}
