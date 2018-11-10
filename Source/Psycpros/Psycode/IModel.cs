using System.Collections.Generic;

namespace Psycpros.Psycode {
    class IModel {
        private List<ISimpleMesh> pMeshes;

        public IModel(List<ISimpleMesh> pMeshList) {
            pMeshes = pMeshList;
        }

        public void Render() {
            foreach(ISimpleMesh mesh in pMeshes) {
                mesh.Render();
            }
        }
    }
}
