using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{

    private float[] parallaxScales;
    private Vector3 previousCameraPosition;

    public Transform[] backgrounds;
    public float smoothing;

	void Start ()
    {
        previousCameraPosition = Camera.main.transform.position;

        parallaxScales = new float[backgrounds.Length];
        var increase = -20;
        var start = increase;
        for(var i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = start;
            start += increase;
        }
	}
	
	void LateUpdate ()
    {
	    for(var i = 0; i <backgrounds.Length; i++)
        {
            var parallax = (previousCameraPosition.x - Camera.main.transform.position.x) * parallaxScales[i];
            var targetPositionX = backgrounds[i].position.x + parallax;

            var target = new Vector3(targetPositionX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, target, smoothing * Time.deltaTime);
        }

        previousCameraPosition = Camera.main.transform.position;
	}
}
