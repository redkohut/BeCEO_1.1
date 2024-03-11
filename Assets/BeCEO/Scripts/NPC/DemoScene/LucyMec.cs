using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;


namespace BeCEO.NPC
{
    public class LucyMec : MonoBehaviour
    {
        public int love = 100;
        private Rigidbody2D rigdbody;
        private float speedMovement = 10f;

        private bool isStartingForwardMovemnt;
        // Start is called before the first frame update
        void Start()
        {
            rigdbody = GetComponent<Rigidbody2D>(); 
        }

        // Update is called once per frame
        void FixedUpdate()
        {

        }

        public void MoveFromHome()
        {
            Debug.Log("Love now:" + love.ToString());
            love -= 50;
            Debug.Log("Love after operation :" + love.ToString());
        }
        /***
         * מעזו, ןנמ 
         * 
         * ***/
    }

}
