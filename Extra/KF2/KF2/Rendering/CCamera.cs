using System;
using OpenTK;

namespace KF2.Rendering {
    public class CCamera {
        //Location
        private Vector3 vPosition;
        private Vector3 vDirection;
        private Vector2 vRotation;

        //Control
        private Matrix4 mView;
        private Matrix4 mPerspective;
        private Matrix4 mOrtho;

        private Vector3 vUp;
        private Vector4 vScreen;
       
        //
        // Constructor
        //
        public CCamera(Vector4 screen, Vector3 up) {
            vPosition = new Vector3(0.0f, 0.0f, 0.0f);
            vDirection = new Vector3(0.0f, 0.0f, 0.0f);
            vRotation = new Vector2(0.0f, 0.0f);

            vScreen = screen;
            vUp = up;
        }

        //
        // Project
        //
        public void Project(float fov, float zNear, float zFar) {
            mView = Matrix4.LookAt(vPosition, vPosition + vDirection, vUp);
            mPerspective = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (fov / 180.0f),
            vScreen.Z / vScreen.W,
            zNear,
            zFar);
            mOrtho = Matrix4.CreateOrthographic(vScreen.Z, vScreen.W, zNear, zFar);
        }

        //
        // Get Matrices
        //
        public Matrix4 GetView() {
            return mView;
        }
        public Matrix4 GetPerspective() {
            return mPerspective;
        }
        public Matrix4 GetOrtho() {
            return mOrtho;
        }

        //
        // Get Vectors
        //
        public Vector3 GetFrom() {
            return vPosition;
        }
        public Vector3 GetTo() {
            return vDirection;
        }

        //
        // Set Vectors
        //
        public void SetPosition(Vector3 position) {
            vPosition = position;
        }
        public void SetDirection(Vector3 dir) {
            vDirection = dir;
        }
        public void SetScreen(Vector4 screen) {
            vScreen = screen;
        }

        //
        // Basic Functionality
        //
        public void Move(float speed) {
            vPosition += (vDirection * speed);
        }

        public void Rotate(float yaw, float pitch) {
            float degtorad = (float)(Math.PI / 180.0f);

            vRotation.X += yaw * degtorad;
            vRotation.Y += pitch * degtorad;

            vDirection = new Vector3(
                -(float)Math.Cos(vRotation.X),
                (float)Math.Sin(vRotation.Y),
                (float)Math.Sin(vRotation.X)
                );
        }
    }
}