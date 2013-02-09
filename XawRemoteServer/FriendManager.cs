using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace XawRemoteServer
{
    /************************************************************************/
    /* A Class, that wraps the Management of the CCFriends                  */
    /************************************************************************/
    class FriendManager
    {
        private Dictionary<String, CCFriend> friends = new Dictionary<String, CCFriend>();

        public String[] getLables()
        {
            return friends.Keys.ToArray<String>();
        }

        public void handleRequest(String label, HttpListenerContext context)
        {
            try
            {
                if (!friends.ContainsKey(label))
                {
                    friends.Add(label, new CCFriend(label));
                    Debug.Print("Added new friend {0}", label);
                }
                friends[label].handleRequest(context);
            }
            catch (Exception ex)
            {
                Debug.Print("Excepion in FriendManager.handleRequest: {0}", ex);
            }
        }
        public void execCommand(String label, String command)
        {
            try
            {
                if (!friends.ContainsKey(label))
                {
                    Debug.Print("The Computer {0} has no friend", label);
                }
                else
                {
                    friends[label].addCommand(command);
                }
            }
            catch (Exception ex)
            {
                Debug.Print("Excepion in FriendManager.execCommand: {0}", ex);
            }
        }
    }
}
