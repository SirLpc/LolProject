using UnityEngine;
using System.Collections;

namespace AssemblyCSharp
{
    public class RPCController
    {
        private RPCController() { }
        private static RPCController defaultCtr = null;
        public static RPCController DefaultCtr
        {
            get
            {
                if (defaultCtr == null)
                    defaultCtr = new RPCController();
                return defaultCtr;
            }
        }



        //NetworkView networkView = LPC_GameServer.DefalutNetworkView.RPC()



    }
}

