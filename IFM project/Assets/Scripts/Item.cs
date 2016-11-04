using UnityEngine;
using System.Collections;

public interface IUsable {
	void Use();
}

public class Item : MonoBehaviour {
	public string itemName;
	public string lookAtText;


}
