using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class Conversation {
	private string tela;
	public SpeakDesign speakDesign { get; private set; }

	public Conversation(ConversationName tela) {
		this.tela = tela.ToString();
		speakDesign = getSpeakDesign();
	}
	SpeakDesign getSpeakDesign() {

		SpeakDesign speakDesign = GetType().GetProperty(tela).GetValue(this, null) as SpeakDesign;

		return speakDesign;
	}
	Sprite getSprite(string name) {
		return Resources.Load<Sprite>("SpritesSpeaks/Background/" + name);
	}

	public SpeakDesign FlorestaMundo {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("GameMaster", 157, 340);
			Sprite background = getSprite("Forest");
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Jorves", "right", "Olá Senhor Programador, por favor desperte!"));
			speaks.Add(new Speak("Programador", "left", "Huuuum... \nSó mais cinco minutos, manhêêê..."));
			speaks.Add(new Speak("Jorves", "right", "Desculpe senhor, sou uma inteligência artificial, logo é impossível que eu seja sua manhêêê."));
			speaks.Add(new Speak("Programador", "left", "Mas que mer... hã?! \n Quem é você?!\n Pera aí, que lugar é esse?!\n Parece uma floresta!"));
			speaks.Add(new Speak("Jorves", "right", "Acalme-se, pelo visto o senhor não se lembra de nada.\n Eu sou Jorves, a inteligência artificial criada pelo senhor e os outros programadores para ser o Game Master do Jogo de Realidade Virtual Begin Reality."));
			speaks.Add(new Speak("Programador", "left", " Jorves?!\n Ah, tá... É você, maquina inútil!\n Mas mudando de pau pra cavaco, como eu vim parar aqui?!"));
			speaks.Add(new Speak("Programador", "left", " Enfim, devo ter adormecido, sei lá, isso que dá ficar sem tomar café por mais de dez minutos.\n Tô indo, tenho que “grindar” no Tree of Savior...\n Pera aí, ô maquina inútil o que você fez com o botão de logoff dessa bagaça?!"));
            speaks.Add(new Speak("Jorves", "right", " Era isso que eu estava tentando informa-lo.\n O senhor não adormeceu, na verdade, estava testando o jogo no momento em que ocorreu uma invasão em nossos servidores e, devido a invasão, o senhor ficou desacordado por algum tempo.\n Neste momento os crackers estão roubando informações do sistema, e bloquearam as rotas de entrada e saída do jogo e o acesso externo ao sistema, portanto o senhor está preso aqui.\n A única forma de escapar é indo até os terminais físicos de acesso direto ao sistema que são localizados no final de cada capitulo do jogo."));
			speaks.Add(new Speak("Programador", "left", " Você está de sacanagem com minha cara né?!\n Sempre sobra para que eu corrija as merdas desses programadores, e olha que eu sou Junior.\n Bora lá, me teletransporte para os terminais."));
			speaks.Add(new Speak("Jorves", "right", " Desculpe-me senhor, não posso utilizar essa função, estou usando todo meu processamento para impedir os crackers de roubar as informações e deletar o jogo.\n O senhor terá que ir por si mesmo até os terminais, que se encontram após os mestres de cada capitulo.\n Portanto vou lhe fornecer três ajudantes que aparecerão quando você entrar em batalha, para que sob seu comando possam derrotar os mestres de cada capitulo.\n A programação de batalha deles foi apagada, portanto o senhor precisará programa - los durante as batalhas!\n Caso tenha alguma dúvida senhor, você poderá acessar a tela de ajuda que está acessível de qualquer lugar do jogo clicando no botão como símbolo de interrogação(“?”), lá estarão as respostas para suas dúvidas.\nVá rápido senhor, por que se o jogo for apagado, seu cérebro sofrerá danos.\n Boa Sorte!"));
            speaks.Add(new Speak("Programador", "left", "Tô falando... Maquina inútil, bela ajuda que você me deu.\n Seria engraçado se não fosse trágico, sempre quis um jogo de realidade virtual e agora posso me ferrar por ter conseguido.\n É melhor eu ir...\n Mas ei, espera, mas que roupa de “Playboy” é essa que eu tô usando? Pareço ator de novela de época!\n Esse jogo tá bem diferente do projeto, não sei pra que Brainstorming... \n Ahh como eu queria um cafezinho..."));
			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign ManutencaoTime {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("GameMaster", 157, 340);
			Sprite background = null;
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Programador", "left", "Essa é a tela de manutenção de time?\nEssa não foi eu que criei, nem me lembro como funciona..."));
			speaks.Add(new Speak("Jorves", "right", " Olá senhor, esta tela foi desenvolvida pelo Ceifas e Jesus.\n Aqui é onde o senhor pode visualizar os seus personagens, os equipamentos e as habilidades que eles possuem.\nUsando os ByteCoins que são adquiridos durante as batalhas, o senhor pode subir o nível dos seus equipamentos.\n Cada vez que você subir um nível de um equipamento, bônus aleatórios serão adicionados ao equipamento, os bônus variam conforme o tipo de equipamento.\n Use os ByteCoins com sabedoria, pois upar os equipamentos certos pode ser a diferença entre a vitória ou a derrota.\n Agora tenho que ir, boa sorte!"));
            speaks.Add(new Speak("Programador", "left", "Máquina inútil, invés de me explicar podia ter burlado o sistema e colocado todos os itens no nível máximo.\n Agora tenho que caçar dinheiro e prestar atenção em qual equipamento eu vou upar."));
			
			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign CriarMetodo {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("GameMaster", 157, 340);
			Sprite background = null;
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Jorves", "right", " Bem-vindo à tela de criação de métodos, Senhor!\n Eu criei essa tela repetidamente para ajudá-lo a controlar o seu time.\n Nesta tela o senhor poderá cria métodos para auxiliá-lo durante as batalhas.\n Será útil criar métodos para rotinas que você irá executar várias vezes, assim lhe poupando tempo."));
			speaks.Add(new Speak("Programador", "left", "Interessante Jorves, deu uma dentro dessa vez.\n Jorves?!\n Já sumiu de novo?! Só que me faltava, estou na caverna do dragão e o Jorves é o mestre dos magos!"));

			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign ResumoBatalha {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("GameMaster", 157, 340);
			Sprite background = null;
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Jorves", "right", " Senhor está tela serve para que o senhor possa acompanhar seu progresso enquanto avança pelos mapas.\n Aqui o senhor poderá acompanhar as informações de cada batalha na qual entrou, e ver todo o código usado em batalha.\n Agora tenho que ir, faça bom proveito deste recurso Senhor!"));
			speaks.Add(new Speak("Programador", "left", " Nem vou tentar falar com ele, ele some rápido demais...,\n mas esse é um recurso interessante."));

			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign Floresta {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("GameMaster", 157, 340);
			Sprite background = getSprite("Forest");
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Jorves", "right", " Senhor, venho lhe apresentar seu time.\n Esses são:\n Karspisky da classe Mago\n Nortun da classe Guerreiro\n E Pande da classe Arqueiro."));
			speaks.Add(new Speak("Programador", "left", " Um inútil trazendo mais inúteis, vamos ver se eles servem para alguma coisa."));
			speaks.Add(new Speak("Jorves", "right", " Eles com certeza serão uteis, desde que o senhor os use da forma correta.\n Como o senhor também não estava envolvido nesta parte do projeto, vou lhe explicar algumas coisas.\n Seus heróis possuem habilidades especiais, para vê-las o senhor deve acessar a tela de manutenção de time, mas somente fora de batalha.\n Em batalha, o senhor deve clicar sobre os rostos dos personagens, para que uma caixa de mensagem com as habilidades do personagem selecionado apareça e clicando sobre a habilidade desejada, você veja os detalhes da habilidade."));
			speaks.Add(new Speak("Jorves", "right", " Seus heróis possuem habilidades especiais, para vê-las o senhor deve acessar a tela de manutenção de time, mas somente fora de batalha."));
			speaks.Add(new Speak("Jorves", "right", " Em batalha, o senhor deve clicar sobre os rostos dos personagens, para que uma caixa de mensagem com as habilidades do personagem selecionado apareça e clicando sobre a habilidade desejada, você veja os detalhes da habilidade."));
			speaks.Add(new Speak("Programador", "left", " Interessante, talvez eles possam me ajudar mesmo.\n Vou fazer bom uso das habilidades deles."));
			speaks.Add(new Speak("Jorves", "right", " Apenas para complementar, as habilidades serão liberadas conforme seus personagens subirem de nível, ou seja, sempre fique de olho nas habilidades após um de seus personagens subir de nível, porque é provável que uma habilidade muito útil apareça.\n Boa sorte em sua batalha senhor."));
			speaks.Add(new Speak("Programador", "left", " O que é que esse Jorves está aprontando?\n Sempre aparece e logo desaparece, deve estar indo brincar de Pacman."));
			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign FlorestaChefe {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("TrollChief", 329, 380);
			Sprite background = getSprite("Forest");
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Programador", "left", " HAHA, foi fácil!\n Já estou vendo o terminal!\n Agora é só ir lá e ... Hã?!\n Que coisa feia é essa?! Dá uma licencinha aí, ô cara de pastel."));
			speaks.Add(new Speak("Chefe Troll", "right", " Troll não sair, aqui minha casa.\n Humano chegar na hora certa, agora é hora da comida!\n ATAQUEM OU MORRAM, GOBLINS!!! \n TROLL ESMAGA!"));
            speaks.Add(new Speak("Programador", "left", " Vish, jantar?!\n Acho melhor não, eu estou malpassado, não pego sol faz alguns anos...\n Mas se quer tentar a sorte, cai dentro bicho feio!"));
			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign PraiaChefe {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("PirateCaptain", 120, 230);
			Sprite background = getSprite("Beach");
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Programador", "left", " Aff, saio de uma floresta e agora tô numa praia.\n Odeio sol, cara!\n Fico todo queimado e descascando sempre que venho à praia."));
			speaks.Add(new Speak("Pirata Capitury", "right", " Ei, você tem alguma garrafa de rum aí com você?!\n Se tiver passe para cá, ou melhor, aproveita e passa tudo que você tem, mas pode ficar com a sua cueca."));
			speaks.Add(new Speak("Programador", "left", " Esses programadores, colocaram piratas nesse jogo?!\n Tão assistindo Piratas do Caribe demais.\n Ô seu pirata, desculpa, mas não sou chegado em bebida alcoólica, eu prefiro sinceramente um suco de laranja...\n Mas vai curtir uma praia vai, só quero fazer uma coisinha aqui e já tô de saída."));
			speaks.Add(new Speak("Pirata Capitury", "right", " Seu pirata?!\n Exijo respeito, me chame de Capitão...\n Capitão Catupiry.\n E pare de enrolação, ninguém passa daqui vivo!\n Passe todos seus pertences e volte por onde veio, ou irá andar na prancha e o tubarão vai te pegar."));
			speaks.Add(new Speak("Programador", "left", " Aí, tô falando que estão assistindo filme demais.\n O cara tá se achando o capitão do Perola Negra, não vô nem comentar o nome, e o pior, ainda curte um axé...\n Ok, vamos acabar com isso logo, não tenho todo tempo do mundo."));
			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign RuínasChefe {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("Griffin", 436, 340);
			Sprite background = getSprite("Ruins");
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Programador", "left", " Primeiro um troll burro, depois um pirata com nome de recheio de pizza, já até sei que vai vir algum bicho agora, sempre que chego perto de uma torre algum idiota aparece para me atrapalhar."));
			speaks.Add(new Speak("Griffin", "right", " E o idiota agora sou eu!\n Vou ignorar seu comentário e lhe dar uma chance.\n Este é um território perigoso, saia agora e volte por onde veio e terá uma chance de sobreviver, mesmo que por pouco tempo."));
			speaks.Add(new Speak("Programador", "left", " Tô cheio desses monstros!\n Eu tô mais quebrado do que arroz de terceira, mas eu preciso de um pouco de ação então eu vou te arrebentar."));
			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign MontanhaChefe {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("DragonHammertail", 486, 400);
			Sprite background = getSprite("Mountain");
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Programador", "left", " Tô na área, se derrubar é pênalti, cheguei pra abalar, hein galera!\n Finalmente tô quase lá, só mais esse e outro terminal e já era."));
			speaks.Add(new Speak("Hammertail", "right", " Que barulheira é essa em meus domínios?!\n Ah....Entendo....\n Um humano insolente, era de se esperar.\n A raça mais insignificante que existe!\n Você poderia ter passado livremente, mas você cometeu um crime grave, me acordou de meu precioso sono.\n Aqui, eu sou o juiz e o júri e sua sentença é a morte!"));
			speaks.Add(new Speak("Programador", "left", " É só o que me faltava, onde é que eu fui amarrar meu burro?!\n Fica esperto ae dragão, não tenho samba no pé, mas eu bato pra caramba, agora eu vou com a cara e a coragem!"));
			return new SpeakDesign(left, right, background, speaks); ;
		}
	}
	public SpeakDesign VulcãoChefe {
		get {
			Personagem left = new Personagem("Programmer", 92, 230);
			Personagem right = new Personagem("Demon", 486, 400);
			Sprite background = getSprite("Vulcanic");
			List<Speak> speaks = new List<Speak>();
			speaks.Add(new Speak("Programador", "left", " Finalmente, eu não conheci esse mundo por querer, mas acabei me envolvendo em toda essa treta.\n Quando eu descobrir quem foram os crackers que fizeram da minha vida um inferno eu vou acabar com o Facebook deles.\n Aff, o ultimo tinha que ser um vulcão, né? Que calor!\n Cadê o um anel pra queimar?!\n Ah não, essa é outra história."));
			speaks.Add(new Speak("TinhoDemon", "right", " Finalmente você chegou até aqui, humano.\n Deixe eu me apresentar.\n Sou TinhoDemon, o rei e mais forte de todos os demônios.\n Aquele que irá dominar todo este mundo.\n Parabéns por ter chego até aqui, mas meu território será seu túmulo!"));
            speaks.Add(new Speak("Programador", "left", " Nossa que enredo mais clichê, vou me lembrar de mudar isso quando voltar.\n Dane - se o mundo, que eu não me chamo Raimundo.\n Eu só preciso chegar naquele terminal ali, é rapidinho."));
			speaks.Add(new Speak("TinhoDemon", "right", " Cale-se e lute, mesmo que você não tenha a menor chance, anãozinho."));
			speaks.Add(new Speak("Programador", "left", " Você é grande, mas não é dois e eu sou pequeno, mas não sou meio!\n Prepare - se!\n Você é o último obstáculo e nada mais ficará entre eu, Tree Of Savior e uma pizza de Onion Rings com borda de Cream Cheese com Pepperoni!"));
            return new SpeakDesign(left, right, background, speaks); ;
		}
	}
}
public class SpeakDesign{
	public Personagem left;
	public Personagem right;
	public Sprite background;
	public List<Speak> speaks;

	public SpeakDesign(Personagem left, Personagem right, Sprite background, List<Speak> speaks) {
		this.left = left;
		this.right = right;
		this.background = background;
		this.speaks = speaks;
	}
}
public class Speak {
	public string nome;
	public string tag;
	public string speaks;

	public Speak(string nome, string tag, string speaks) {
		this.nome = nome;
		this.tag = tag;
		this.speaks = speaks;
	}
}
public class Personagem{
	public Sprite Sprite;
	public Sprite Face;
	public int width;
	public int heigth;

	public Personagem(string nomeSprite, int width, int heigth) {
		this.Sprite = Resources.Load<Sprite>("Personagens/"+ nomeSprite + "/Battler_Front_" + nomeSprite);
		this.Face = Resources.Load<Sprite>("Personagens/"+ nomeSprite + "/Face_" + nomeSprite);
        this.width = width;
		this.heigth = heigth;
	}
}
[Serializable]
public class Tela {
	public string tela;
	public bool lido;

	public Tela(string tela, bool lido) {
		this.tela = tela;
		this.lido = lido;
	}
}
