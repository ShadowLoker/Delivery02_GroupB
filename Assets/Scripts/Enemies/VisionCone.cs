using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VisionCone : MonoBehaviour
{
    public float Radius = 5f;
    [Range(1, 360)] public float Angle = 45f;

    public GameObject PlayerRef;
    public LayerMask TargetMask;
    public LayerMask ObstructionMask;
    public bool CanSeePlayer { get; private set; }

    void Start()
    {
        PlayerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheck());
    }
    private IEnumerator FOVCheck()
    {
        WaitForSeconds Wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return Wait;
            FieldOFViewCheck();
        }
    }
    private void FieldOFViewCheck()
    {
        Collider2D[] RangeCheck = Physics2D.OverlapCircleAll(transform.position, Radius, TargetMask); // just checks if the layer is targetmask (not a wall nor an obstacle of any kind)

        if (RangeCheck.Length > 0)
        {
            Transform Target = RangeCheck[0].transform; //the first object that finds
            Vector2 DirectionToTarget = (Target.position - transform.position).normalized;

            if(Vector2.Angle(transform.up, DirectionToTarget) < Angle/2) 
            {
                float DistanceToTarget = Vector2.Distance(transform.position, Target.position);
                if(!Physics2D.Raycast(transform.position, DirectionToTarget, DistanceToTarget, ObstructionMask))
                    CanSeePlayer = true;
                else
                    CanSeePlayer = false;
            }
            else
                CanSeePlayer = false;
        }
        else if(CanSeePlayer)
            CanSeePlayer = false; //in range previously but not in range anymore so set to false; 
    }
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, Radius);
        Vector3 Angle01 = DirectionFromAngle(-transform.eulerAngles.z, -Angle / 2);
        Vector3 Angle02 = DirectionFromAngle(-transform.eulerAngles.z, Angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position+Angle01 * Radius);
        Gizmos.DrawLine(transform.position, transform.position + Angle02 * Radius);

        if(CanSeePlayer) 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, PlayerRef.transform.position);
        }

    }

    private Vector2 DirectionFromAngle(float EulerY, float AngleInDegrees)
    {
        AngleInDegrees += EulerY;
        return new Vector2(Mathf.Sin(AngleInDegrees*Mathf.Deg2Rad), Mathf.Cos(AngleInDegrees*Mathf.Deg2Rad));
    }
}
