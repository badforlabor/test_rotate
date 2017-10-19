﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour {

    public Transform Pivot;

	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
    Vector3 ClickPosition;
    int MouseButton = -1;
    Vector3 LastPosition;
	void Update () {
        if (Input.GetMouseButton(0))
        {
            if (MouseButton == -1)
            {
                MouseButton = 0;
                ClickPosition = Input.mousePosition;
                LastPosition = this.transform.position;
            }
        }
        else if(Input.GetMouseButton(1))
        {
            if (MouseButton == -1)
            {
                Debug.logger.Log(1);
                MouseButton = 2;
                ClickPosition = Input.mousePosition;
                LastPosition = this.transform.position;

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitinfo;
                if (Physics.Raycast(ray, out hitinfo, 100000, 0xFFFFF))
                {
                    Pivot.transform.position = hitinfo.point;
                }
            }
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            MouseButton = -1;
        }
        if (MouseButton == 0) {
            Vector3 xaxis = this.transform.right;
            Vector3 yaxis = Vector3.Cross(this.transform.right, Vector3.up);
            Vector3 delta = Input.mousePosition - ClickPosition;
            // Debug.logger.Log(""+delta +"," + Input.mousePosition + "," +ClickPosition);
            this.transform.position = LastPosition + (delta.x * xaxis + delta.y * yaxis) * 0.01f;
        }
        else if (MouseButton == 2) 
        {
            // 右键旋转
            var angle = 1;
            Vector3 delta = Input.mousePosition - ClickPosition;
            ClickPosition = Input.mousePosition;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                this.transform.RotateAround(Pivot.transform.position, Vector3.up, angle * (delta.x > 0 ? 1 : -1));
            }
            else if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x))
            {
                // Vector3 yaxis = Vector3.Cross(this.transform.right, Vector3.up);
                this.transform.RotateAround(Pivot.transform.position, this.transform.right, angle * (delta.y > 0 ? 1 : -1));
            }
        }
        else if (Input.mouseScrollDelta.magnitude != 0)
        {
            this.transform.position += (this.transform.forward * Input.mouseScrollDelta.x + this.transform.forward * Input.mouseScrollDelta.y) * 0.5f;
        }
	}    
}
