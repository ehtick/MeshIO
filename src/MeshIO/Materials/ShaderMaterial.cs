namespace MeshIO.Materials;

/// <summary>
/// Represents a material that uses a custom shader for rendering effects.
/// </summary>
public class ShaderMaterial : Material
{
	public ShaderMaterial() : this(string.Empty)
	{
	}

	public ShaderMaterial(string name) : base(name)
	{
	}
}
