using OpenTK;

namespace KF2.Rendering.Primitive {
    public class CPrimitivePlaneXZ : CPrimitive {
        private Vector3 Scale;
        private Vector4 Colour;

        public CPrimitivePlaneXZ(float sx, float sy, float sz, Vector4 Colour) {
            this.Colour = Colour;
            Scale = new Vector3(sx, sy, sz);

            this.Build();
        }

        protected override void Build() {
            pVertices = new Vertex[] {
                new Vertex(new Vector3(-0.5f * Scale.X, 0.0f * Scale.Y, -0.5f * Scale.Z), 
                    new Vector3(0.0f, 0.0f, 0.0f), Colour),
                new Vertex(new Vector3( 0.5f * Scale.X, 0.0f * Scale.Y, -0.5f * Scale.Z), 
                    new Vector3(1.0f, 0.0f, 0.0f), Colour),
                new Vertex(new Vector3(-0.5f * Scale.X, 0.0f * Scale.Y,  0.5f * Scale.Z), 
                    new Vector3(0.0f, 1.0f, 0.0f), Colour),
                new Vertex(new Vector3( 0.5f * Scale.X, 0.0f * Scale.Y,  0.5f * Scale.Z), 
                    new Vector3(1.0f, 1.0f, 0.0f), Colour),

            };

            pIndices = new ushort[] {
                2, 1, 0,
                1, 2, 3
            };

            base.Build();
        }
    }
}