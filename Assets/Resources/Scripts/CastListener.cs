using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public interface CastListener {

	void CastIt(Mage Caster, Spell Spell, Vector2 from, Vector2 direction);
}
