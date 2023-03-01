using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public struct LightVert
{
    public LightVert(Vector2 pos, float angle)
    {
        this.pos = pos;
        this.angle = angle;
    }
    public Vector2 pos { get; set; }
    public float angle { get; set; }
}

[ExecuteInEditMode]

public class LightSource : MonoBehaviour {

    public Material lightMaterial;
    public float rangeX, rangeY;
    public LayerMask layerMask;
    public bool staticLight = false;

    [HideInInspector]public Vector2[] vertices, dynamicVerts ,allVertices;
    [HideInInspector]public LightVert[] lightVerts;

    private int dynamicVertCount;
    private List<PolygonCollider2D> dynamicCols;
    private bool calculatedOnce = false;

    Mesh lightMesh;

    void Start()
    {
        getAllMeshes();
        transform.GetComponent<Renderer>().enabled = true;
    }

    void getAllMeshes() {
        MeshFilter meshFilter = transform.GetComponent<MeshFilter>();//(MeshFilter)gameObject.AddComponent(typeof(MeshFilter));
        MeshRenderer renderer = transform.GetComponent<MeshRenderer>();//gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;			
        renderer.sharedMaterial = lightMaterial;
        lightMesh = new Mesh();
        meshFilter.mesh = lightMesh;
        lightMesh.name = "Light Mesh";
        lightMesh.MarkDynamic();

        //Get Vertices
        Collider2D[] allCols = Physics2D.OverlapAreaAll(new Vector2(-100000, -100000), new Vector2(100000, 100000), layerMask);

        PolygonCollider2D[] cols;

        PolygonCollider2D p = new PolygonCollider2D();

        dynamicCols = new List<PolygonCollider2D>();

        int colCounter = 0;

        for(int i = 0; i < allCols.Length; i++)
        {
            if (p.GetType().IsInstanceOfType(allCols[i]))colCounter++;
        }
        cols = new PolygonCollider2D[colCounter];
        colCounter = 0;
        for (int i = 0; i < allCols.Length; i++)
        {
            if (p.GetType().IsInstanceOfType(allCols[i]))
            {
                cols[colCounter] = (PolygonCollider2D)allCols[i];
                colCounter++;
            }
        }

        dynamicVertCount = 0;
        int vertCount = 0;

        for (int i = 0; i < cols.Length; i++)
        {
            if (cols[i].tag.Contains("DynamicPos"))
            {
                if (!staticLight)
                {
                    dynamicVertCount += cols[i].points.Length;
                    dynamicCols.Add(cols[i]);
                }
            }
            else
            {
                vertCount += cols[i].points.Length;
            }
        }

        allVertices = new Vector2[vertCount];
        dynamicVerts = new Vector2[dynamicVertCount];

        vertCount = 0;
        dynamicVertCount = 0;

        for (int i = 0; i < cols.Length; i++)
        {
            for(int j = 0; j < cols[i].points.Length; j++)
            {
                if (cols[i].tag.Contains("DynamicPos"))
                {
                    if (!staticLight)
                    {
                        dynamicVerts[dynamicVertCount] = cols[i].transform.TransformPoint(cols[i].points[j]);
                        dynamicVertCount++;
                    }
                }
                else
                {
                    allVertices[vertCount] = cols[i].transform.TransformPoint(cols[i].points[j]);
                    vertCount++;
                }
            }
        }
    }

    private void getDynamicVerts()
    {
        int count = 0;
        for(int i = 0; i < dynamicCols.Count; i++)
        {
            for (int j = 0; j < dynamicCols[i].points.Length; j++)
            {
                dynamicVerts[count] = dynamicCols[i].transform.TransformPoint(dynamicCols[i].points[j]);
                count++;
            }
        }
    }
	
