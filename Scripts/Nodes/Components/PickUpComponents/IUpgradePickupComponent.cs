using Godot;
using System;

public interface IUpgradePickupComponent<[MustBeVariant] T> where T : struct, Enum
{
	T Upgrade { get; }

	void HandlePlayerTouched(CharacterController characterController);
}
