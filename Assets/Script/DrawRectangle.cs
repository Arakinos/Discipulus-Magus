using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DrawRectangle : MonoBehaviour
{
    public GameObject jaugeGO;
    public Jauge jaugeScript;
    public AudioSource validationSon;

    LineRenderer line;
    private bool IsPlayerDrawing;
    List<Vector3> pointsList = new List<Vector3>();
    private Vector3 MousePosition;
    public Material Alpha;
    Ray ray;
    RaycastHit hit;
    int AllIsGood = 0;
    int AllIsNotGood = 0;
    private void Awake()
    {
        jaugeScript = jaugeGO.GetComponent<Jauge>();
        line = gameObject.AddComponent<LineRenderer>();
        line.material = Alpha;
        line.positionCount = 0;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.sortingOrder = 2;
        line.startColor = Color.white;
        line.endColor = Color.white;
    }
    
    private void Update()
    {
        if (DataManager.currentPatern != "0")
        {
            Draw();
        }
    }
    private void Draw()
    {
        if(Input.GetMouseButtonDown(0))
        {
            IsPlayerDrawing = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            IsPlayerDrawing = false;
            try
            {
                float diff = 500;
                if (IsGood(AllIsNotGood, AllIsGood))
                {
                    diff = DifferenceBetweenLines(pointsList.ToArray(), DataManager.paternDic[DataManager.currentPatern].Alpha.ToArray());
                }
                const float threshold = 1f;
                if(diff<threshold)
                {
                    GameObject.Find("GameManager").GetComponent<DataManager>().ChangeCurrentPatern("0");
                    GameObject.Find("GameManager").GetComponent<GameManager>().numInstability = 0;
                    GameObject.Find("GameManager").GetComponent<GameManager>().couleurSort.gameObject.GetComponent<EffectsTest>().SetNormal();
                    // Tracé OK
                    jaugeScript.stabilityGauge += 1000;
                    jaugeScript.UpdateHealth();
                    validationSon.Play();
                }
            }
            catch
            {
                Debug.Log("Not that close...");
            }
            AllIsNotGood = 0;
            AllIsGood = 0;
            line.positionCount = 0;
            pointsList.Clear();   
        }
        if (IsPlayerDrawing)
        {

            MousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (line.positionCount <= 0)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name.Equals(DataManager.gm.name))
                    {
                        AllIsGood++;
                    }
                    else
                    {
                        AllIsNotGood++;
                    }
                }
                else
                {
                    AllIsNotGood++;
                }
                pointsList.Add(MousePosition);
                line.positionCount = pointsList.Count;
                line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
            }
            else if (line.positionCount > 0 && pointsList[pointsList.Count-1] != MousePosition)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.name.Equals(DataManager.gm.name))
                    {
                        AllIsGood++;
                    }
                    else
                    {
                        AllIsNotGood++;
                    }
                }
                else
                {
                    AllIsNotGood++;
                }
                pointsList.Add(MousePosition);
                line.positionCount = pointsList.Count;
                line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
            }

        }
    }

    private bool IsGood(int isnotgood, int isgood)
    {
        float tempo = isnotgood + isgood;
        if (tempo < 100)
        {
            Debug.Log(AllIsGood + " < " + tempo * 80);
            Debug.Log("false");
            return false;
        }
        tempo = tempo / 100;
        if (AllIsGood > (tempo * 90))
        {
            Debug.Log(AllIsGood + " > " + tempo * 80);
            Debug.Log(true);
            float diff = DifferenceBetweenLines(pointsList.ToArray(), DataManager.paternDic[DataManager.currentPatern].Alpha.ToArray());
            const float threshold = 5f;

            Debug.Log(diff < threshold ? "Pretty close!" : "Not that close...");
            return true;
        }
        else
        {
            Debug.Log(AllIsGood + " < " + tempo * 80);
            Debug.Log("false");
            return false;
        }
    }
    float DifferenceBetweenLines(Vector3[] drawn, Vector3[] toMatch)
    {
        float sqrDistAcc = 0f;
        float length = 0f;

        Vector3 prevPoint = toMatch[0];

        foreach (var toMatchPoint in WalkAlongLine(toMatch))
        {
            sqrDistAcc += SqrDistanceToLine(drawn, toMatchPoint);
            length += Vector3.Distance(toMatchPoint, prevPoint);

            prevPoint = toMatchPoint;
        }

        return sqrDistAcc / length;
    }

    /// <summary>
    /// Move a point from the beginning of the line to its end using a maximum step, yielding the point at each step.
    /// </summary>
    IEnumerable<Vector3> WalkAlongLine(IEnumerable<Vector3> line, float maxStep = .1f)
    {
        using (var lineEnum = line.GetEnumerator())
        {
            if (!lineEnum.MoveNext())
                yield break;

            var pos = lineEnum.Current;

            while (lineEnum.MoveNext())
            {
                Debug.Log(lineEnum.Current);
                var target = lineEnum.Current;
                while (pos != target)
                {
                    yield return pos = Vector3.MoveTowards(pos, target, maxStep);
                }
            }
        }
    }

    static float SqrDistanceToLine(Vector3[] line, Vector3 point)
    {
        return ListSegments(line)
            .Select(seg => SqrDistanceToSegment(seg.a, seg.b, point))
            .Min();
    }

    static float SqrDistanceToSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {
        var projected = ProjectPointOnLineSegment(linePoint1, linePoint1, point);
        return (projected - point).sqrMagnitude;
    }

    /// <summary>
    /// Outputs each position of the line (but the last) and the consecutive one wrapped in a Segment.
    /// Example: a, b, c, d --> (a, b), (b, c), (c, d)
    /// </summary>
    static IEnumerable<Segment> ListSegments(IEnumerable<Vector3> line)
    {
        using (var pt1 = line.GetEnumerator())
        using (var pt2 = line.GetEnumerator())
        {
            pt2.MoveNext();

            while (pt2.MoveNext())
            {
                pt1.MoveNext();

                yield return new Segment { a = pt1.Current, b = pt2.Current };
            }
        }
    }
    struct Segment
    {
        public Vector3 a;
        public Vector3 b;
    }

    //This function finds out on which side of a line segment the point is located.
    //The point is assumed to be on a line created by linePoint1 and linePoint2. If the point is not on
    //the line segment, project it on the line using ProjectPointOnLine() first.
    //Returns 0 if point is on the line segment.
    //Returns 1 if point is outside of the line segment and located on the side of linePoint1.
    //Returns 2 if point is outside of the line segment and located on the side of linePoint2.
    static int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {
        Vector3 lineVec = linePoint2 - linePoint1;
        Vector3 pointVec = point - linePoint1;

        if (Vector3.Dot(pointVec, lineVec) > 0)
        {
            return pointVec.magnitude <= lineVec.magnitude ? 0 : 2;
        }
        else
        {
            return 1;
        }
    }

    //This function returns a point which is a projection from a point to a line.
    //The line is regarded infinite. If the line is finite, use ProjectPointOnLineSegment() instead.
    static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point)
    {
        //get vector from point on line to point in space
        Vector3 linePointToPoint = point - linePoint;
        float t = Vector3.Dot(linePointToPoint, lineVec);
        return linePoint + lineVec * t;
    }

    //This function returns a point which is a projection from a point to a line segment.
    //If the projected point lies outside of the line segment, the projected point will
    //be clamped to the appropriate line edge.
    //If the line is infinite instead of a segment, use ProjectPointOnLine() instead.
    static Vector3 ProjectPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
    {
        Vector3 vector = linePoint2 - linePoint1;
        Vector3 projectedPoint = ProjectPointOnLine(linePoint1, vector.normalized, point);

        switch (PointOnWhichSideOfLineSegment(linePoint1, linePoint2, projectedPoint))
        {
            case 0:
                return projectedPoint;
            case 1:
                return linePoint1;
            case 2:
                return linePoint2;
            default:
                //output is invalid
                return Vector3.zero;
        }
    }
}

