using OpenTK;

namespace KF2.Rendering {
    public struct Transform {
        Vector3    Position;
        Quaternion Rotation;
        Vector3    Scale;

        public Transform(Vector3 pos, Vector3 rot, Vector3 scale) {
            Position = pos;
            Rotation = Quaternion.FromEulerAngles(rot);
            Scale    = scale;
        }

        public Vector3    localPosition {
            get { return Position; }
            set { Position = value; }
        }
        public Quaternion localRotation {
            get { return Rotation; }
            set { Rotation = value; }
        }
        public Vector3    localEulerRotation {
            set { Rotation = Quaternion.FromEulerAngles(value); }
        }
        public Vector3    localScale {
            get { return Scale; }
            set { Scale = value; }
        }
        public Matrix4    localTransform {
            get {
                Matrix4 T = Matrix4.CreateTranslation(Position);
                Matrix4 R = Matrix4.CreateFromQuaternion(Rotation);
                Matrix4 S = Matrix4.CreateScale(Scale);

                return S * R * T;
            }
        }
    }
}
