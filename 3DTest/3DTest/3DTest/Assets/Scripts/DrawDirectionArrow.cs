using UnityEngine;

public class DrawDirectionArrow : MonoBehaviour
{
	public float arrowLength = 1.0f;
	public Color arrowColor = Color.red;

	void OnDrawGizmos()
	{
		Gizmos.color = arrowColor;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * arrowLength);

		Vector3 arrowTip = transform.position + transform.forward * arrowLength;
		Gizmos.DrawLine(arrowTip, arrowTip - (transform.forward + transform.right).normalized * 0.2f * arrowLength);
		Gizmos.DrawLine(arrowTip, arrowTip - (transform.forward - transform.right).normalized * 0.2f * arrowLength);
	}
}
