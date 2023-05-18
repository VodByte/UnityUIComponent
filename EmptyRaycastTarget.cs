using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VodByte.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class EmptyRaycastTarget : MaskableGraphic
    {
        protected EmptyRaycastTarget() => useLegacyMeshGeneration = false;
        protected override void OnPopulateMesh(VertexHelper toFill) => toFill.Clear();
    }
}