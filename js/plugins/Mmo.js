var socket;
try {
  socket = new WebSocket('ws://PUBLIC_IP:8080');
}
catch(err) {
  socket = new WebSocket('ws://127.0.0.1:8080');
}

const id = getRandomInt(2000);
var otherPlayers = {};
var currentMap = null;

function broadcastLocation(x, y) {
  socket.send(`${id},${x},${y},${$dataMap.displayName}`);
}

socket.addEventListener('message', function (event) {
    if($dataMap == null || $gameMap == null)  {
      return;
    }
    var playerInfo = event.data.split(',');
    if(playerInfo[0] == id) {
      return;
    }
    if(playerInfo[3] != $dataMap.displayName) {
      if(otherPlayers[`${playerInfo[0]}`] != null) {
        var otherPlayer = otherPlayers[`${playerInfo[0]}`];
        try {
          $gameMap.eraseEvent(otherPlayer.eventId());
        }
        catch(err) {
          console.log(err);
        }
        otherPlayers[`${playerInfo[0]}`] = null;
      }
      return;
    }
    try {
      if(otherPlayers[`${playerInfo[0]}`] == null) {
        otherPlayers[`${playerInfo[0]}`] = $gameMap.createActorAt(2, playerInfo[1], playerInfo[2], 2, 1, true);
      }
      else {
        var otherPlayer = otherPlayers[`${playerInfo[0]}`];
        otherPlayer.moveStraight(otherPlayer.findDirectionTo(playerInfo[1], playerInfo[2]));
      }
    }
    catch(err) {
      console.log(err);
    }
});

function getRandomInt(max) {
  return Math.floor(Math.random() * Math.floor(max));
}