using ClassesEnum;

namespace EnumUtils {
	[System.Serializable]
    public enum ClasseEnum {
		[TipoPlayer.ClasseInfo("Todos", null, TipoAtaque.CORPO_A_CORPO, 0, 0, 0, 0, 0, 0, 0, 0, 0)]
		TODOS,
		//Equipe
		[TipoPlayer.ClasseInfo("Nortun", "Soldier", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8)]
        GUERREIRO,
		[TipoPlayer.ClasseInfo("Pandmé", "Archer", TipoAtaque.ATAQUE_A_DISTANCIA, 12, 11, 15, 10, 2, 5, 0, 40, 12)]
        ARQUEIRO,
		[TipoPlayer.ClasseInfo("Kapirsky", "Mage", TipoAtaque.MAGICO, 10, 9, 13, 15, 2, 5, 0, 30, 20)]
        MAGO,

		//Chefes
		[TipoPlayer.ClasseInfo("Troll Chief", "TrollChief", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, true, 30)]
		TROLLCHIEF,
		[TipoPlayer.ClasseInfo("Capitão Pirata", "PirateCapitain", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, true, 30)]
		PIRATACAPITAO,
		[TipoPlayer.ClasseInfo("Grifo", "Griffin", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, true, 30)]
		GRIFO,
		[TipoPlayer.ClasseInfo("Dragão Rabo de Martelo", "DragonHammertail", TipoAtaque.ATAQUE_A_DISTANCIA, 12, 11, 15, 10, 2, 5, 0, 40, 12, true, true, 30)]
		DRAGAO,
		[TipoPlayer.ClasseInfo("Tinho Demon", "Demon", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, true, 30)]
		TINHODEMON,

		//Corpo a Corpo
		[TipoPlayer.ClasseInfo("Pirata", "PirateSwashbuckler", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		PIRATA,
		[TipoPlayer.ClasseInfo("Behemoth do Mar", "BehemothSea", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		BEHEMOTH,
		[TipoPlayer.ClasseInfo("Meta-Sapo Azul", "FrogkinBlue", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		SAPOAZUL,
		[TipoPlayer.ClasseInfo("Goblin", "GoblinGrunt", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		GOBLIN,
		[TipoPlayer.ClasseInfo("Succubus", "Succubus", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		SUCCUBUS,
		[TipoPlayer.ClasseInfo("Troll", "Troll", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		TROLL,
		[TipoPlayer.ClasseInfo("Wyrm", "Wyrm", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		WYRM,
		[TipoPlayer.ClasseInfo("Wyrm Marrom", "WyrmBrown", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		WYRMMARROM,
		[TipoPlayer.ClasseInfo("Zumbi", "Zombie", TipoAtaque.CORPO_A_CORPO, 15, 14, 12, 8, 1, 1, 0, 50, 8, true, false, 10)]
		ZUMBI,
		//Ataque a Distancia
		[TipoPlayer.ClasseInfo("Arranha", "Spider", TipoAtaque.ATAQUE_A_DISTANCIA, 12, 11, 15, 10, 2, 5, 0, 40, 12, true, false, 10)]
		ARANHA,
		[TipoPlayer.ClasseInfo("Meta-Sapa Amarelo", "FrogkinYellow", TipoAtaque.ATAQUE_A_DISTANCIA, 12, 11, 15, 10, 2, 5, 0, 40, 12, true, false, 10)]
		SAPOAMARELO,
		[TipoPlayer.ClasseInfo("Harpia Escrava", "Harpy", TipoAtaque.ATAQUE_A_DISTANCIA, 12, 11, 15, 10, 2, 5, 0, 40, 12, true, false, 10)]
		HARPIA,

		//Mágico		
		[TipoPlayer.ClasseInfo("Dríade", "Dryad", TipoAtaque.MAGICO, 10, 9, 13, 15, 2, 5, 0, 30, 20, true, false, 10)]
		DRIADE,
		[TipoPlayer.ClasseInfo("Goblin Xamã", "GoblinShaman", TipoAtaque.MAGICO, 10, 9, 13, 15, 2, 5, 0, 30, 20, true, false, 10)]
		GOBLINXAMA,
		[TipoPlayer.ClasseInfo("Liche", "Liche", TipoAtaque.MAGICO, 10, 9, 13, 15, 2, 5, 0, 30, 20, true, false, 10)]
		LICHE,
		[TipoPlayer.ClasseInfo("Diabólico", "Fiend", TipoAtaque.CORPO_A_CORPO, 10, 9, 13, 15, 2, 5, 0, 30, 20, true, false, 10)]
		DIABOLICO
	}
}