﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Player {

	public Mage Mage;
	private List<Spell> SpellList;

	public Player(Mage mage, List<Spell> spellList) {
		Mage = mage;
		SpellList = spellList;
	}
}