    ����          �using Utils;
using Movements;
using Character;
public class CSCodeEvaler{ 
private TurnManager turnManager;
public void EvalCode(){
turnEnd(); 
} 
private Player getPlayer(string playerName){
setTurnManager();return turnManager.getPlayer(playerName);
} 
private void turnEnd(){
setTurnManager();new Task (turnManager.NextTurn(), true);
} 
private void setTurnManager(){
if (turnManager == null) {this.turnManager = new Generic().getTurnManager();}} 
} 
