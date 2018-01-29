using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManager
{
    class TemporaryNodecs
    {


//        using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace NodeManagement
//    {
//        public class Node : INode
//        {
//            private readonly Random _rnd;

//            #region Properties
//            // Basic properties
//            public int NodeId { get; private set; }
//            public string City { get; private set; }

//            // State
//            public DateTime OnlineTime { get; private set; }
//            public bool IsOnline { get; private set; }

//            // Metrics
//            public float UploadUtilization { get; private set; }
//            public float DownloadUtilization { get; private set; }
//            public float ErrorRate { get; private set; }
//            public uint ConnectedClients { get; private set; }
//            #endregion

//            #region Initialization
//            public Node(int nodeId, string city)
//            {

//            }
//            public Node(int nodeId, string city, Random rnd)
//            {
//                _rnd = rnd;

//                NodeId = nodeId;
//                City = city;

//                OnlineTime = DateTime.Now;

//                IsOnline = false;

//                ResetMetrics();
//            }

//            #endregion

//            #region Public Methods
//            public void SetOnline()
//            {
//                IsOnline = true;

//                SimulateRandomMetrics();
//            }

//            public void SetOffline()
//            {
//                IsOnline = false;

//                ResetMetrics();
//            }
//            #endregion

//            #region Private Methods
//            private void ResetMetrics()
//            {
//                // Clear metrics back to 0.

//                ConnectedClients = 0;

//                UploadUtilization = 0.0f;
//                DownloadUtilization = 0.0f;
//                ErrorRate = 0.0f;
//            }

//            private void SimulateRandomMetrics()
//            {
//                // Generate random values to simulate metrics.

//                ConnectedClients = (uint)_rnd.Next(1, 500);

//                UploadUtilization = (float)_rnd.NextDouble();
//                DownloadUtilization = (float)_rnd.NextDouble();
//                ErrorRate = (float)_rnd.NextDouble();
//            }
//            #endregion
//        }
//    }



}
}
