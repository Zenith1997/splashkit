using System;
using SplashKitSDK;

namespace MyProgram
{
    public class Program
    {
        public static void Main()
        {
            Window statusWindow = new Window("Quote of the Moment", 300, 100);

            MessageServer server = new MessageServer();

            while (server.IsRunning && !statusWindow.CloseRequested)
            {
                SplashKit.ProcessEvents();
                statusWindow.Clear(Color.White);

                statusWindow.DrawText(server.Message, Color.Black, 10, 10);
                statusWindow.DrawText("Activity:", Color.Black, 10, 30);
                statusWindow.DrawText("Close to quit server", Color.Black, 10, 60);

                statusWindow.Refresh(60);

                if (server.HasIncomingRequests)
                {
                    statusWindow.FillRectangle(Color.Green, 80, 30, 10, 10);
                    statusWindow.Refresh(60);

                    server.HandleNextRequest();

                    SplashKit.Delay(150);
                }
            }

            server.StopServer();
        }
    }

    /// <summary>
    /// The MessageServer creates a web server that maintains
    /// a "message of the moment" which clients can read and 
    /// write to.
    /// </summary>
    public class MessageServer
    {
        /// <summary>
        /// Indicates if the server should keep running.
        /// </summary>
        private bool _running = true;

        /// <summary>
        /// The message of the moment.
        /// </summary>
        private string _message = "Welcome to SplashKit server";

        /// <summary>
        /// The web server used to process client requests.
        /// Changed port to 8081 (avoid conflicts with 8080).
        /// </summary>
        private WebServer _server = new WebServer(8081);

        /// <summary>
        /// Indicates if the server is running.
        /// </summary>
        public bool IsRunning
        {
            get { return _running; }
        }

        /// <summary>
        /// The message of the moment.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// Checks if the server has incoming requests.
        /// </summary>
        public bool HasIncomingRequests
        {
            get { return _server.HasIncomingRequests; }
        }

        /// <summary>
        /// Stops the server.
        /// </summary>
        public void StopServer()
        {
            if (_running)
            {
                _running = false;
                _server.Stop();
            }
        }

        /// <summary>
        /// Handles the next incoming request, and sends it a response.
        /// </summary>
        public void HandleNextRequest()
        {
            HttpRequest request = _server.NextWebRequest;

            try
            {
                if (request.IsGetRequestFor("/stop"))
                {
                    request.SendResponse("Server stopped");
                    StopServer();
                }
                else if (request.IsGetRequestFor("/message"))
                {
                    request.SendResponse(_message);
                }
                else if (request.IsPutRequestFor("/message"))
                {
                    _message = request.Body;
                    request.SendResponse();
                }
                else if (request.IsGetRequestFor("/index.html") || request.IsGetRequestFor("/"))
                {
                    request.SendHtmlFileResponse("message_index.html");
                }
                else
                {
                    request.SendResponse(HttpStatusCode.HttpStatusBadRequest);
                }
            }
            catch
            {
                request.SendResponse(HttpStatusCode.HttpStatusInternalServerError);
            }
        }
    }
}
