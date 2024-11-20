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
        List<Edge> edges = new List<Edge>()
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

        IEnumerable<Vector2> result = pathFinder.GetPath(new Vector2(-6.5f, 15), new Vector2(37, 25), edges);
    }
}
