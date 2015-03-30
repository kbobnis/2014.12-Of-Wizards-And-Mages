using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Shield {

	public float SetupCost;
	public float SustainCost;
	public float SustainTime;
	public float ShieldHeight;

	public Shield(float setupCost, float sustainCost, float sustainTime, float shieldHeight) {

		SetupCost = setupCost;
		SustainCost = sustainCost;
		SustainTime = sustainTime;
		ShieldHeight = shieldHeight;
	}
}
