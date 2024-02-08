using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FoV : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public float soundDetectionRadius;

    private Mesh viewMesh;
    public GameObject player;

    private Player_Movement playerMovement;

    public enum PlayerDetectionState
    {
        NotDetected,
        PartiallyDetected,
        FullyDetected
    }

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        GetComponent<MeshFilter>().mesh = viewMesh;
        //get PlayerMovement script from player
        playerMovement = player.GetComponent<Player_Movement>();


    }

    private void LateUpdate()
    {


        DrawFieldOfView();
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * 100);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = -viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        Vector2[] uvs = new Vector2[vertexCount]; // Add this line
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        uvs[0] = new Vector2(0.5f, 0); // Add this line
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            uvs[i + 1] = new Vector2(0.5f, Mathf.Clamp01(vertices[i + 1].magnitude / viewRadius)); // Add this line

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.uv = uvs; // Add this line
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }



    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, false); // Changed this line
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);

        if (hit.collider != null)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }


    public PlayerDetectionState IsPlayerInFieldOfView()
    {
        Vector3 dirToPlayer = player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.up, dirToPlayer);
        float distanceToPlayer = dirToPlayer.magnitude;

        if (angleToPlayer < viewAngle / 2f && distanceToPlayer < viewRadius)
        {
            // Check for full detection
            if (!Physics2D.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstacleMask))
            {
                return PlayerDetectionState.FullyDetected;
            }
        }

        // Check for partial detection
        if (!playerMovement.isCrouching && playerMovement.isMoving && Vector3.Distance(transform.position, player.transform.position) < soundDetectionRadius)
        {
            return PlayerDetectionState.PartiallyDetected;
        }

        return PlayerDetectionState.NotDetected;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, soundDetectionRadius);
    }

}
