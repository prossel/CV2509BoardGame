using UnityEngine;

/// <summary>
/// Remappe automatiquement les UV d'un cube vers un atlas 4x3 (layout "T" / croix horizontal).
/// Voir la croix horizontale ici https://docs.unity3d.com/2017.4/Documentation/Manual/class-Cubemap.html
/// Corrige l'orientation/miroir des faces.
/// </summary>
[RequireComponent(typeof(MeshFilter))]
//[ExecuteAlways]
public class CubeAtlasMapper : MonoBehaviour
{
    public int columns = 4;
    public int rows = 3;

    public Vector2Int Left   = new Vector2Int(0, 1);
    public Vector2Int Front  = new Vector2Int(1, 1);
    public Vector2Int Right  = new Vector2Int(2, 1);
    public Vector2Int Back   = new Vector2Int(3, 1);
    public Vector2Int Top    = new Vector2Int(1, 2);
    public Vector2Int Bottom = new Vector2Int(1, 0);

    void Start()
    {
        RemapUVs();
    }

    public void RemapUVs()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null) { Debug.LogError("Pas de MeshFilter trouvé."); return; }

        Mesh mesh = mf.mesh;
        // Mesh mesh = mf.sharedMesh; // shared mesh will modify the original asset (all cubes using it will be affected)
        Vector3[] verts = mesh.vertices;
        Vector3[] norms = mesh.normals;
        Vector2[] newUVs = new Vector2[verts.Length];

        float cellU = 1f / (float)columns;
        float cellV = 1f / (float)rows;

        Bounds b = mesh.bounds;
        Vector3 bMin = b.min;
        Vector3 bSize = b.size;

        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 n = norms[i];
            Vector3 v = verts[i];

            Vector2 localUV = Vector2.zero;
            Vector2Int cell = Front;

            // Faces X (Left / Right)
            if (Mathf.Abs(n.x) > 0.5f)
            {
                cell = (n.x > 0f) ? Right : Left;
                float u = (v.z - bMin.z) / bSize.z;
                float vv = (v.y - bMin.y) / bSize.y;

                // Correction d’orientation
                if (n.x < 0f) u = 1f - u; // Right → inverser U

                localUV = new Vector2(u, vv);
            }
            // Faces Y (Top / Bottom)
            else if (Mathf.Abs(n.y) > 0.5f)
            {
                cell = (n.y > 0f) ? Top : Bottom;
                float u = (v.x - bMin.x) / bSize.x;
                float vv = (v.z - bMin.z) / bSize.z;

                // Correction d’orientation
                if (n.y < 0f) vv = 1f - vv; // Top → inverser V

                localUV = new Vector2(u, vv);
            }
            // Faces Z (Front / Back)
            else
            {
                cell = (n.z < 0f) ? Front : Back;
                float u = (v.x - bMin.x) / bSize.x;
                float vv = (v.y - bMin.y) / bSize.y;

                // Correction d’orientation
                if (n.z > 0f) u = 1f - u; // Front → inverser U

                localUV = new Vector2(u, vv);
            }

            // Placement dans l’atlas
            float mappedU = (cell.x * cellU) + (localUV.x * cellU);
            float mappedV = (cell.y * cellV) + (localUV.y * cellV);
            newUVs[i] = new Vector2(mappedU, mappedV);
        }

        mesh.uv = newUVs;
        mesh.RecalculateBounds();
        Debug.Log("UVs remappées et corrigées.");
    }
}
