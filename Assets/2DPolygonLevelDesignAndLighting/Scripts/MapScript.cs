using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class MapScript : MonoBehaviour {
#if UNITY_EDITOR

    [Tooltip("Should the paralax effect be inwards? Press 'Update Paralax' to apply changes.")]
    public bool inwards = true;
    [Tooltip("Should here be a paralax effect? Press 'Update Paralax' to apply changes.")]
    public bool paralax = false;
    [Tooltip("Materials of the two paralax layers. Press 'Update Paralax' to apply changes.")]
    public Material paralaxMaterial1, paralaxMaterial2;
    [Tooltip("Array of all details to place randomly. Put in the name (path in resources folder) of the sprite (type: multiple).")]
    public string detailPath;
    [Tooltip("Density value between 0 and 1.")]
    public float detailDensity = 0.3f;

    private Sprite[] details;

    public void replaceDetails()
    {
        if (detailPath == null)
        {
            Debug.LogError("Cannot place details, the parameter 'detailPath' is null!");
            return;
        }
        details = Resources.LoadAll<Sprite>(detailPath);
        if (details == null)
        {
            Debug.LogError("Cannot place details, the sprite array at path " + detailPath + " in the Resources folder is null!");
            return;
        }
        if (detailDensity < 0 || detailDensity > 1)
        {
            Debug.LogError("Cannot place details, 'detailDensity' must be a value between 0 and 1!");
            return;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            if (g.name.Equals("Details"))
            {
                GameObject.DestroyImmediate(g);
            }
        }

        GameObject d = new GameObject("Details");
        d.transform.SetParent(transform);

        PolygonCollider2D pc2 = gameObject.GetComponent<PolygonCollider2D>();
        Vector2[] points = pc2.points;
        for(int i = 1; i < points.Length-1; i++)
        {
            if(Random.Range(0f, 1f) < detailDensity)
            {
                if(Random.Range(0, 2) == 0)
                {
                    //--On Corner--
                    GameObject g = new GameObject();
                    g.AddComponent<SpriteRenderer>().sprite = details[Random.Range(0, details.Length)];
                    g.GetComponent<SpriteRenderer>().sortingLayerName = "FG";
                    g.transform.position = points[i] + (Vector2)transform.position;
                    g.transform.SetParent(d.transform);
                    float scale = Random.Range(0.6f, 0.8f);
                    g.transform.localScale = new Vector3(scale, scale, scale);
                    float a1 = Mathf.Rad2Deg * Mathf.Atan2(points[i].y - points[i - 1].y, points[i].x - points[i - 1].x);
                    float a2 = Mathf.Rad2Deg * Mathf.Atan2(points[i + 1].y - points[i].y, points[i + 1].x - points[i].x);
                    float zRot = a1 + (a2 - a1) / 2f;
                    if(a2 - a1 < 180)g.transform.localEulerAngles = new Vector3(0f, 0f, zRot);
                    else g.transform.localEulerAngles = new Vector3(0f, 0f, 360 - zRot);
                }
                else
                {
                    //--On Edge--
                    GameObject g = new GameObject();
                    g.AddComponent<SpriteRenderer>().sprite = details[Random.Range(0, details.Length)];
                    g.GetComponent<SpriteRenderer>().sortingLayerName = "FG";
                    g.transform.position = (points[i] - points[i-1]) * Random.Range(0.25f, 0.75f) + points[i-1] + (Vector2)transform.position;
                    g.transform.SetParent(d.transform);
                    float scale = Random.Range(0.6f, 0.8f);
                    g.transform.localScale = new Vector3(scale, scale, scale);
                    float dx = points[i].x - points[i - 1].x;
                    float dy = points[i].y - points[i - 1].y;
                    g.transform.localEulerAngles = new Vector3(0, 0, Vector2.Angle(new Vector2(0, 1), new Vector2(-dy, dx)));
                }
                
            }
        }

    }

    public void replaceParalax()
    {
        PolygonCollider2D pc2 = gameObject.GetComponent<PolygonCollider2D>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            if (g.name.Equals("Paralax"))
            {
                GameObject.DestroyImmediate(g);
            }
        }

        if (!paralax)
        {
            Debug.LogError("Cannot update Paralax Background, set 'paralax' to true first!");
            return;
        }

        GameObject pivot = null;

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject g = transform.GetChild(i).gameObject;
            if (g.name.Equals("Pivot"))
            {
                if(pivot == null)
                {
                    pivot = g;
                }
                else
                {
                    GameObject.DestroyImmediate(g);
                }
            }
        }

        if(pivot == null)
        {
            pivot = new GameObject("Pivot");
            pivot.transform.position = transform.position;
            pivot.transform.SetParent(transform);
            Debug.LogError("Pivot Point was Missing, position it now!");
            return;
        }

        GameObject p = new GameObject("Paralax");
        p.transform.SetParent(transform);

        GameObject l1 = new GameObject("Layer1");
        l1.transform.position = pivot.transform.position;
        l1.transform.SetParent(p.transform);

        GameObject l2 = new GameObject("Layer2");
        l2.transform.position = pivot.transform.position;
        l2.transform.SetParent(p.transform);

        GameObject g1 = new GameObject("L1");
        g1.transform.position = transform.position;
        g1.transform.localScale = transform.localScale;
        g1.transform.rotation = transform.rotation;
        g1.AddComponent<PolygonCollider2D>();
        g1.layer = LayerMask.NameToLayer("Ignore Raycast");
        g1.transform.SetParent(l1.transform);

        GameObject g2 = new GameObject("L2");
        g2.transform.position = transform.position;
        g2.transform.localScale = transform.localScale;
        g2.transform.rotation = transform.rotation;
        g2.AddComponent<PolygonCollider2D>();
        g2.layer = LayerMask.NameToLayer("Ignore Raycast");
        g2.transform.SetParent(l2.transform);

        if (UnityEditorInternal.ComponentUtility.CopyComponent(pc2))
        {
            UnityEditorInternal.ComponentUtility.PasteComponentValues(g1.GetComponent<PolygonCollider2D>());
            UnityEditorInternal.ComponentUtility.PasteComponentValues(g2.GetComponent<PolygonCollider2D>());
        }
        else
        {
            Debug.LogError("Cannot Update Paralax, missing the Polygon Collider?");
            return;
        }

        g1.AddComponent<MeshFilter>();
        g1.AddComponent<MeshRenderer>().material = paralaxMaterial1;
        g1.transform.position = new Vector3(g1.transform.position.x, g1.transform.position.y, 2);
        g1.GetComponent<PolygonCollider2D>().isTrigger = true;
        g2.AddComponent<MeshFilter>();
        g2.AddComponent<MeshRenderer>().material = paralaxMaterial2;
        g2.transform.position = new Vector3(g2.transform.position.x, g2.transform.position.y, 4);
        g2.GetComponent<PolygonCollider2D>().isTrigger = true;

        meshFromCol(g1);
        meshFromCol(g2);

        g1.GetComponent<PolygonCollider2D>().enabled = false;
        g2.GetComponent<PolygonCollider2D>().enabled = false;

        if (inwards)
        {
            l1.transform.localScale = new Vector3(0.95f, 0.95f, 1f);
            l2.transform.localScale = new Vector3(0.9f, 0.9f, 1f);
        }
        else
        {
            l1.transform.localScale = new Vector3(1.15f, 1.15f, 1f);
            l2.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
        }
    }

    private void meshFromCol(GameObject g)
    {
        int pointCount = 0;
        PolygonCollider2D pc2 = g.GetComponent<PolygonCollider2D>();
        pointCount = pc2.GetTotalPointCount();

        MeshFilter mf = g.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        Vector2[] points = pc2.points;
        Vector3[] vertices = new Vector3[pointCount];
        for (int j = 0; j < pointCount; j++)
        {
            Vector2 actual = points[j];
            vertices[j] = new Vector3(actual.x, actual.y, 0);
        }
        Triangulator tr = new Triangulator(points);
        int[] triangles = tr.Triangulate();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mf.mesh = mesh;
    }

    void Update()
    {
        if(!Application.isPlaying)meshFromCol(gameObject);

    }
#endif
}
