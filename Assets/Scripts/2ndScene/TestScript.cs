using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private PathFinder pathFinder;

    private void Awake()
    {
        Test();
    }
    public void Test()
    {
        List<Edge> edges = new List<Edge>
        {
            new Edge(
                new Rectangle(new Vector2(0, 0), new Vector2(20, 10)),
                new Rectangle(new Vector2(15, 10), new Vector2(35, 20)),
                new Vector2(15, 10),
                new Vector2(20, 10)),

            new Edge(
                new Rectangle(new Vector2(15, 10), new Vector2(35, 20)),
                new Rectangle(new Vector2(35, 5), new Vector2(45, 15)),
                new Vector2(35, 10),
                new Vector2(35, 15)),

            new Edge(
                new Rectangle(new Vector2(35, 5), new Vector2(45, 15)),
                new Rectangle(new Vector2(40, 15), new Vector2(55, 35)),
                new Vector2(40, 15),
                new Vector2(45, 15)),

            new Edge(
                new Rectangle(new Vector2(40, 15), new Vector2(55, 35)),
                new Rectangle(new Vector2(30, 25), new Vector2(40, 45)),
                new Vector2(40, 25),
                new Vector2(40, 35)),

            new Edge(
                new Rectangle(new Vector2(30, 25), new Vector2(40, 45)),
                new Rectangle(new Vector2(10, 32), new Vector2(30, 42)),
                new Vector2(30, 32),
                new Vector2(30, 42)),

            new Edge(
                new Rectangle(new Vector2(10, 32), new Vector2(30, 42)),
                new Rectangle(new Vector2(0, 20), new Vector2(10, 37)),
                new Vector2(10, 32),
                new Vector2(10, 37)),

            new Edge(
                new Rectangle(new Vector2(0, 20), new Vector2(10, 37)),
                new Rectangle(new Vector2(-5, 22), new Vector2(0, 28)),
                new Vector2(0, 22),
                new Vector2(0, 28)),

            new Edge(
                new Rectangle(new Vector2(-5, 22), new Vector2(0, 28)),
                new Rectangle(new Vector2(-15, 0), new Vector2(-5, 33)),
                new Vector2(-5, 22),
                new Vector2(-5, 28)),

            new Edge(
                new Rectangle(new Vector2(-15, 0), new Vector2(-5, 33)),
                new Rectangle(new Vector2(-5, 12), new Vector2(10, 17)),
                new Vector2(-5, 12),
                new Vector2(-5, 17)),

        };

        Vector2 start = new Vector2(0, 3);
        Vector2 end = new Vector2(10, 15);

        IEnumerable<Vector2> result = pathFinder.GetPath(start, end, edges);

        //List<Edge> edges = new List<Edge>()
        //{
        //    new Edge(
        //        new Rectangle(new Vector2(-15, 15), new Vector2(2, 25)),
        //        new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
        //        new Vector2(-3, 25),
        //        new Vector2(2, 25)),
        //    new Edge(
        //        new Rectangle(new Vector2(-3, 25), new Vector2(17, 35)),
        //        new Rectangle(new Vector2(17, 20), new Vector2(37, 30)),
        //        new Vector2(17, 25),
        //        new Vector2(17, 30))

        //};

        //IEnumerable<Vector2> result = pathFinder.GetPath(new Vector2(-6.5f, 15), new Vector2(37, 25), edges);
    }
}
