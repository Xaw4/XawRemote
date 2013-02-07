using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace XawRemoteServer
{
    /************************************************************************/
    /* A Class espetially dedicated to one Computer/Turtle                                                                     */
    /************************************************************************/
    class CCFriend
    {
        public String Label { get; private set; }
        private Queue<String> CommandQue = new Queue<String>();

        public CCFriend(String label)
        {
            Label = label;

        }

        public void addCommand(String command)
        {
            CommandQue.Enqueue(command);
        }

        public bool isIdle()
        {
            return CommandQue.Count == 0;
        }

        public void handleRequest(HttpListenerContext context)
        {
            try
            {
                if (isIdle())
                {
                    // need to do something more inteligent, to prevent request-spamming
                    // -- i want to make it wait on server side, not on client(cc)-side
                    sendMessage("wait", context.Response);
                }
                else
                {
                    String message = CommandQue.Dequeue();
                    sendMessage(message, context.Response);

                }
            }
            catch (System.Exception ex)
            {
                Debug.Print("Excepion in CCFriend.handleRequest: {0}", ex);
            }
                

        }

        private static void sendMessage(String msg, HttpListenerResponse response){
            // Construct a response. 
            string responseString = msg;
            Debug.Print("ResponseString: "+responseString);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
        }

    }
}
