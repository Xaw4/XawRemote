﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Diagnostics;
using System.Net.Sockets;

namespace XawRemoteServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private String msg=null;
        private HttpListener _listener = null;
        private FriendManager manager = new FriendManager();
        
        public MainWindow()
        {
            InitializeComponent();
           
        }
        private HttpListener GetListener()
        {
            if (_listener == null)
            {
                _listener = new HttpListener();
                _listener.Prefixes.Add("http://+:8888/cc/");
                
                _listener.Start();
            }
            return _listener;
        }

        public String gettext()
        {
            return this.ExpressionBox.Text.ToString();
        }

        /******************************************************
         * Button - functions
         * 
         * ***/
        private void Start_Click(object sender, RoutedEventArgs e)
        {

            Debug.Print("start_click");

            //if (listener == null) listener = new TcpListener(8888);

            msg = gettext();
            beginNewContext();
            
            Debug.Print("prefixes: ");
            foreach (String s in GetListener().Prefixes)
            {
                Debug.Print("- "+s);
            }
            Debug.Print("isListening: " + GetListener().IsListening);
            
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            String testTurtleLabel = "hubert";
            manager.execCommand(testTurtleLabel, gettext());
        }

        /*
         * Network Functions
         */
        private void beginNewContext()
        {
            Debug.Print("msg: " + msg);
            AsyncCallback callback = MessageRecieved;

            IAsyncResult result =
                GetListener().BeginGetContext(callback, GetListener());

        }

        private void MessageRecieved(IAsyncResult result)
        {
            try
            {
                Debug.Print("Message!");
                HttpListener listener = (HttpListener)result.AsyncState;
                // Call EndGetContext to complete the asynchronous operation.
                HttpListenerContext context = listener.EndGetContext(result);
            
                HttpListenerRequest request = context.Request;
                Debug.Print(request.ToString());
                Debug.Print("RawUrl: " + request.RawUrl + " Url: " + request.Url);
                Debug.Print("Method: " + request.HttpMethod);
                Debug.Print("QueryString: " + request.QueryString + " len: " + request.QueryString.Count);
                foreach (String s in request.QueryString.AllKeys)
                {
                    Console.WriteLine("   {0,-10} {1}", s, request.QueryString[s]);
                }
                String ccLabel = request.QueryString["label"];
                manager.handleRequest(ccLabel, context);

                beginNewContext();

            }
            catch (System.Exception ex)
            {
                Debug.Print("Excepion in MainWindow.MessageRecieved: {0}", ex);
            }
            /*
            // Obtain a response object.
            HttpListenerResponse response = context.Response;

             
            // Construct a response. 
            string responseString = msg;// "turtle.forward()";//this.ContentBox.Text; 
            Debug.Print("ResponseString: "+responseString);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();*/

        }
    }
}
