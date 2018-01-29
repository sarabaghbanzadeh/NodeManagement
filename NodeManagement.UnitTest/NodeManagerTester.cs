namespace NodeManagement.UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    [TestClass]
    public class NodeManagerTester
    {


        [TestMethod]
        public void AddNode_Success()
        {
            var nodeManager = new NodeManager();

            nodeManager.AddNewNodeToDataCenter("Ottawa");

            var nodes = nodeManager.getNodes();
            var node = nodes[1];

            // The first node has been added
        }


        [TestMethod]
        public void AddClient_Success()
        {
            var nodeManager = new NodeManager();

            nodeManager.AddNewNodeToDataCenter("Ottawa");

            nodeManager.AddNewClientToTheSystem("Sara Baghbanzadeh", "Ottawa", 1);

            var nodes = nodeManager.getNodes();
            var node = nodes[1];

            var client = node.allConnectedClients[0];

            // The client has been added to the node
        }


        [TestMethod]
        public void UpdateMetrics_Success()
        {
            var nodeManager = new NodeManager();

            nodeManager.AddNewNodeToDataCenter("Ottawa");
            nodeManager.AddNewNodeToDataCenter("Toronto");
            nodeManager.AddNewNodeToDataCenter("Ottawa");
            nodeManager.AddNewNodeToDataCenter("Gatineau");

            nodeManager.UpdateMetricsAutomatically();

            // All metrics got updated
        }


        [TestMethod]
        public void RemoveNode_Success()
        {
            var nodeManager = new NodeManager();

            nodeManager.AddNewNodeToDataCenter("Ottawa");
            nodeManager.AddNewNodeToDataCenter("Toronto");
            nodeManager.AddNewNodeToDataCenter("Ottawa");
            nodeManager.AddNewNodeToDataCenter("Gatineau");

            Assert.IsTrue(nodeManager.getNodes().Count == 4);
            nodeManager.RemoveNode(1);
            Assert.IsTrue(nodeManager.getNodes().Count == 3);

            // Node has been deleted from the system
        }


        [TestMethod]
        public void RemoveClient_Success()
        {
            var nodeManager = new NodeManager();

            List<Client> clients = new List<Client>();
            Dictionary<uint, Client> cli = new Dictionary<uint, Client>();

            Client c1 = new Client("Sara", "Ottawa", 1);
            c1.nodeIDConnectedTo = 1;
            clients.Add(c1);
            cli.Add(1, c1);
            Client c2 = new Client("John", "Toronto", 2);
            c2.nodeIDConnectedTo = 1;
            clients.Add(c2);
            cli.Add(2, c2);

            nodeManager.setClients(cli);

            Node node = new Node(1, "Ottawa", 1.0f, 1.0f, 1.0f);
            node.allConnectedClients = clients;
            Dictionary<uint, Node> dic = new Dictionary<uint, Node>();
            dic.Add(1, node);
            nodeManager.setNodes(dic);

            Assert.IsTrue(nodeManager.getNodes()[1].allConnectedClients.Count == 2);
            nodeManager.RemoveClient(2);
            Assert.IsTrue(nodeManager.getNodes()[1].allConnectedClients.Count == 1);

            // Client has been deleted from the system
        }
    }
}
