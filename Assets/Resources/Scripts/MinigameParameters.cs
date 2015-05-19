using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class MinigameParameters {
	
	private BonusParameters BonusParameters;

	public MinigameParameters(BonusParameters bonusParameters) {
		BonusParameters = bonusParameters;
	}

	public void Update() {
		BonusParameters.Update();
	}
}
