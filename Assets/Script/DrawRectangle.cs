using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DrawRectangle : MonoBehaviour
{
    LineRenderer line;
    private bool IsPlayerDrawing;
    List<Vector3> pointsList = new List<Vector3>();
    private Vector3 MousePosition;
    public Material Alpha;
    private void Awake()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.material = Alpha;
        line.positionCount = 0;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.startColor = Color.green;
        line.endColor = Color.green;
    }
    
    public void Compare()
    {
        //Vector3[] Buffer1 = Reduce(pointsList).ToArray() ;
        //Vector3[] Buffer2 = DataManager.MagicCircleDataBase["1"].ToArray();
        //Array.Resize(ref Buffer1, line.positionCount);
        //Array.Resize(ref Buffer2, DataManager.MagicCircleDataBaseLineRenderer["1"].positionCount);

        //Debug.Log(line.GetPositions(Buffer1));
        //DataManager.MagicCircleDataBaseLineRenderer["1"].GetPositions(Buffer2);

        //float diff = DifferenceBetweenLines(Buffer1, Buffer2);
        //const float threshold = 5f;

        //Debug.Log(diff < threshold ? "Pretty close!" : "Not that close...");
    }
    void Start()
    {

    }
    private void Update()
    {
        
        Draw();
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
            Compare();
            AttachCollider(line);
            Debug.Log(IsGood(pointsList, DataManager.MagicCircleDataBaseLineRenderer["1"].gameObject,line));
            line.positionCount = 0;
            pointsList.Clear();   
        }
        if(IsPlayerDrawing)
        {
            MousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y,0);
            
            pointsList.Add(MousePosition);
            line.positionCount = pointsList.Count;
            line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
        }
    }
    /*List<Vector3> Reduce(List<Vector3> input)
    {
        // TODO fix nbPoints sometimes fails
        List<Vector3> output = new List<Vector3>();
        int step = Mathf.FloorToInt(input.Count / (DataManager.MagicCircleDataBase["1"].Count+1));
        float scale = 1;
        for (int i = 0; i < input.Count && output.Count < DataManager.MagicCircleDataBase["1"].Count; i += step)
        {
            Vector3 position = input[i] - input[0];
            scale = Mathf.Max(position.magnitude, scale);
            Debug.Log(position);
            output.Add(position);
        }
        for (int i = 0; i < output.Count; i++)
            output[i] = Vector3.ClampMagnitude(output[i], output[i].magnitude / scale);
        return output;
    }*/

    List<Vector3> ReducePointList(List<Vector3> basicList)
    {
        List<Vector3> OutputList = new List<Vector3>();
        
        return OutputList;
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
                //Debug.Log(lineEnum.Current);
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
        return ListSegments(line).Select(seg => SqrDistanceToSegment(seg.a, seg.b, point)).Min();
        //return 0.0f;
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
    void AttachCollider(LineRenderer lr)
    {
        var ec = gameObject.AddComponent<EdgeCollider2D>();
        Vector3[] points = new Vector3[lr.positionCount];
        lr.GetPositions(points);

        Vector2[] pointsList = new Vector2[lr.positionCount];

        for (int i = 0; i < lr.positionCount; i++)
        {
            pointsList[i] = ((Vector2)points[i]);
        }

        ec.points = pointsList;

    }
    private bool IsGood(List<Vector3> positions, GameObject gameObject, LineRenderer lr)
    {
        var m_Collider = gameObject.GetComponent<EdgeCollider2D>();
        int AllIsGood = 0;
        int AllIsNotGood = 0;
        foreach (Vector3 position in positions)
        {
            if(m_Collider.bounds.Contains(position))
            {
                AllIsGood += 1;
            }
            else
            {
                Debug.Log(position);
                AllIsNotGood += 1;
            }
        }
        int tempo = AllIsGood + AllIsNotGood;
        if(tempo<100)
        {
            return false;
        }
        tempo = tempo/100;
        if (AllIsGood > (tempo * 90))
        {
            Debug.Log(AllIsGood + " > " + tempo * 80);
            return true;
        }
        else Debug.Log(AllIsGood + " < " + tempo * 80); return false;
}
}
