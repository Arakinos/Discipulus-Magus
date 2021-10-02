using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static Dictionary<string, List<Vector3>> MagicCircleDataBase = new Dictionary<string, List<Vector3>>();
    public static Dictionary<string, LineRenderer> MagicCircleDataBaseLineRenderer = new Dictionary<string, LineRenderer>();
    public LineRenderer beta;
    // Start is called before the first frame update
    public void Awake()
    {
        GameObject gameObject = new GameObject("Hi");
        beta = gameObject.AddComponent<LineRenderer>();
        beta.startWidth = 0.3f;
        beta.endWidth = 0.3f;
        List<Vector3> Alpha = new List<Vector3>();
        
        Alpha.Add(new Vector3(1, 1, 1));
        Alpha.Add(new Vector3(1, -1, 1));
        Alpha.Add(new Vector3(-1, -1, 1));
        Alpha.Add(new Vector3(-1, 1, 1));
        Alpha.Add(new Vector3(1, 1, 1));
        Alpha.Add(new Vector3(1.5f, 0, 1));
        int i = 0;
        foreach (Vector3 vector in Alpha)
        {
            beta.positionCount = i+1;
            beta.SetPosition(i, vector);
            i++;
        }
        AttachCollider(beta, gameObject);
        MagicCircleDataBase.Add("1", Alpha);
        MagicCircleDataBaseLineRenderer.Add("1", beta);
        //Destroy(gameObject);
    }
    void Start()
    {
        
    }
    void AttachCollider(LineRenderer lr, GameObject gameObject)
    {
        var ec = gameObject.AddComponent<EdgeCollider2D>();
        Vector3[] points = new Vector3[lr.positionCount];
        lr.GetPositions(points);

        Vector2[] pointsList = new Vector2[lr.positionCount + lr.positionCount];

        for (int i = 0; i < lr.positionCount; i++)
        {
            //pointsList[i] = ((Vector2)points[i]);
            //pointsList[i] = new Vector2(points[i].x+1, points[i].y+1);
            if(points[i].x>0)
            {
                if (points[i].y > 0)
                {
                    pointsList[i] = new Vector2(points[i].x + 0.15f, points[i].y + 0.15f);
                    pointsList[i+lr.positionCount] = new Vector2(points[i].x - 0.15f, points[i].y - 0.15f);
                }
                if (points[i].y < 0)
                {
                    pointsList[i] = new Vector2(points[i].x + 0.15f, points[i].y - 0.15f);
                    pointsList[i + lr.positionCount] = new Vector2(points[i].x - 0.15f, points[i].y + 0.15f);
                }
            }
            if (points[i].x < 0)
            {
                if (points[i].y < 0)
                {
                    pointsList[i] = new Vector2(points[i].x - 0.15f, points[i].y - 0.15f);
                    pointsList[i + lr.positionCount] = new Vector2(points[i].x + 0.15f, points[i].y + 0.15f);
                }
                if (points[i].y > 0)
                {
                    pointsList[i] = new Vector2(points[i].x - 0.15f, points[i].y + 0.15f);
                    pointsList[i + lr.positionCount] = new Vector2(points[i].x + 0.15f, points[i].y - 0.15f);
                }
            }
        }

        ec.points = pointsList;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
