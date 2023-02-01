using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfVew : MonoBehaviour
{

	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public bool teveo;

	public LayerMask targetMask;
	public LayerMask obstacleMask;
	

	[SerializeField]
	private EnemyMove enemymove;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);

		enemymove = GetComponent<EnemyMove>();
	}
    private void Update()
    {
        if (teveo == true)
        {
			enemymove.playerfollow();
        }
       
		
		else
        {
			enemymove.noFollow();
		}
		
	}


    IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		
		}
	}

	void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

		teveo = false;
		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				teveo = true;


				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					visibleTargets.Add(target);
					


                }



			}
			



		}
		
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
			
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}