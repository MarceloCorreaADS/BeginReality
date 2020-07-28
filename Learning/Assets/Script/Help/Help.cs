using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class Help {
	public List<HelpDescription> helpDescriptions { get; private set; }

	public Help() {
		List<HelpDescription> lista = new List<HelpDescription>();
		foreach (HelpDescription h in getHelpDescriptions()) {
			h.helpDescriptionSub = getHelpDescriptionSubs(h.title);
			lista.Add(h);
		}
		helpDescriptions = lista;
	}

	public HelpDescription Tutorial {
		get {
			string title = null;
			List<HelpDescriptionSub> helpDescriptionSubs = new List<HelpDescriptionSub>();
			title = "Tutorial";
			return new HelpDescription(title, helpDescriptionSubs);
		}
	}
	public HelpDescription Batalha {
		get {
			string title = null;
			List<HelpDescriptionSub> helpDescriptionSubs = new List<HelpDescriptionSub>();
			title = "Batalha";
			return new HelpDescription(title, helpDescriptionSubs);
		}
	}
	public HelpDescription MapaMundo {
		get {
			string title = null;
			List<HelpDescriptionSub> helpDescriptionSubs = new List<HelpDescriptionSub>();
			title = "Mapa Mundo";
			return new HelpDescription(title, helpDescriptionSubs);
		}
	}
	public HelpDescription ManutencaoEquipe {
		get {
			string title = null;
			List<HelpDescriptionSub> helpDescriptionSubs = new List<HelpDescriptionSub>();
			title = "Manutenção de Equipe";
			return new HelpDescription(title, helpDescriptionSubs);
		}
	}
	public HelpDescription Metodos {
		get {
			string title = null;
			List<HelpDescriptionSub> helpDescriptionSubs = new List<HelpDescriptionSub>();
			title = "Métodos";
			return new HelpDescription(title, helpDescriptionSubs);
		}
	}
	public HelpDescription ResumoBatalha {
		get {
			string title = null;
			List<HelpDescriptionSub> helpDescriptionSubs = new List<HelpDescriptionSub>();
			title = "Resumos de Batalha";
			return new HelpDescription(title, helpDescriptionSubs);
		}
	}
	public HelpDescriptionSub T1 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Pontos de Ação";
			titleParent = "Tutorial";
			description += " Para evitar que o jogador possa fazer inúmeras ações e matar os inimigos em um único turno foi implementado um sistema de pontos de ação que onde cada personagem possui uma quantidade de pontos de ação por turno, e cada ação que ele faz consome uma certa quantidade desses pontos podendo-se realizar ações somente enquanto houver pontos de ação.\n";
			description += " Para saber mais sobre como funciona os pontos de ação e quanto cada ação gasta de pontos de ação vá ao tópico de “Batalha” e ache o sub tópico “Pontos de Ação” onde é melhor explicado sobre isso.\n";
			description += " Dica: Sempre leve em consideração seus pontos de ação e faça as contas corretamente, pois a falta de pontos durante um turno pode te levar à derrota.\n";

			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub T2 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Utilizando um player";
			titleParent = "Tutorial";
			description += " Para utilizar um player você deve pegar o player que você deseja e adiciona-lo em uma variável para utilizá-lo.\n";
			description += " Para isso você deve usar alguns métodos pré-programados que são:\n";
			description += " Player soldier = getSoldier();\n";
			description += " Player mage = getMage();\n";
			description += " Player archer = getArcher();\n";
			description += " Esses métodos buscam e retornam um objeto tipo Player de seu time que possui a classe à qual o método se refere, ou seja, por exemplo o método getSoldier() irá retornar o objeto do tipo Player do seu time que tem a classe Soldier.\n";
			description += " Com isso você pode acessar o player usando apenas a variável que você criou.";

			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub T3 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Se movimentando";
			titleParent = "Tutorial";
			description += " Para se movimentar basta utilizar o tipo move que está dentro de Player, e utilizar o método conforme a direção que você deseja, informando a quantidade de posições que você deseja andar.\n";
			description += " Exemplo:\nplayer1.move.MoveUp(3);\n";
			description += " Você pode acumular mais de uma movimentação, para cima, baixo, direita ou esquerda, use uma sintaxe após a outra na ordem que você deseja que as ações sejam feitas.\n";
			description += " Depois que você definir as movimentações você deve usar o método “Move()” para efetivar as movimentações.\n";
			description += " Exemplo:\n player1.move.MoveUp(3);\n player1.move.MoveRight(2);\n player.move.Move();\n";
			description += " As funções direcionais como por exemplo “player.move.MoveRight(2); ” possui um retorno do tipo “bool” que identifica se a função pode ser executada, ou seja, ela informa se consegue ou não fazer esse movimento e lhe dá um retorno de “true” ou “false” para que você possa tratar caso não possa fazer esse movimento.\n";
			description += " Dica: Sempre faça a verificação de que seu movimento vá ou não funcionar, pois se um movimento não funcionar os passos seguintes de sua estratégia podem também não funcionar.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub T4 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Atacando com ataque básicos";
			titleParent = "Tutorial";
			description += " Para realizar um ataque básico você deve acessar os métodos de ataque que se encontram dentro de action que é acessado a partir do player.\n";
			description += " Existem cinco métodos de ataque, um no qual você passa como parâmetro o nome do inimigo que deseja atacar e outros quatro que são ataques nas 4 direções.\n";
			description += " Exemplos:\n player.action.Attack(“NomeDoAlvo”);\n player.action.AttackUp();\n player.action.AttackDown();\n player.action.AttackRight();\n player.action.AttackLeft();\n";
			description += " Lembrando que os ataques devem respeitar a distância mínima e máxima dos ataques básicos de cada classe que são:\n";
			description += " Guerreiro: Mínima 1 e Máxima 1\n Arqueiro: Mínima 2 e Máxima 5\n Mago: Mínima 2 e Máxima 5";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub T5 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Atacando com Ataques Especiais (Habilidades Especiais)";
			titleParent = "Tutorial";
			description += " É possível usar ataques especiais que são muito mais poderosos e que as vezes possuem uma distância de ataque diferente que os ataques básicos, mas use-os com sabedoria pois estes gastam SP.\n";
			description += " Você pode ver os detalhes de suas Habilidades clicando sobre as imagens de sua equipe em batalha e ver as habilidades disponíveis para eles. Lembrando que habilidades novas são liberadas a cada 2 níveis.\n";
			description += " Os ataques especiais se encontram dentro de action também, mas diferente de ataque básico existe somente uma forma de usá-lo e é passando seu nome e o nome do alvo, caso esteja dentro da distância mínima e máximo da habilidade a mesma será executada.\n";
			description += " Exemplo:\n player.action.Ability(“NomeDaHabilidade”, “NomeDoAlvo”);\n";
			description += " Lembrando que cada membro de sua equipe possui suas habilidades e não pode utilizar habilidades de outro membro da equipe.";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub T6 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Final";
			titleParent = "Tutorial";
			description += " Esses foram os códigos específicos do sistema, de resto a programação funciona com padrões iguais à programação normal sendo usado como base programação C#, pode-se usar IF, DO-While, For, For Each, etc.\n";
			description += " Seja criativo e crie códigos dinâmicos e inteligentes que te ajudem em sua estratégia.\n";
			description += " Boa Batalha!\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub B1 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Turno e Codificação";
			titleParent = "Batalha";
			description += " As batalhas funcionam em turno, portanto você só poderá realizar suas ações após o turno inimigo estiver terminado, mas ainda assim pode deixar seu código pronto e apenas apertar o executar quando ele for liberado, mas não é aconselhável, pois dependendo das ações de seus inimigos você poderá ter que mudar sua estratégia.\n";
			description += " Você poderá escrever seu código no console de codificação que se encontra no canto direito da tela de batalha, ele não possui limites e você pode navegar pelo texto usando as setas de seu teclado.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub B2 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Pontos de Ação";
			titleParent = "Batalha";
			description += " Os pontos de ação foram implementados para impedir que o jogo acabe em um único turno, eles servem para limitar a quantidade de ações dos jogadores, mas não se engane achando que os monstros não seguem essa regra, pois eles também possuem a mesma quantidade de pontos de ação que os jogadores.\n";
			description += " O sistema de pontos de ação funciona da seguinte forma:\n";
			description += " Cada personagem ao iniciar seu turno recebe um valor de pontos de ação.\n";
			description += " Para cada ação realizada pelo personagem é descontado o valor que ele custa de pontos de ação da quantidade total.\n";
			description += " Quando o valor de pontos de ação de um personagem chega a 0 todas as ações realizadas por aquele personagem depois de acabar os pontos de ação não são realizadas.\n";
			description += " No próximo turno os pontos de ação são restaurados ao valor máximo para que o personagem possa realizar suas ações novamente.\n";
			description += " Segue uma lista de quanto cada ação tem de custo de pontos de ação:\n";
			description += " Pontos de ação total para cada personagem do time: 15;\n";
			description += " Movimentação: Pontos de movimentação dependem do tipo de ataque do personagem, e a seguinte regra é aplicada:\n";
			description += " \nCorpo a Corpo:\n Até 4 quadrados: 2 pontos por quadrados;\n Acima de 4 quadrados: 3 pontos por quadrados;\n";
			description += " \nÀ distância:\n Até 2 quadrados: 2 pontos por quadrados;\n Acima de 2 quadrados: 3 pontos por quadrados;\n";
			description += " \nAtaque básico: 5 pontos;\n";
			description += " Ataque especial (Habilidade): Cada habilidade possui um custo, verifique na descrição da habilidade;\n";
			description += " Use com sabedoria os pontos de ação para que consiga fazer a melhor estratégia.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub B3 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Acerto e Classe de Armadura";
			titleParent = "Batalha";
			description += " O acerto é um atributo calculado a partir de seus atributos base juntamente com os bônus de itens e que irá definir se você acerta ou não o seu alvo.\n";
			description += " A classe de armadura é um atributo também calculado a partir de seus atributos base junto com os bônus de itens que irá definir se você será acertado ou não pelo ataque inimigo.\n";
			description += " Os dois valores podem variar para mais ou para menos, pois além do valor calculado é rodado um dado para trazer imprevisibilidade ao processo. Por isso caso você tenha acertado um ataque não necessariamente acertará novamente, mas quanto maior o valor de seu acerto maior a chance de acertar um alvo, assim como quanto maior sua classe de armadura mais difícil será de um inimigo acertá-lo.\n";
			description += " Os Valores serão comparados entre si e o maior ganha, assim se o acerto for maior consequentemente o ataque irá acertar, caso seja a classe de armadura o ataque não surtirá efeito.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub B4 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Consoles de Report";
			titleParent = "Batalha";
			description += " No canto inferior esquerdo possui um console de report onde informações das batalhas irão ser mostradas em suas respectivas abas para que você possa ir acompanhando.\n";
			description += " Você pode alternar entre as abas clicando sobre o nome da mesma, onde cada aba irá exibir um tipo de informação diferente.\n";
			description += " Abaixo à a explicação sobre cada uma das abas:\n";
			description += " Report: Nessa aba será mostrado as informações referentes aos seus códigos, caso você tenha colocado algo que não poderia ser colocado, ou caso haja algum erro, será nessa aba que isso será informado.\n";
			description += " Batalha: Aqui serão informadas todas as informações de batalha que estão ocorrendo, desde os danos, uso de sp, e se os ataques tanto dos aliados quanto dos inimigos acertaram.\n";
			description += " História: Caso a tela tenha alguma conversa tela relacionada a história do jogo, você poderá relê-la novamente nesta aba.\n";
			description += " Missão: Na última aba serão escritas, caso haja, as missões que precisam ser realizadas para a batalha na qual você está.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub B5 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Caixas de Status";
			titleParent = "Batalha";
			description += " Nos cantos superior direito e esquerdo do mapa de batalha é possível encontrar duas caixas de status onde são encontradas as imagens da sua equipe na caixa da direita e na caixa esquerda os seus inimigos.\n";
			description += " Nas duas é possível encontrar duas barras para cada personagem, uma que mostra sua vida, e outra que mostra sua mana, e que são atualizadas automaticamente conforme se perde vida ou usa a mana.\n";
			description += " As duas caixas podem ser escondidas ou reexibidas clicando no botão de seta que se encontra acima da caixa.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub B6 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Visualizar Habilidades";
			titleParent = "Batalha";
			description += "Em batalha é possível visualizar as habilidades de sua equipe clicando sobre a imagem do personagem que você deseja ver, quando isso for feito irá aparecer uma caixa com todos os nomes das habilidades disponíveis para uso do seu personagem, quando clicado em cima do nome de uma habilidade uma outra caixa irá aparecer dando a descrição completa da habilidade.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub B7 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Visualizar Métodos";
			titleParent = "Batalha";
			description += "Juntamente com as abas do report há um botão de método que ao ser clicado irá aparecer uma caixa que irá exibir uma lista com o nome de todos os métodos que você criou e ao clicar sobre esses métodos você poderá ver o método completo.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub B8 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Sair da tela ou fechar o jogo";
			titleParent = "Batalha";
			description += "Caso você saia de batalha seja por meio do menu ou por fechar o jogo você perderá o progresso, experiência e Byte Coins conseguidas naquela batalha em especifico.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub MM1 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Indo para o próximo mapa";
			titleParent = "Mapa Mundo";
			description += " Para você avançar no jogo você deve passar por todos os mapas do jogo, e para isso você deve chegar ao ponto de passagem para o próximo mapa que normalmente se encontra no outro lado do mapa. Chegando lá você deve cumprir as condições para que possa ser liberado o seu avanço.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub MM2 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Liberando acesso ao próximo mapa";
			titleParent = "Mapa Mundo";
			description += " Para liberar o ponto de passagem você deve ir ao terminal que se encontrará em algum lugar do mapa no qual você está e acessá-lo para liberar a passagem, porém para acessar o terminar você deverá primeiro derrotar o chefe do mapa.\n";
			description += " Para enfrenta-lo basta encontra-lo no mapa e ir de encontro com ele, após derrota-lo volte ao terminal e depois ao ponto de passagem para avançar.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub MM3 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Entrando em Batalhas";
			titleParent = "Mapa Mundo";
			description += " Mas não pense que será tão simples assim, enquanto você anda pelo mapa você poderá esbarrar em grupos de monstros e você deve enfrenta-los e ganhar para poder avançar, pois caso você perca além de não ganhar nada pela batalha ainda irá perder experiência e byte coin que você já tinha ganho.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub ME1 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Status";
			titleParent = "Manutenção de Equipe";
			description += " Na tela de manutenção de equipe clicando sobre a imagem de um personagem da sua equipe você terá acesso a todas as informações dele, como os atributos base, byte coin que ele possui, itens e habilidades.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub ME2 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Habilidades";
			titleParent = "Manutenção de Equipe";
			description += " Ao clicar em habilidades será aberto uma caixa que irá mostrar todas as habilidades utilizáveis do personagem selecionado.\n";
			description += " Clicando sob o nome da habilidade será aberta outra caixa com a descrição completa da habilidade.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub ME3 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Itens";
			titleParent = "Manutenção de Equipe";
			description += " Assim como a habilidades, clicando sob itens será exibido uma caixa com a lista de itens que o personagem seleciona possui, e ao clicar sob o nome do item uma caixa mostrando a descrição do item e seus bônus serão mostrados, além de aparecer o botão de Melhorar item.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub ME4 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Melhoria de item";
			titleParent = "Manutenção de Equipe";
			description += " Todos os itens começam no nível 0 e podem ser melhorados até o nível 10 clicando sob o botão que se encontra na sua descrição.\n";
			description += " Toda vez que você melhorar um item será cobrado um valor que irá mudar conforme o nível do item, quanto maior o nível maior o valor, caso você não tenha byte coin suficiente para o próximo nível o botão de melhoria será bloqueado até que você consiga o valor.\n";
			description += " Quando um item é melhorado, são escolhidos três bônus conforme o tipo de item e valores aleatórios são gerados conforme a afinidade da classe com o tipo de bônus.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub M1 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Criar Métodos";
			titleParent = "Métodos";
			description += " Você pode criar métodos que irão lhe ajudar durante o jogo, evitando que você tenha que reescrever códigos muitas vezes, para isso pense no que pode ser transformado em método.\n";
			description += " Para cria-lo clique sobre o botão “Criar Método” preencha os campos solicitados e clique em salvar, e ele irá aparecer na lista de métodos no canto direito.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub M2 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Editando e Excluindo";
			titleParent = "Métodos";
			description += " Você pode editar os métodos clicando sobre eles na lista de métodos que se encontra no canto direito e depois alterando as informações nos campos de textos, após fazer as alterações desejadas basta clicar em salvar.\n";
			description += " Caso queira excluir um método basta clicar sob o botão “X” que se encontra ao lado do nome de cada método na lista de métodos, e depois confirmar a exclusão no alerta que irá aparecer.\n";

			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub M3 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Usando o método";
			titleParent = "Métodos";
			description += " Para usar o método em batalha é como se você o tivesse usando em programação, chamando o nome dele no console de codificação da batalha, nada fora do normal, veja um exemplo:\n";
			description += " atacarInimigoProximo();\n";
			description += " string nomeInimigo = pegarNomeCoordenada(10,5);\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub M4 {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Ver método em Batalha";
			titleParent = "Métodos";
			description += "Durante a batalha caso você esqueça algo do código de algum método, caso isso aconteça você tem como visualizar os seus métodos em batalha.\n";
			description += "Juntamente com as abas do report há um botão de método que ao ser clicado irá aparecer uma caixa que irá exibir uma lista com o nome de todos os métodos que você criou e ao clicar sobre esses métodos você poderá ver o método completo.\n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public HelpDescriptionSub RB {
		get {
			string title = null;
			string titleParent = null;
			string description = null;

			title = "Resumo";
			titleParent = "Resumos de Batalha";
			description += "No resumo de batalhas é possível encontrar uma lista de todas as batalhas que você passou, sejam elas vencidas ou perdidas.\n";
			description += "Ao clicar sob uma batalha no canto direito irá aparecer o resumo dessas batalhas mostrando as informações de data da batalha, quantos turnos durou, inimigos e todos os códigos que você utilizou na batalha.  \n";
			return new HelpDescriptionSub(title, titleParent, description);
		}
	}
	public List<PropertyInfo> getPropertyInfos(Type type) {
		List<PropertyInfo> propertyInfos = new List<PropertyInfo>((GetType().GetProperties() as PropertyInfo[]));
		object objec;
		propertyInfos = propertyInfos.FindAll(objeto => (objec = objeto.GetValue(this, null)) != null && objec.GetType().Equals(type));
		objec = null;

		return propertyInfos;
	}

	public List<HelpDescriptionSub> getHelpDescriptionSubs(string title) {
		List<PropertyInfo> propertyInfos = getPropertyInfos(typeof(HelpDescriptionSub));
		List<HelpDescriptionSub> helpDescriptionSubs = new List<HelpDescriptionSub>();
		foreach (PropertyInfo objeto in propertyInfos) {
			HelpDescriptionSub helpDescriptionSub = objeto.GetValue(this, null) as HelpDescriptionSub;
			if (helpDescriptionSub != null && helpDescriptionSub.titleParent == title)
				helpDescriptionSubs.Add(objeto.GetValue(this, null) as HelpDescriptionSub);
		}
		return helpDescriptionSubs;
	}
	public List<HelpDescription> getHelpDescriptions() {
		List<PropertyInfo> propertyInfos = getPropertyInfos(typeof(HelpDescription));
		List<HelpDescription> helpDescriptions = new List<HelpDescription>();
		foreach (PropertyInfo objeto in propertyInfos) {
			HelpDescription helpDescription = objeto.GetValue(this, null) as HelpDescription;
			if (helpDescription != null)
				helpDescriptions.Add(objeto.GetValue(this, null) as HelpDescription);
		}
		return helpDescriptions;
	}
}
public class HelpDescription {
	public string title;
	public List<HelpDescriptionSub> helpDescriptionSub;

	public HelpDescription(string title, List<HelpDescriptionSub> helpDescriptionSub) {
		this.title = title;
		this.helpDescriptionSub = helpDescriptionSub;
	}
}
public class HelpDescriptionSub {
	public string title;
	public string titleParent;
	public string description;

	public HelpDescriptionSub(string title, string titleParent, string description) {
		this.title = title;
		this.titleParent = titleParent;
		this.description = description;
	}
}