using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace JumpArena {

    public enum Action_e {
        Idle,
        Move,
        Jump,
        Land
    }

    public class Player : MonoBehaviour {
        private static readonly float SPEED = 10f;
        private static readonly float JUMP_FORCE = 20f;
        private static readonly float DOWN_FORCE = 10f;
        private static readonly float MAX_JUMP_HEIGHT = 6f;
        public bool canJump { get; set; } = true;
        private Vector3 _direction;
        private Rigidbody _rigidbody;
        private bool _jump = false;
        private float _baseJumpHeight;

        private void Awake() {
            this._rigidbody = this.GetComponent<Rigidbody>();
        }

        private void Start() {
            this._direction = Vector3.zero;
        }

        void Update() {
            this._direction.x = Input.GetAxis("Horizontal");
            this._direction.z = Input.GetAxis("Vertical");
            if (Input.GetAxis("Jump") >= 1.0f && this.canJump == true) {
                this._baseJumpHeight = this.transform.position.y;
                this.canJump = false;
                this._jump = true;
            }
        }

        private void FixedUpdate() {
            Vector3 force = this._direction * SPEED;
            if (this._jump == true) {
                this._rigidbody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.VelocityChange);
                this._jump = false;
            }
            if (this._rigidbody.position.y - this._baseJumpHeight > MAX_JUMP_HEIGHT) {
                this._rigidbody.AddForce(Vector3.down * DOWN_FORCE, ForceMode.VelocityChange);
            }
            this._rigidbody.MovePosition(this._rigidbody.position + force * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter(Collision other) {
            this.canJump = true;
        }
    }
}