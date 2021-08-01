﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeshIO.FBX.Exceptions
{

	[Serializable]
	public class FbxConverterException : Exception
	{
		public FbxConverterException() { }
		public FbxConverterException(string message) : base(message) { }
		public FbxConverterException(string message, Exception inner) : base(message, inner) { }
		protected FbxConverterException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
