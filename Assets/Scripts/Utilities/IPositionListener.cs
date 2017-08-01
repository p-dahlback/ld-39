using UnityEngine;
using System.Collections;

public interface IPositionListener 
{
	void PositionChangeFinished(Vector3 newPosition);
}

