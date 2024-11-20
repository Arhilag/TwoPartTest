using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder : MonoBehaviour, IPathFinder
{
    private List<Edge> edgesList = new List<Edge>();
    private List<Vector2> points = new List<Vector2>();

    private struct PointData
    {
        public Vector2 Point;
        public int Edge;
    }

    public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {
        edgesList = edges.ToList();
        List<Vector2> points = new List<Vector2>();

        int lastEdge = 1;
        points.Add(A);
        while (lastEdge < edgesList.Count)
        {
            bool verticalDirection = CheckVerticalPointsDirection(points[points.Count - 1], edgesList, lastEdge);
            bool horizontalDirection = CheckHorizontalPointsDirection(points[points.Count - 1], edgesList, lastEdge);

            if(!verticalDirection && !horizontalDirection)
            {
                if(edgesList[lastEdge - 1].Start.x == edgesList[lastEdge - 1].First.Max.x || edgesList[lastEdge - 1].Start.x == edgesList[lastEdge - 1].First.Min.x)
                {
                    var difference = TurnVertical(edgesList, lastEdge, points[points.Count - 1]);
                    if (difference.x != 0 || difference.y != 0)
                    {
                        var newPoint = points[points.Count - 1] + difference;
                        if (newPoint.x < edgesList[lastEdge - 1].First.Max.x && newPoint.x > edgesList[lastEdge - 1].First.Min.x &&
                            newPoint.y < edgesList[lastEdge - 1].First.Max.y && newPoint.x > edgesList[lastEdge - 1].First.Min.y)
                        {
                            points[points.Count - 1] = newPoint;
                        }
                        else
                        {
                            points.Add(edgesList[lastEdge - 1].Start);
                            lastEdge++;
                            continue;
                        }
                    }
                    verticalDirection = true;
                }
                else
                {
                    var difference = TurnHorizontal(edgesList, lastEdge, points[points.Count - 1]);
                    if (difference.x != 0 || difference.y != 0)
                    {
                        var newPoint = points[points.Count - 1] + difference;
                        if (newPoint.x < edgesList[lastEdge - 1].First.Max.x && newPoint.x > edgesList[lastEdge - 1].First.Min.x &&
                            newPoint.y < edgesList[lastEdge - 1].First.Max.y && newPoint.x > edgesList[lastEdge - 1].First.Min.y)
                        {
                            points[points.Count - 1] = newPoint;
                        }
                        else
                        {
                            points.Add(edgesList[lastEdge - 1].Start);
                            lastEdge++;
                            continue;
                        }
                    }
                    horizontalDirection = true;
                }
            }

            var pointData = GetPoint(A, C, edgesList, 1, verticalDirection, horizontalDirection);
            points.Add(pointData.Point);
            lastEdge = pointData.Edge;
        }
        if(points[points.Count - 1] != C)
        {
            points.Add(C);
        }

        return points;
    }

    private PointData GetPoint(Vector2 A, Vector2 C, List<Edge> edgesList, int startEdges, bool verticalDirection, bool horizontalDirection)
    {
        PointData data = new PointData();

        for (int i = startEdges; i < edgesList.Count; i++)
        {
            var firstPoint = edgesList[i].Start;
            var secondPoint = edgesList[i].End;

            bool checkStart = false;
            bool checkEnd = false;

            if (horizontalDirection)
            {
                checkStart = CheckPreviousHorizontalEdges(edgesList, A, firstPoint, i);
                checkEnd = CheckPreviousHorizontalEdges(edgesList, A, secondPoint, i);
            }

            if (verticalDirection)
            {
                checkStart = CheckPreviousVerticalEdges(edgesList, A, firstPoint, i);
                checkEnd = CheckPreviousVerticalEdges(edgesList, A, secondPoint, i);
            }

            if (!checkStart && !checkEnd)
            {
                data.Point = edgesList[i - 1].Start;
                data.Edge = i + 1;
                return data;
            }
            else if (checkStart)
            {
                data.Point = edgesList[i].Start;
                data.Edge = i + 2;
            }
            else if (checkEnd)
            {
                data.Point = edgesList[i].End;
                data.Edge = i + 2;
            }
        }

        if ((horizontalDirection && CheckPreviousHorizontalEdges(edgesList, A, C, edgesList.Count - 1)) ||
           (verticalDirection && CheckPreviousVerticalEdges(edgesList, A, C, edgesList.Count - 1)))
        {
            data.Point = C;
            data.Edge = edgesList.Count;
        }

        return data;
    }

    private Vector2 TurnVertical(List<Edge> edgesList, int startEdges, Vector2 lastPoint)
    {
        Vector2 difference = Vector2.zero;

        var firstPoint = edgesList[startEdges].Start;
        var secondPoint = edgesList[startEdges].End;

        var distNegative = edgesList[startEdges - 1].End.y - firstPoint.y;
        var distPositive = edgesList[startEdges - 1].Start.y - secondPoint.y;

        if (distNegative > 0)
        {
            if(startEdges > 2)
            {
                var fHight = lastPoint.y - edgesList[startEdges - 3].Start.y;
                var sHight = lastPoint.y + distNegative - edgesList[startEdges - 3].Start.y;
                var sWidth = lastPoint.x - edgesList[startEdges - 3].Start.x;

                var fWidth = ((fHight / sHight) * sWidth) + edgesList[startEdges - 3].Start.x;

                return new Vector2(fWidth, lastPoint.y + distNegative);
            }

            return new Vector2(lastPoint.x, lastPoint.y + distNegative);
        }
        else if (distPositive < 0)
        {
            if (startEdges > 2)
            {
                var fHight = lastPoint.y - edgesList[startEdges - 3].Start.y;
                var sHight = lastPoint.y + distPositive - edgesList[startEdges - 3].Start.y;
                var sWidth = lastPoint.x - edgesList[startEdges - 3].Start.x;

                var fWidth = ((fHight / sHight) * sWidth) + edgesList[startEdges - 3].Start.x;

                return new Vector2(fWidth, lastPoint.y + distPositive);
            }

            return new Vector2(lastPoint.x, lastPoint.y + distPositive);
        }

        return difference;
    }

    private Vector2 TurnHorizontal(List<Edge> edgesList, int startEdges, Vector2 lastPoint)
    {
        Vector2 difference = Vector2.zero;

        var firstPoint = edgesList[startEdges].Start;
        var secondPoint = edgesList[startEdges].End;

        var distNegative = edgesList[startEdges - 1].Start.x - secondPoint.x;
        var distPositive = edgesList[startEdges - 1].End.x - firstPoint.x;

        if (distNegative > 0)
        {
            if (startEdges > 2)
            {
                var fHight = lastPoint.x - edgesList[startEdges - 3].Start.x;
                var sHight = lastPoint.x + distNegative - edgesList[startEdges - 3].Start.x;
                var sWidth = lastPoint.y - edgesList[startEdges - 3].Start.y;

                var fWidth = ((fHight / sHight) * sWidth) + edgesList[startEdges - 3].Start.y;

                return new Vector2(fWidth, lastPoint.x + distNegative);
            }

            return new Vector2(lastPoint.y, lastPoint.x + distNegative);
        }
        else if (distPositive < 0)
        {
            if (startEdges > 2)
            {
                var fHight = lastPoint.x - edgesList[startEdges - 3].Start.x;
                var sHight = lastPoint.x + distPositive - edgesList[startEdges - 3].Start.x;
                var sWidth = lastPoint.y - edgesList[startEdges - 3].Start.y;

                var fWidth = ((fHight / sHight) * sWidth) + edgesList[startEdges - 3].Start.y;

                return new Vector2(fWidth, lastPoint.x + distPositive);
            }

            return new Vector2(lastPoint.y, lastPoint.x + distPositive);
        }

        return difference;
    }

    private bool CheckHorizontalPointsDirection(Vector2 A, List<Edge> edgesList, int lastEdge)
    {
        return ((A.y == edgesList[lastEdge - 1].First.Min.y && edgesList[lastEdge - 1].Start.y == edgesList[lastEdge - 1].First.Max.y) ||
           (A.y == edgesList[lastEdge - 1].First.Max.y && edgesList[lastEdge - 1].Start.y == edgesList[lastEdge - 1].First.Min.y));
    }

    private bool CheckVerticalPointsDirection(Vector2 A, List<Edge> edgesList, int lastEdge)
    {
        return ((A.x == edgesList[lastEdge - 1].First.Min.x && edgesList[lastEdge - 1].Start.x == edgesList[lastEdge - 1].First.Max.x) ||
           (A.x == edgesList[lastEdge - 1].First.Max.x && edgesList[lastEdge - 1].Start.x == edgesList[lastEdge - 1].First.Min.x));
    }

    private bool CheckPreviousHorizontalEdges(List<Edge> edgesList, Vector2 A, Vector2 point, int i)
    {
        for (int j = i - 1; j >= 0; j--)
        {
            var fHight = edgesList[j].Start.y - A.y;
            var sHight = point.y - A.y;
            var sWidth = point.x - A.x;

            var fWidth = ((fHight / sHight) * sWidth) + A.x;

            if (!(fWidth >= edgesList[j].Start.x && fWidth <= edgesList[j].End.x))
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckPreviousVerticalEdges(List<Edge> edgesList, Vector2 A, Vector2 point, int i)
    {
        for (int j = i - 1; j >= 0; j--)
        {
            var fHight = edgesList[j].Start.x - A.x;
            var sHight = point.x - A.x;
            var sWidth = point.y - A.y;

            var fWidth = ((fHight / sHight) * sWidth) + A.y;

            if (!(fWidth >= edgesList[j].Start.y && fWidth <= edgesList[j].End.y))
            {
                return false;
            }
        }
        return true;
    }

    private void Awake()
    {
        edgesList = new List<Edge>()
        {
            new Edge(
                new Rectangle(new Vector2(-15, 15), new Vector2(2, 25)),
                new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
                new Vector2(-3, 25),
                new Vector2(2, 25)),
            new Edge(
                new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
                new Rectangle(new Vector2(17, 20), new Vector2(37, 30)),
                new Vector2(17, 25),
                new Vector2(17, 30))

        };

        points = GetPath(new Vector2(-6.5f, 15), new Vector2(37, 25), edgesList).ToList();

        //edges = new List<Edge>()
        //{
        //    new Edge(
        //        new Rectangle(new Vector2(-10, 10), new Vector2(0, 20)),
        //        new Rectangle(new Vector2(-5, 20), new Vector2(5, 30)),
        //        new Vector2(-5, 20),
        //        new Vector2(0, 20)),
        //    new Edge(
        //        new Rectangle(new Vector2(-5, 20), new Vector2(5, 30)),
        //        new Rectangle(new Vector2(-5, 30), new Vector2(5, 35)),
        //        new Vector2(-5, 30),
        //        new Vector2(5, 30)),
        //    new Edge(
        //        new Rectangle(new Vector2(-5, 30), new Vector2(5, 35)),
        //        new Rectangle(new Vector2(0, 35), new Vector2(5, 40)),
        //        new Vector2(0, 35),
        //        new Vector2(5, 35)),
        //    new Edge(
        //        new Rectangle(new Vector2(0, 35), new Vector2(5, 40)),
        //        new Rectangle(new Vector2(5, 35), new Vector2(10, 40)),
        //        new Vector2(5, 35),
        //        new Vector2(5, 40))

        //};

        //points = GetPath(new Vector2(-5f, 10), new Vector2(10, 37), edges).ToList();
    }

    private void Update()
    {
        foreach (var edge in edgesList)
        {
            var rectangles = new List<Rectangle>()
            {
                edge.First,
                edge.Second
            };

            foreach (var rectangle in rectangles)
            {
                Debug.DrawLine(rectangle.Min, new Vector3(rectangle.Min.x, rectangle.Max.y));
                Debug.DrawLine(rectangle.Min, new Vector3(rectangle.Max.x, rectangle.Min.y));
                Debug.DrawLine(rectangle.Max, new Vector3(rectangle.Min.x, rectangle.Max.y));
                Debug.DrawLine(rectangle.Max, new Vector3(rectangle.Max.x, rectangle.Min.y));
            }

            //Debug.DrawLine(edge.Start, edge.End, Color.yellow);
        }

        if(points.Count > 0)
        {
            for (int i = 1; i < points.Count; i++)
            {
                Debug.DrawLine(points[i - 1], points[i], Color.blue);
            }
        }
    }
}
