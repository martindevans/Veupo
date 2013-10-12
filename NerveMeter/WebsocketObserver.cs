using System;
using System.Collections.Generic;
using System.Diagnostics;
using Fleck;
using Newtonsoft.Json;

namespace NerveMeter
{
    public class WebsocketObserver<T>
        :IObserver<T>
    {
        private WebSocketServer _websocket;
        private readonly List<IWebSocketConnection> _connections = new List<IWebSocketConnection>();

        public readonly string Endpoint;

        public WebsocketObserver(ushort port)
        {
            Endpoint = "ws://localhost:" + port;

            _websocket = new WebSocketServer(Endpoint);
            _websocket.Start(socket =>
            {
                socket.OnBinary = OnBinaryMessage;
                socket.OnClose = () => OnClose(socket);
                socket.OnError = OnWebsocketError;
                socket.OnMessage = msg => OnMessage(socket, msg);
                socket.OnOpen = () => OnOpen(socket);
            });
        }

        #region websocket events
        private void OnBinaryMessage(byte[] msg)
        {
            throw new NotImplementedException();
        }

        private void OnOpen(IWebSocketConnection connection)
        {
            _connections.Add(connection);
        }

        private void OnMessage(IWebSocketConnection connection, string message)
        {
            Trace.TraceInformation("Websocket message " + message);
        }

        private void OnWebsocketError(Exception error)
        {
            Trace.TraceError("Websocket error: " + error.Message);
        }

        private void OnClose(IWebSocketConnection connection)
        {
            _connections.Remove(connection);
        }
        #endregion

        #region observer events
        public void OnCompleted()
        {
            if (_websocket != null)
                _websocket.Dispose();
            _websocket = null;

            foreach (var webSocketConnection in _connections)
                webSocketConnection.Close();
            _connections.Clear();
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value)
        {
            var json = JsonConvert.SerializeObject(value);
            foreach (var webSocketConnection in _connections)
                webSocketConnection.Send(json);
        }
        #endregion
    }
}
