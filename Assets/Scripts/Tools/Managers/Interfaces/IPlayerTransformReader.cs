﻿using SaveSystem;

namespace Tools.Managers.Interfaces
{
	public interface IPlayerTransformReader
	{
		SerializableVector3 PlayerPosition { get; }
		SerializableVector3 PlayerRotation { get; }
	}
}