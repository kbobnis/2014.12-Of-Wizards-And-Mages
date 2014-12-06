using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface MageControlListener {

	void SwapSpell(Side s);
	void Drop();
	void Cast(Vector3 side);
	void WantCast();
	void DontWantCast();
}
