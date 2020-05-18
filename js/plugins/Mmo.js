const socket = new WebSocket('ws://127.0.0.1:8080');

function setMmoMessage(message) {
  socket.send(message);
}

socket.addEventListener('message', function (event) {
    $gameMessage.setFaceImage('Actor1',0)
    $gameMessage.setBackground(1)
    $gameMessage.setPositionType(1)
    $gameMessage.add(event.data)
});
