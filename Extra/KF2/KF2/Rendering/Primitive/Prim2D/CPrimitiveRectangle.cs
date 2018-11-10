using OpenTK;

namespace KF2.Rendering.Primitive {
    public class CPrimitiveRectangle : CPrimitive {
        private Vector2 Size;
        private Vector4 Colour;

        public CPrimitiveRectangle(float w, float h, Vector4 Colour) {
            Size = new Vector2(w, h);
            this.Colour = Colour;

            this.Build();
        }

        protected override void Build() {
            pVertices = new Vertex[] {
                new Vertex(new Vector3(-Size.X, -Size.Y, -1.0f), new Vector3(0.0f, 0.0f, 0.0f), Colour),
                new Vertex(new Vector3( Size.X, -Size.Y, -1.0f), new Vector3(1.0f, 0.0f, 0.0f), Colour),
                new Vertex(new Vector3(-Size.X,  Size.Y, -1.0f), new Vector3(0.0f, 1.0f, 0.0f), Colour),
                new Vertex(new Vector3( Size.X,  Size.Y, -1.0f), new Vector3(1.0f, 1.0f, 0.0f), Colour)
            };

            pIndices = new ushort[] {
                0, 1, 2,
                2, 1, 3
            };

            base.Build();
        }
    }
}
