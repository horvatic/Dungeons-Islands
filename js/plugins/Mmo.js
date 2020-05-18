const socket = new WebSocket('ws://127.0.0.1:8080');
const id = getRandomInt(2000);
var otherPlayers = {};

function broadcastLocation(x, y) {
  socket.send(`${id},${x},${y}`);
}

socket.addEventListener('message', function (event) {
    var playerInfo = event.data.split(',');
    if(playerInfo[0] == id) {
      return;
    }

    try {
      if(otherPlayers[`${playerInfo[0]}`] == null) {
        otherPlayers[`${playerInfo[0]}`] = $gameMap.createActorAt(1, playerInfo[1], playerInfo[2], 2, 1, true);
      }
      else {
        var otherPlayer = otherPlayers[`${playerInfo[0]}`];
        otherPlayer.moveStraight( otherPlayer.findDirectionTo(playerInfo[1], playerInfo[2]));
      }
    }
    catch(err) {
      console.log(err);
    }
});

function getRandomInt(max) {
  return Math.floor(Math.random() * Math.floor(max));
}