using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class BonusConfig {
	
	public Bonus Bonus;
	public float Weight;

	public BonusConfig(Bonus bonus, float weight) {
		Bonus = bonus;
		Weight = weight;
	}
}
