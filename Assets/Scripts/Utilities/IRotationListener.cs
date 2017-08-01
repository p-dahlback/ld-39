using UnityEngine;
using System.Collections;

public interface IRotationListener
{
	void RotationChangeFinished(Quaternion newRotation);
}

