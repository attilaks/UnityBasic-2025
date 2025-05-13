using System;
using UnityEngine;

namespace SaveSystem
{
	[Serializable]
	public struct SerializableVector3
	{
		public float x;
		public float y;
		public float z;

		public static explicit operator Vector3(SerializableVector3 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		public static implicit operator SerializableVector3(Vector3 v)
		{
			return new SerializableVector3 { x = v.x, y = v.y, z = v.z };
		}

		public override string ToString()
		{
			return $"(x: {x}, y: {y}, z: {z})";
		}
	}
}