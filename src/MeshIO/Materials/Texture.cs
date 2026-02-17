namespace MeshIO.Materials;

public class Texture : Element3D
{
	public TextureFilterType MagnificationFilter { get; set; }

	public TextureFilterType MinificationFilter { get; set; }

	public TextureFilterType MipFilter { get; set; }

	public WrapMode WrapModeS { get; set; }

	public WrapMode WrapModeT { get; set; }

	public Texture() : base()
	{
	}

	public Texture(string name) : base(name)
	{
	}
}