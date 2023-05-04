﻿using CSMath;
using System;

namespace MeshIO
{
	public class Transform
	{
		public XYZ Translation
		{
			get { return this._translation; }
			set
			{
				this._translation = value;
				this.updateMatrix();
			}
		}

		public XYZ Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				if (value.X == 0 || value.Y == 0 || value.Z == 0)
					throw new ArgumentException("Scale value cannot be 0");

				this._scale = value;
				this.updateMatrix();
			}
		}

		public XYZ EulerRotation
		{
			get { return this._rotation; }
			set
			{
				this._rotation = value;
				this.updateMatrix();
			}
		}

		public Quaternion Quaternion
		{
			get
			{
				return Quaternion.CreateFromYawPitchRoll(MathUtils.ToRadian(this._rotation));
			}
		}

		private XYZ _translation = XYZ.Zero;
		private XYZ _scale = new XYZ(1, 1, 1);
		private XYZ _rotation = XYZ.Zero;

		private Matrix4 _matrix;

		public Transform()
		{
			this._matrix = Matrix4.Identity;
			this.Translation = XYZ.Zero;
			this.EulerRotation = XYZ.Zero;
			this.Scale = new XYZ(1, 1, 1);
			this._matrix.m33 = 1;
		}

		public Transform(XYZ translation, XYZ scale, XYZ rotation) : this()
		{
			this.Translation = translation;
			this.Scale = scale;
			this.EulerRotation = rotation;
		}

		public Transform(Matrix4 matrix)
		{
			this._matrix = matrix;
		}

		public XYZ ApplyTransform(XYZ xyz)
		{
			return this._matrix * xyz;
		}

		public bool TryDecompose(out XYZ translation, out XYZ scaling, out Quaternion rotation)
		{
			Matrix4 matrix = this._matrix;

			translation = new XYZ();
			scaling = new XYZ();
			rotation = new Quaternion();
			var XYZDouble = new XYZ();

			if (matrix.m33 == 0.0)
				return false;

			Matrix4 matrix4_3 = matrix;
			matrix4_3.m03 = 0.0;
			matrix4_3.m13 = 0.0;
			matrix4_3.m23 = 0.0;
			matrix4_3.m33 = 1.0;

			if (matrix4_3.GetDeterminant() == 0.0)
				return false;

			if (matrix.m03 != 0.0 || matrix.m13 != 0.0 || matrix.m23 != 0.0)
			{
				if (!Matrix4.Inverse(matrix, out Matrix4 inverse))
				{
					return false;
				}

				matrix.m03 = matrix.m13 = matrix.m23 = 0.0;
				matrix.m33 = 1.0;
			}

			translation.X = matrix.m30;
			matrix.m30 = 0.0;
			translation.Y = matrix.m31;
			matrix.m31 = 0.0;
			translation.Z = matrix.m32;
			matrix.m32 = 0.0;

			XYZ[] cols = new XYZ[3]
			{
			  new XYZ(matrix.m00, matrix.m01, matrix.m02),
			  new XYZ(matrix.m10, matrix.m11, matrix.m12),
			  new XYZ(matrix.m20, matrix.m21, matrix.m22)
			};

			scaling.X = cols[0].GetLength();
			cols[0] = cols[0].Normalize();
			XYZDouble.X = cols[0].Dot(cols[1]);
			cols[1] = cols[1] * 1 + cols[0] * -XYZDouble.X;

			scaling.Y = cols[1].GetLength();
			cols[1] = cols[1].Normalize();
			XYZDouble.Y = cols[0].Dot(cols[2]);
			cols[2] = cols[2] * 1 + cols[0] * -XYZDouble.Y;

			XYZDouble.Z = cols[1].Dot(cols[2]);
			cols[2] = cols[2] * 1 + cols[1] * -XYZDouble.Z;
			scaling.Z = cols[2].GetLength();
			cols[2] = cols[2].Normalize();

			XYZ rhs = XYZ.Cross(cols[1], cols[2]);
			if (cols[0].Dot(rhs) < 0.0)
			{
				for (int index = 0; index < 3; ++index)
				{
					scaling.X *= -1.0;
					cols[index].X *= -1.0;
					cols[index].Y *= -1.0;
					cols[index].Z *= -1.0;
				}
			}

			double trace = cols[0].X + cols[1].Y + cols[2].Z + 1.0;
			double qx;
			double qy;
			double qz;
			double qw;

			if (trace > 0)
			{
				double s = 0.5 / Math.Sqrt(trace);
				qx = (cols[2].Y - cols[1].Z) * s;
				qy = (cols[0].Z - cols[2].X) * s;
				qz = (cols[1].X - cols[0].Y) * s;
				qw = 0.25 / s;
			}
			else if (cols[0].X > cols[1].Y && cols[0].X > cols[2].Z)
			{
				double s = Math.Sqrt(1.0 + cols[0].X - cols[1].Y - cols[2].Z) * 2.0;
				qx = 0.25 * s;
				qy = (cols[0].Y + cols[1].X) / s;
				qz = (cols[0].Z + cols[2].X) / s;
				qw = (cols[2].Y - cols[1].Z) / s;
			}
			else if (cols[1].Y > cols[2].Z)
			{
				double s = Math.Sqrt(1.0 + cols[1].Y - cols[0].X - cols[2].Z) * 2.0;
				qx = (cols[0].Y + cols[1].X) / s;
				qy = 0.25 * s;
				qz = (cols[1].Z + cols[2].Y) / s;
				qw = (cols[0].Z - cols[2].X) / s;
			}
			else
			{
				double s = Math.Sqrt(1.0 + cols[2].Z - cols[0].X - cols[1].Y) * 2.0;
				qx = (cols[0].Z + cols[2].X) / s;
				qy = (cols[1].Z + cols[2].Y) / s;
				qz = 0.25 * s;
				qw = (cols[1].X - cols[0].Y) / s;
			}

			rotation.X = -qx;
			rotation.Y = -qy;
			rotation.Z = -qz;
			rotation.W = qw;

			return true;
		}

		private void updateMatrix()
		{
			Matrix4 translationMatrix = Matrix4.CreateTranslation(this._translation);
			Matrix4 rotationMatrix = Matrix4.CreateFromQuaternion(this.Quaternion);
			Matrix4 scaleMatrix = Matrix4.CreateScale(this._scale);

			this._matrix = translationMatrix * rotationMatrix * scaleMatrix;
		}
	}
}
