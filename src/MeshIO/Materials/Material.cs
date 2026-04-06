namespace MeshIO.Materials;

/// <summary>
/// Represents the base class for 3D materials that define the appearance of surfaces in a scene, including color and
/// lighting properties.
/// </summary>
/// <remarks>The Material class provides common properties for ambient, diffuse, and emissive lighting components.
/// Derived classes can extend this class to implement specific material behaviors or additional properties. Materials
/// are typically assigned to 3D objects to control how they interact with light sources in the rendering
/// environment.</remarks>
public abstract class Material : Element3D
{
	public Color AmbientColor { get; set; }

	public double AmbientFactor { get; set; }

	public Color DiffuseColor { get; set; }

	public double DiffuseFactor { get; set; }

	public Color EmissiveColor { get; set; }

	public double EmissiveFactor { get; set; }

	public Material() : this(string.Empty)
	{
	}

	public Material(string name) : base(name)
	{
	}
}