	void Update () {
        if (staticLight && calculatedOnce) return;
        //getAllMeshes();
        getDynamicVerts();
        int vertCount = 0;

        for(int i = 0; i < allVertices.Length; i++)
        {
            if (allVertices[i].x > transform.position.x - rangeX && allVertices[i].x < transform.position.x + rangeX && allVertices[i].y > transform.position.y - rangeY && allVertices[i].y < transform.position.y + rangeY)
            {
                vertCount++;
            }
        }

        for(int i = 0; i < dynamicVerts.Length; i++)
        {
            if (dynamicVerts[i].x > transform.position.x - rangeX && dynamicVerts[i].x < transform.position.x + rangeX && dynamicVerts[i].y > transform.position.y - rangeY && dynamicVerts[i].y < transform.position.y + rangeY)
            {
                vertCount++;
            }
        }

        vertices = new Vector2[vertCount * 2 + 4];
        vertices[0] = new Vector2(-rangeX, -rangeY) + (Vector2)transform.position;
        vertices[1] = new Vector2(rangeX, -rangeY) + (Vector2)transform.position;
        vertices[2] = new Vector2(-rangeX, rangeY) + (Vector2)transform.position;
        vertices[3] = new Vector2(rangeX, rangeY) + (Vector2)transform.position;


        vertCount = 4;

        for (int i = 0; i < allVertices.Length; i++)
        {
            if (allVertices[i].x > transform.position.x - rangeX && allVertices[i].x < transform.position.x + rangeX && allVertices[i].y > transform.position.y - rangeY && allVertices[i].y < transform.position.y + rangeY)
            {
                vertices[vertCount] = allVertices[i];
                vertCount++;
            }
        }

        for (int i = 0; i < dynamicVerts.Length; i++)
        {
            if (dynamicVerts[i].x > transform.position.x - rangeX && dynamicVerts[i].x < transform.position.x + rangeX && dynamicVerts[i].y > transform.position.y - rangeY && dynamicVerts[i].y < transform.position.y + rangeY)
            {
                vertices[vertCount] = dynamicVerts[i];
                vertCount++;
            }
        }

        lightVerts = new LightVert[vertices.Length * 2];
        int lightVertCount = 0;
        
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i] == null) continue;
            //Debug.DrawLine(transform.position, vertices[i], Color.blue);
            Vector2 dir = vertices[i] - (Vector2)transform.position;

            //LEFT SIDE
            if (dir.y > 0) dir.x -= 0.05f; // rotate slightly
            else dir.x += 0.05f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 100, layerMask);
            if (hit)
            {
                lightVerts[lightVertCount] = new LightVert(hit.point, getVectorAngle(true, dir.x, dir.y));
                lightVertCount++;
                Debug.DrawLine(transform.position, hit.point, Color.white);
            }
            else Debug.DrawLine(transform.position, (Vector2)transform.position + (vertices[i] - (Vector2)transform.position) * 2, Color.red);

            //RIGHT SIDE
            if (dir.y > 0) dir.x += 0.1f;
            else dir.x -= 0.1f;
            hit = Physics2D.Raycast(transform.position, dir, 100, layerMask);
            if (hit)
            {
                lightVerts[lightVertCount] = new LightVert(hit.point, getVectorAngle(true, dir.x, dir.y));
                lightVertCount++;
                Debug.DrawLine(transform.position, hit.point, Color.white);
            }
            else Debug.DrawLine(transform.position, (Vector2)transform.position + (vertices[i] - (Vector2)transform.position) * 2, Color.red);

        }

        lightVerts = sortArray(lightVerts);
        for(int i = 0; i < lightVerts.Length - 1; i++)
        {
            Debug.DrawLine(lightVerts[i].pos, lightVerts[i + 1].pos, Color.cyan);
        }
        /*for(int i = 0; i < lightVerts.Length; i++)
        {
            if (lightVerts[i].pos != null) Debug.Log(lightVerts[i].angle);
        }*/

        renderLightMesh();
        calculatedOnce = true;
    }

    void renderLightMesh()
    {
        //interface_touch.vertexCount = allVertices.Count; // notify to UI

        Vector3[] initVerticesMeshLight = new Vector3[lightVerts.Length + 1];

        initVerticesMeshLight[0] = Vector3.zero;


        for (int i = 0; i < lightVerts.Length; i++)
        {
            //Debug.Log(allVertices[i].angle);
            initVerticesMeshLight[i + 1] = lightVerts[i].pos - (Vector2)transform.position;

            //if(allVertices[i].endpoint == true)
            //Debug.Log(allVertices[i].angle);

        }

        lightMesh.Clear();
        lightMesh.vertices = initVerticesMeshLight;

        Vector2[] uvs = new Vector2[initVerticesMeshLight.Length];
        for (int i = 0; i < initVerticesMeshLight.Length; i++)
        {
            uvs[i] = new Vector2(initVerticesMeshLight[i].x, initVerticesMeshLight[i].y);
        }
        lightMesh.uv = uvs;

        // triangles
        int idx = 0;
        int[] triangles = new int[(lightVerts.Length * 3)];
        for (int i = 0; i < (lightVerts.Length * 3); i += 3)
        {

            triangles[i] = 0;
            triangles[i + 1] = idx + 1;


            if (i == (lightVerts.Length * 3) - 3)
            {
                //-- if is the last vertex (one loop)
                triangles[i + 2] = 1;
            }
            else
            {
                triangles[i + 2] = idx + 2; //next next vertex	
            }

            idx++;
        }


        lightMesh.triangles = triangles;
        //lightMesh.RecalculateNormals();
        GetComponent<Renderer>().sharedMaterial = lightMaterial;
    }

    LightVert[] sortArray(LightVert[] array)
    {
        return array.OrderBy(item => item.angle).ToArray();
    }

    /*void sortList(List<LightVert> list)
    {
        list.Sort((item1, item2) => (item2.angle.CompareTo(item1.angle)));
    }*/

    float getVectorAngle(bool pseudo, float x, float y)
    {
        float ang = 0;
        if (pseudo == true)
        {
            ang = pseudoAngle(x, y);
        }
        else
        {
            ang = Mathf.Atan2(y, x);
        }
        return ang;
    }

    float pseudoAngle(float dx, float dy)
    {
        // Hight performance for calculate angle on a vector (only for sort)
        // APROXIMATE VALUES -- NOT EXACT!! //
        float ax = Mathf.Abs(dx);
        float ay = Mathf.Abs(dy);
        float p = dy / (ax + ay);
        if (dx < 0)
        {
            p = 2 - p;

        }
        return p;
    }
}
