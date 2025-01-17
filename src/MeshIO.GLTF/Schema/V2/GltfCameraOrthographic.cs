//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeshIO.GLTF.Schema.V2 {
    using System.Linq;
    using System.Runtime.Serialization;
    
    
    public class GltfCameraOrthographic {
        
        /// <summary>
        /// Backing field for Xmag.
        /// </summary>
        private float _xmag;
        
        /// <summary>
        /// Backing field for Ymag.
        /// </summary>
        private float _ymag;
        
        /// <summary>
        /// Backing field for Zfar.
        /// </summary>
        private float _zfar;
        
        /// <summary>
        /// Backing field for Znear.
        /// </summary>
        private float _znear;
        
        /// <summary>
        /// Backing field for Extensions.
        /// </summary>
        private System.Collections.Generic.Dictionary<string, object> _extensions;
        
        /// <summary>
        /// Backing field for Extras.
        /// </summary>
        private GltfExtras _extras;
        
        /// <summary>
        /// The floating-point horizontal magnification of the view. Must not be zero.
        /// </summary>
        [Newtonsoft.Json.JsonRequiredAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("xmag")]
        public float Xmag {
            get {
                return this._xmag;
            }
            set {
                this._xmag = value;
            }
        }
        
        /// <summary>
        /// The floating-point vertical magnification of the view. Must not be zero.
        /// </summary>
        [Newtonsoft.Json.JsonRequiredAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("ymag")]
        public float Ymag {
            get {
                return this._ymag;
            }
            set {
                this._ymag = value;
            }
        }
        
        /// <summary>
        /// The floating-point distance to the far clipping plane. `zfar` must be greater than `znear`.
        /// </summary>
        [Newtonsoft.Json.JsonRequiredAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("zfar")]
        public float Zfar {
            get {
                return this._zfar;
            }
            set {
                if ((value <= 0D)) {
                    throw new System.ArgumentOutOfRangeException("Zfar", value, "Expected value to be greater than 0");
                }
                this._zfar = value;
            }
        }
        
        /// <summary>
        /// The floating-point distance to the near clipping plane.
        /// </summary>
        [Newtonsoft.Json.JsonRequiredAttribute()]
        [Newtonsoft.Json.JsonPropertyAttribute("znear")]
        public float Znear {
            get {
                return this._znear;
            }
            set {
                if ((value < 0D)) {
                    throw new System.ArgumentOutOfRangeException("Znear", value, "Expected value to be greater than or equal to 0");
                }
                this._znear = value;
            }
        }
        
        /// <summary>
        /// Dictionary object with extension-specific objects.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("extensions")]
        public System.Collections.Generic.Dictionary<string, object> Extensions {
            get {
                return this._extensions;
            }
            set {
                this._extensions = value;
            }
        }
        
        /// <summary>
        /// Application-specific data.
        /// </summary>
        [Newtonsoft.Json.JsonPropertyAttribute("extras")]
        public GltfExtras Extras {
            get {
                return this._extras;
            }
            set {
                this._extras = value;
            }
        }
        
        public bool ShouldSerializeExtensions() {
            return ((_extensions == null) 
                        == false);
        }
        
        public bool ShouldSerializeExtras() {
            return ((_extras == null) 
                        == false);
        }
    }
}
