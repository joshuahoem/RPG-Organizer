using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour
{
    [SerializeField] float minZoomSize;
    [SerializeField] float maxZoomSize;
    [SerializeField] [Range(0.01f,1f)] float zoomScalePercentage;

    [SerializeField] RectTransform imageToZoom;
    [SerializeField] RectTransform contentSize;


    private void Update() 
    {
        CheckFingerScroll();
        CheckScrollWheel();
        // ChangePivotPosition();
    }

    private void CheckScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                ZoomIn();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                ZoomOut();
            }
        }
    }

    private void CheckFingerScroll()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * zoomScalePercentage * 0.01f);

            Debug.Log("Josh difference " + difference);

        }
    }

    private void zoom(float increment)
    {
        if (imageToZoom.localScale.x + increment < minZoomSize || 
            imageToZoom.localScale.x + increment > maxZoomSize) 
            {
                Debug.Log("Josh said its too big or too small " + (imageToZoom.localScale.x + increment));
                return;
            }

        float newX = imageToZoom.localScale.x + increment;
        float newY = imageToZoom.localScale.y + increment;
        imageToZoom.localScale = new Vector2 (newX, newY);

        float newWidth = imageToZoom.GetComponent<RectTransform>().sizeDelta.x * newX;
        float newHeight = imageToZoom.GetComponent<RectTransform>().sizeDelta.y * newY;
        contentSize.sizeDelta = new Vector2(newWidth, newHeight);
        // Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, minZoomSize, maxZoomSize);

        Debug.Log("Josh camera " + Camera.main.orthographicSize);
    }

    private void ChangePivotPosition()
    {
        float xPos = (contentSize.position.x) / contentSize.sizeDelta.x;
        float yPos = (contentSize.position.y) / contentSize.sizeDelta.y;
        // Debug.Log(xPos + "," + yPos);
        imageToZoom.pivot = new Vector2(xPos, yPos);
    }

    public void ZoomIn()
    {
        float zoomScaleAmount = zoomScalePercentage * imageToZoom.localScale.x;

        if (imageToZoom.localScale.x + zoomScaleAmount > maxZoomSize) {return;}

        float newX = imageToZoom.localScale.x + zoomScaleAmount;
        float newY = imageToZoom.localScale.y + zoomScaleAmount;
        imageToZoom.localScale = new Vector2 (newX, newY);

        float newWidth = imageToZoom.GetComponent<RectTransform>().sizeDelta.x * newX;
        float newHeight = imageToZoom.GetComponent<RectTransform>().sizeDelta.y * newY;
        contentSize.sizeDelta = new Vector2(newWidth, newHeight);
        
    }

    public void ZoomOut()
    {
        float zoomScaleAmount = zoomScalePercentage * imageToZoom.localScale.x;

        if (imageToZoom.localScale.x - zoomScaleAmount < minZoomSize) {return;}

        float newX = imageToZoom.localScale.x - zoomScaleAmount;
        float newY = imageToZoom.localScale.y - zoomScaleAmount;
        imageToZoom.localScale = new Vector2 (newX, newY);

        float newWidth = imageToZoom.GetComponent<RectTransform>().sizeDelta.x * newX;
        float newHeight = imageToZoom.GetComponent<RectTransform>().sizeDelta.y * newY;
        contentSize.sizeDelta = new Vector2(newWidth, newHeight);

    }
}
