using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class TouchController : MonoBehaviour {

	public Image outer_circle;
	public Image inner_circle;

	private float maxMoveDist = 0f;

	// Use this for initialization
	void Start () {
	
		float outerRadius = outer_circle.GetComponent<CircleCollider2D> ().radius * outer_circle.transform.lossyScale.x;
		//float innterRadius = inner_circle.GetComponent<CircleCollider2D> ().radius;

		maxMoveDist = outerRadius;

		outer_circle.enabled = false;
		inner_circle.enabled = false;

	}



	void OnEnable() {
		TouchManager.Instance.Drag += OnSwipe;
		TouchManager.Instance.TouchStart += OnTouchStart;
		TouchManager.Instance.TouchEnd += OnTouchEnd;
	}


	void OnDisable() {
		if (TouchManager.Instance) {
			TouchManager.Instance.Drag -= OnSwipe;
			TouchManager.Instance.TouchStart -= OnTouchStart;
			TouchManager.Instance.TouchEnd -= OnTouchEnd;
		}
	}

	void OnTouchStart(object sender, CustomInputEventArgs e) {
		outer_circle.enabled = true;
		inner_circle.enabled = true;

		outer_circle.transform.position = e.Input.ScreenPosition;
		inner_circle.transform.position = e.Input.ScreenPosition;
	}

	void OnTouchEnd(object sender, CustomInputEventArgs e) {
		outer_circle.enabled = false;
		inner_circle.enabled = false;
	}

	void OnSwipe (object sender, CustomInputEventArgs e) {

		Vector3 touchPos = e.Input.ScreenPosition;
		Vector3 dir = (touchPos - outer_circle.transform.position).normalized;
		float dist = Vector3.Distance(touchPos, outer_circle.transform.position);
		dist = (dist <= maxMoveDist) ? dist : maxMoveDist;

		inner_circle.transform.position = outer_circle.transform.position + dir * dist;

	}

}
