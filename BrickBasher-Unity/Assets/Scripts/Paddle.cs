/**** 
 * Created by: Bob Baloney
 * Date Created: April 20, 2022
 * 
 * Last Edited by: 
 * Last Edited:
 * 
 * Description: Paddle controler on Horizontal Axis
****/

/*** Using Namespaces ***/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour
{
    public float speed = 10; //speed of paddle


    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal")!=0)
        {
            Vector3 pos = transform.position; //current position
            Vector3 temp = pos; //second reference to current position
            pos.x = pos.x + Input.GetAxis("Horizontal") * speed*Time.deltaTime; //change the position based on if the horizontal axis is changing
            if(pos.x<(-12.5f) || pos.x > 12.5f) //if the paddle is to go past the walls, don't let it
            {
                pos = temp;
            }
            transform.position = pos; //update the position of the paddle
        }
    }//end Update()

}
