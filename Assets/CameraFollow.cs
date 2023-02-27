// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraWork.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Used in PUN Basics Tutorial to deal with the Camera work to follow the player
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    
    public class CameraWork : MonoBehaviour
    {
        public float FollowSpeed = 2f;
        public Transform target;

        void Update()
        {
            Vector3 newPos = new Vector3(target.position.x,target.position.y,10f);
            transform.position = Vector3.Slerp(transform.position,newPos,FollowSpeed*Time.deltaTime);
        }    
    }
}