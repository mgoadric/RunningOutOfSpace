using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eOrientationMode { NODE = 0, TANGENT }

[AddComponentMenu("Splines/Spline Controller")]
[RequireComponent(typeof(SplineInterpolator))]
public class SplineController : MonoBehaviour
{
    public GameObject SplineRoot;
    public GameObject BaseMover;
    private GameObject[] movers;
	public float Duration = 10;
	public eOrientationMode OrientationMode = eOrientationMode.NODE;
	public eWrapMode WrapMode = eWrapMode.ONCE;
	public bool AutoStart = true;
	public bool AutoClose = true;
	public bool HideOnExecute = true;

	Transform[] mTransforms;

	void OnDrawGizmos()
	{
		Transform[] trans = GetTransforms();
		if (trans.Length < 2)
			return;

		SplineInterpolator interp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;
		SetupSplineInterpolator(interp, trans, 0);
		interp.StartInterpolation(null, false, WrapMode);


		Vector3 prevPos = trans[0].position;
		for (int c = 1; c <= 100; c++)
		{
			float currTime = c * Duration / 100;
			Vector3 currPos = interp.GetHermiteAtTime(currTime);
			float mag = (currPos-prevPos).magnitude * 2;
			Gizmos.color = new Color(mag, 0, 0, 1);
			Gizmos.DrawLine(prevPos, currPos);
			prevPos = currPos;
		}
	}


	void Start()
	{
        //		mSplineInterp = GetComponent(typeof(SplineInterpolator)) as SplineInterpolator;

        mTransforms = GetTransforms();

		if (HideOnExecute)
			DisableTransforms();

		if (AutoStart)
			FollowSpline();
	}

    void SetupMovers(Transform[] trans) {
        movers = new GameObject[trans.Length];
        for (int m = 0; m < movers.Length; m++)
        {
            movers[m] = (GameObject)Instantiate(BaseMover);
            SplineInterpolator interp = movers[m].AddComponent<SplineInterpolator>();
            SetupSplineInterpolator(interp, trans, m);
        }
    }

    void SetupSplineInterpolator(SplineInterpolator interp, Transform[] trans, int indexToStart)
    {

        interp.Reset();

        float step = (AutoClose) ? Duration / trans.Length :
            Duration / (trans.Length - 1);

        for (int c = 0; c < trans.Length; c++)
        {
            int which = (c + indexToStart) % trans.Length;
            if (OrientationMode == eOrientationMode.NODE)
            {
                interp.AddPoint(trans[which].position, trans[which].rotation, step * c, new Vector2(0, 1));
            }

            // WILL NOT USE THIS, CAN LEAVE BROKEN
            else if (OrientationMode == eOrientationMode.TANGENT)
            {
                Quaternion rot;
                if (c != trans.Length - 1)
                    rot = Quaternion.LookRotation(trans[c + 1].position - trans[c].position, trans[c].up);
                else if (AutoClose)
                    rot = Quaternion.LookRotation(trans[0].position - trans[c].position, trans[c].up);
                else
                    rot = trans[c].rotation;

                interp.AddPoint(trans[c].position, rot, step * c, new Vector2(0, 1));
            }
        }

        if (AutoClose)
            interp.SetAutoCloseMode(step * trans.Length);
        
    }


	/// <summary>
	/// Returns children transforms, sorted by name.
	/// </summary>
	Transform[] GetTransforms()
	{
		if (SplineRoot != null)
		{
			List<Component> components = new List<Component>(SplineRoot.GetComponentsInChildren(typeof(Transform)));
			List<Transform> transforms = components.ConvertAll(c => (Transform)c);

			transforms.Remove(SplineRoot.transform);
			transforms.Sort(delegate(Transform a, Transform b)
			{
				return a.name.CompareTo(b.name);
			});

			return transforms.ToArray();
		}

		return null;
	}

	/// <summary>
	/// Disables the spline objects, we don't need them outside design-time.
	/// </summary>
	void DisableTransforms()
	{
		if (SplineRoot != null)
		{
			SplineRoot.SetActive(false);
		}
	}


	/// <summary>
	/// Starts the interpolation
	/// </summary>
	void FollowSpline()
	{
		if (mTransforms.Length > 0)
		{
			SetupMovers(mTransforms);
            foreach (GameObject m in movers)
            {
                SplineInterpolator mSplineInterp = m.GetComponent<SplineInterpolator>();
                mSplineInterp.StartInterpolation(null, true, WrapMode);
            }
		}
	}
}