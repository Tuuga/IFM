using UnityEngine;
using System.Collections;

public interface IUsableInventory {
	void Use(Pointable p);
}

public interface IUsableWorld {
	void Use();
}
