function getRPC() {
    var rpc = new JSONRPCProxy('/json/', ['PollData', 'Throttle', 'ToggleGear']);
    return rpc;
}