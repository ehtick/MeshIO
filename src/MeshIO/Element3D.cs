﻿namespace MeshIO
{
	/// <summary>
	/// Base class for all the elements contained in the 3D environment
	/// </summary>
	public abstract class Element3D
	{
		/// <summary>
		/// Unique id to identify this element
		/// </summary>
		public ulong? Id { get; internal set; } = null;

		/// <summary>
		/// Name of the element
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Properties of this element
		/// </summary>
		public PropertyCollection Properties { get; }

		/// <summary>
		/// Default constructor
		/// </summary>
		public Element3D() : this(string.Empty) { }

		public Element3D(string name)
		{
			this.Name = name;
			this.Id = IdUtils.CreateId();
			this.Properties = new PropertyCollection(this);
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"{this.GetType().Name}:{this.Name}";
		}
	}
}
