using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class PlayerController {

	public abstract void PrepareFight(Mage mage, Ai ai, PanelMage myPanel, PanelMage enemy);
}
