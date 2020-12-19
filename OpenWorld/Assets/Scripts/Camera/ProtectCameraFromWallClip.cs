using System;
using System.Collections;
using UnityEngine;

public class ProtectCameraFromWallClip : MonoBehaviour
{
    public float clipMoveTime = 0.05f;
    public float returnTime = 0.4f;
    public float sphereCastRadius = 0.1f;
    public bool visualiseInEditor;
    public float closestDistance = 0.5f;
    public bool protecting { get; private set; }
    public string dontClipTag = "Player";

    private Transform m_Cam;
    private Transform m_Pivot;
    private float m_OriginalDist;
    private float m_MoveVelocity;
    private float m_CurrentDist;
    private Ray m_Ray = new Ray();
    private RaycastHit[] m_Hits = new RaycastHit[30];
    private Collider[] cols = new Collider[30];

    private void Start()
    {
        m_Cam = GetComponentInChildren<Camera>().transform;
        m_Pivot = m_Cam.parent;
        m_OriginalDist = m_Cam.localPosition.magnitude;
        m_CurrentDist = m_OriginalDist;
    }


    private void LateUpdate()
    {
        float targetDist = m_OriginalDist;

        m_Ray.origin = m_Pivot.position + m_Pivot.forward * sphereCastRadius;
        m_Ray.direction = -m_Pivot.forward;

        int m_RayCount = 0;

        m_RayCount = Physics.OverlapSphereNonAlloc(m_Ray.origin, sphereCastRadius, cols);

        bool initialIntersect = false;
        bool hitSomething = false;

        for (int i = 0; i < m_RayCount; i++)
        {
            if ((!cols[i].isTrigger) &&
                !(cols[i].attachedRigidbody != null && cols[i].attachedRigidbody.CompareTag(dontClipTag)))
            {
                initialIntersect = true;
                break;
            }
        }

        int m_HitCount = 0;

        if (initialIntersect)
        {
            m_Ray.origin += m_Pivot.forward * sphereCastRadius;
            m_HitCount = Physics.RaycastNonAlloc(m_Ray, m_Hits, m_OriginalDist - sphereCastRadius);
        }
        else
        {
            m_HitCount = Physics.SphereCastNonAlloc(m_Ray, sphereCastRadius, m_Hits, m_OriginalDist + sphereCastRadius);
        }
        RayHitSort.Instance.Sort(m_Hits,0, m_HitCount);

        float nearest = Mathf.Infinity;

        for (int i = 0; i < m_HitCount; i++)
        {
            if (m_Hits[i].distance < nearest && (!m_Hits[i].collider.isTrigger) &&
                !(m_Hits[i].collider.attachedRigidbody != null &&
                  m_Hits[i].collider.attachedRigidbody.CompareTag(dontClipTag)))
            {
                nearest = m_Hits[i].distance;
                targetDist = -m_Pivot.InverseTransformPoint(m_Hits[i].point).z;
                hitSomething = true;
            }
        }

        if (hitSomething)
        {
            Debug.DrawRay(m_Ray.origin, -m_Pivot.forward * (targetDist + sphereCastRadius), Color.red);
        }

        protecting = hitSomething;
        m_CurrentDist = Mathf.SmoothDamp(m_CurrentDist, targetDist, ref m_MoveVelocity,
                                       m_CurrentDist > targetDist ? clipMoveTime : returnTime);
        m_CurrentDist = Mathf.Clamp(m_CurrentDist, closestDistance, m_OriginalDist);
        m_Cam.localPosition = -Vector3.forward * m_CurrentDist;
    }

    public class RayHitSort : Sorter<RaycastHit>
    {
        public static readonly RayHitSort Instance = new RayHitSort();

        public void Sort(RaycastHit[] data, int start, int end)
        {
            QuickSort(data, start, end);
        }

        protected override int Compare(RaycastHit x, RaycastHit y)
        {
            return x.distance.CompareTo(y.distance);
        }
    }
}

