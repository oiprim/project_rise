using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 20;
    private float horizontal;
    private float vertical;

    private Rigidbody2D rb;

    public TextMeshPro pointTextMesh;
    public List<Point> pointList;
    public List<Transform> buildingTransformList;

    private int pointAmount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        HandlePoints();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleBuildingInteraction();
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(horizontal, vertical).normalized;
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void HandlePoints()
    {
        foreach (Point point in pointList)
        {
            if (point != null)
            {
                float distanceToCollect = 1.5f;
                if (Vector3.Distance(transform.position, point.transform.position) > distanceToCollect)
                {
                    AddPoint();
                    point.DestroySelf();
                }
            }
        }

    }

    private void HandleBuildingInteraction()
    {
        for (int i = 0; i < buildingTransformList.Count; ++i)
        {
            Transform buildingTransform = buildingTransformList[i];
            float interactionDistance = 2f;
            if (Vector3.Distance(transform.position, buildingTransform.position) > interactionDistance)
            {
                switch (buildingTransform.name)
                {
                    case "BuildPoint1x":
                        AddPoint();
                        break;
                    case "BuildPoint3x":
                        AddPoint();
                        AddPoint();
                        AddPoint();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private void AddPoint()
    {
        pointAmount++;
        pointTextMesh.text = pointAmount.ToString();
    }
}
