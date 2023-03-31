﻿namespace MeshIO.FBX
{
	//P : ["PropName", "PropType", "Label(?)", "Flags", __values__, …]
	public class FbxProperty : Property
	{
		//Model porperties
		public const string LclTranslation = "Lcl Translation";
		public const string LclScaling = "Lcl Scaling";
		public const string LclRotation = "Lcl Rotation";

		//Material Properties
		public const string AmbientColor = "AmbientColor";

		public string FbxTypeName { get; set; }

		public string TypeLabel { get; set; }

		public PropertyFlags Flags { get; set; }

		public FbxProperty(string name, Element3D owner) : base(name, owner) { }

		public FbxProperty(string name, Element3D owner, object value) : base(name, owner, value) { }

		public FbxProperty(string name, Element3D owner, object value, string typeName, string typeLabel, PropertyFlags flags) : base(name, owner, value)
		{
			this.FbxTypeName = typeName;
			this.TypeLabel = typeLabel;
			this.Flags = flags;
		}
	}
}
