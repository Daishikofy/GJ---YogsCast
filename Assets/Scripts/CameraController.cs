﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CameraController : MonoBehaviour
    {

        public float distanceToPlayer;

        private GameObject player;
        [SerializeField]
        private Vector3 offset;
        private Vector3 previousPos;
        void Start()
        {
            var aux = FindObjectOfType<Player>();
            if (aux == null) return;
            player = aux.gameObject;
            this.transform.position += player.transform.position + offset;
            previousPos = player.transform.position;
        }

        void LateUpdate()
        {
            if (player == null) return;
            if (distancePlayerCamera())
                transform.position += (player.transform.position - previousPos);
            previousPos = player.transform.position;

        }

        bool distancePlayerCamera()
        {
            float distX = Mathf.Abs(transform.position.x - player.transform.position.x);
            float distY = Mathf.Abs(transform.position.y - player.transform.position.y);
            if (distX > distanceToPlayer || distY > distanceToPlayer)
                return true;
            return false;
        }
    }

