using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour {
    static public ProjectileLine S;

    [Header("Set In Inspector")]
    public float minimumDistance = 0.1f;

    private LineRenderer line;
    private GameObject _pointOfInterest;
    private List<Vector3> _points;

    void Awake()
    {
        S = this;

        line = GetComponent<LineRenderer>();

        line.enabled = false;

        _points = new List<Vector3>();
    }

    public GameObject pointOfInterest
    {
        get
        {
            return _pointOfInterest;
        }
        set
        {
            _pointOfInterest = value;
            if(_pointOfInterest != null)
            {
                line.enabled = false;
                _points = new List<Vector3>();
                AddPoint();
            }
        }
    }

    
    public void Clear()
    {
        _pointOfInterest = null;
        line.enabled = false;
        _points = new List<Vector3>();
    }

    public void AddPoint()
    {
        Vector3 point = _pointOfInterest.transform.position;
        if(_points.Count > 0 && (point - lastPoint).magnitude < minimumDistance)
        {
            return;
        }

        if(_points.Count == 0)
        {
            Vector3 launchPositionDifference = point - Slingshot.LAUNCH_POSITION;
            _points.Add(point + launchPositionDifference);
            _points.Add(point);
            line.positionCount = 2;

            line.SetPosition(0, _points[0]);
            line.SetPosition(1, _points[1]);
            line.enabled = true;
        } else
        {
            _points.Add(point);
            line.positionCount = _points.Count;
            line.SetPosition(_points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public  Vector3 lastPoint
    {
        get
        {
            if(_points == null)
            {
                return Vector3.zero;
            }
            return _points[_points.Count - 1];
        }
    }

    void FixedUpdate()
    {
        if (pointOfInterest == null)
        {
            if (FollowCamera.PointOfInterest != null)
            {
                if (FollowCamera.PointOfInterest.tag == "Projectile")
                {
                    pointOfInterest = FollowCamera.PointOfInterest;
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        AddPoint();
        if(FollowCamera.PointOfInterest == null)
        {
            pointOfInterest = null;
        }
    }
}
