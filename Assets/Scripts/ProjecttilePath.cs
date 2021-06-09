using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjecttilePath : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private Transform pointParent;

    List<Transform> pathPoints;

    public int maxPoints = 20;
    public float offset = 0.5f;

    private float deltaTime;


    Vector2 pointPos;


    private void Start()
    {
        pathPoints = new List<Transform>();
        Hide();
        for (int i = 0; i < maxPoints; i++)
        {
            Transform tr = Instantiate(pointPrefab).transform;
            tr.SetParent(pointParent);
            pathPoints.Add(tr);
        }
    }

    public void UpdatePoints(Vector2 startPos , Vector2 force)
    {
        deltaTime = offset;

        for (int i = 0; i < maxPoints; i++)
        {
            pointPos.x = startPos.x + force.x * deltaTime;
            pointPos.y = startPos.y + force.y * deltaTime - Physics2D.gravity.magnitude * deltaTime * deltaTime * 0.5f;

            pathPoints[i].position = pointPos;

            deltaTime += offset;
        }
    }


    public void Hide()
    {
        pointParent.gameObject.SetActive(false);
    }

    public void Show()
    {
        pointParent.gameObject.SetActive(true);
    }
}
