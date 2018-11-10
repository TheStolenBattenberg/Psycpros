using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Psycpros.Psycode {
    class ICamera {
        private Matrix4 mProjection;
        private Matrix4 mLookAt;

        private Vector3 vPosition;
        private Vector3 vDirection;
        private Vector3 vUp;

        private Vector4 vScreen;

        public ICamera(Vector4 ScreenCoordinates, Vector3 Up) {
            mProjection = new Matrix4();
            mLookAt = new Matrix4();
            vPosition  = new Vector3();
            vDirection = new Vector3();

            vUp = Up;
            vScreen = ScreenCoordinates;
        }

        public void SetProjectionPerspective(float aspect, float fov, float zNear, float zFar) {
            Matrix4.CreatePerspectiveFieldOfView((float)Math.PI * (fov / 180.0f), aspect, zNear, zFar, out mProjection);
        }

        public void SetProjectionOrtro(float zNear, float zFar) {
            Matrix4.CreateOrthographicOffCenter(vScreen.X, vScreen.Y, vScreen.Z, vScreen.W, zNear, zFar, out mProjection);
        }

        public void Project() {
            //Create LookAt Matrix
            mLookAt = Matrix4.LookAt(vPosition, vDirection, vUp);

            //Set Projection
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref mProjection);

            //Set LookAt
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref mLookAt);
        }

        public void Move(Vector3 Dir, float Speed) {
            vPosition += (Dir * Speed);
        }

        public void Position(Vector3 Pos) {
            vPosition = Pos;
        }

        public void SetRotation(float yaw, float pitch, float roll) {
            vDirection = new Vector3(yaw, pitch, roll);
        }

        /**
         * Get Matrices
        **/
        public Matrix4 GetProjectionMatrix() {
            return mProjection;
        }

        public Matrix4 GetViewMatrix() {
            return mLookAt;
        }
    }
}
