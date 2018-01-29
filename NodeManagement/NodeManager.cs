namespace NodeManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NodeManager
    {
        // Keep all the Node objects
        private Dictionary<uint, Node> _nodes;
        // Keep all the Client objects
        private Dictionary<uint, Client> _clients;

        private Queue<uint> removedIDs_clients;
        private Queue<uint> removedIDs_nodes;

        private float uploadUtilization_threshold;
        private float downloadUtilization_threshold;
        private float errorRate_threshold;
        private uint connectedClients_threshold;

        public NodeManager()
        {
            _nodes = new Dictionary<uint, Node>();
            _clients = new Dictionary<uint, Client>();
            removedIDs_clients = new Queue<uint>();
            removedIDs_nodes = new Queue<uint>();
            uploadUtilization_threshold = 1.0f;
            downloadUtilization_threshold = 1.0f;
            errorRate_threshold = 1.0f;
            connectedClients_threshold = 500;
        }

        public Dictionary<uint, Node> getNodes()
        {
            return _nodes;
        }

        public Dictionary<uint, Client> getClients()
        {
            return _clients;
        }

        public void setNodes(Dictionary<uint, Node> nodes)
        {
            _nodes = nodes;
        }

        public void setClients(Dictionary<uint, Client> client)
        {
            _clients = client;
        }



        public uint AddNewNodeToDataCenter(string cityName)
        {
            uint id = generateUniqueID(false);
            Node _node = new Node(id, cityName, uploadUtilization_threshold, downloadUtilization_threshold, errorRate_threshold);

            _nodes.Add(id, _node);

            return id;
        }

        public string RemoveNode(uint nodeID)
        {
            if (!_nodes.ContainsKey(nodeID))
                return "nodeID does not exist";

            _nodes.Remove(nodeID);
            removedIDs_nodes.Enqueue(nodeID);

            return "Node has been removed successfully";
        }

        public string UpdateNode(uint nodeID, string status)
        {
            if (!_nodes.ContainsKey(nodeID))
                return "nodeID does not exist";

            Node curNode = _nodes[nodeID];

            if (status == "online")
            {
                if (curNode.isOnline)
                    return "Node is already online.";
                else
                    curNode.setOnline(uploadUtilization_threshold, downloadUtilization_threshold, errorRate_threshold);
            }
            else
            {
                if (!curNode.isOnline)
                    return "Node is already offline.";
                else
                    curNode.setOffline();
            }

            return "Node got updated.";
        }

        public string UpdateMetricsAutomatically()
        {
            Node curNode;

            foreach (KeyValuePair<uint, Node> entry in _nodes)
            {
                curNode = entry.Value;
                curNode.SimulateRandomMetrics(new Random());
                AlarmIfThresholdsExceeded(curNode);
            }

            return "Metrics have been updated automatically.";
        }

        public void BroadcastTheStatusOfAllTheNodes()
        {
            Node curNode;

            foreach (KeyValuePair<uint, Node> entry in _nodes)
            {
                curNode = entry.Value;
                Console.WriteLine(curNode.ToString());
            }
        }

        private void AlarmIfThresholdsExceeded(Node node)
        {
            if (node.getDownloadUtilization() > downloadUtilization_threshold)
            {
                Console.WriteLine("Metrics have reached the maximum threshold. The node with id " + node.nodeId + " has been set to offline temporary");
                node.setOffline();
            }   
        }

        public string UpdateTheThresholds(float threshold_upload, float threshold_download, float threshold_ErrorRate, uint threshold_ConnectedClients)
        {
            uploadUtilization_threshold = threshold_upload;
            downloadUtilization_threshold = threshold_download;
            errorRate_threshold = threshold_ErrorRate;
            connectedClients_threshold = threshold_ConnectedClients;

            return "Thresholds have been updated.";
        }

        public string GetTheStatusOfTheNode(uint nodeID)
        {
            if (!_nodes.ContainsKey(nodeID))
                return "nodeID does not exist";

            Node curNode = _nodes[nodeID];

            if (curNode.isOnline)
            {
                int time = DateTime.Now.Minute - curNode.onlineTime.Minute;
                return "Node with id " + nodeID + " is online for last " + time + " minutes.";
            }
            else
                return "Node with id " + nodeID + " is currently offline.";

        }

        public uint AddNewClientToTheSystem(string clientName, string cityName, uint nodeID)
        {
            uint clientID = generateUniqueID(true);
            Client _client = new Client(clientName, cityName, clientID);

            bool clientHasBeenAdded = true;
            Node curNode;

            if (nodeID > 0)
            {
                if (_nodes.ContainsKey(nodeID))
                {
                    curNode = _nodes[nodeID];
                    if (curNode.allConnectedClients.Count < connectedClients_threshold)
                    {
                        _client.nodeIDConnectedTo = nodeID;
                        curNode.allConnectedClients.Add(_client);
                        _clients.Add(clientID, _client);
                    }
                    else
                    {
                        curNode = findFirstAvailableNode();
                        if (curNode != null)
                        {
                            _client.nodeIDConnectedTo = curNode.nodeId;
                            curNode.allConnectedClients.Add(_client);
                            _clients.Add(clientID, _client);
                            Console.WriteLine("The number of clients for the given node had reached the maximum threshold. The client had been added to the node with id " + curNode.nodeId);
                        }
                        else
                        {
                            Console.WriteLine("The Client cannot be added to none of the existing nodes. The client has not been added to the system");
                            clientHasBeenAdded = false;
                        }
                    }
                }
                else
                {
                    curNode = findFirstAvailableNode();
                    if (curNode != null)
                    {
                        _client.nodeIDConnectedTo = curNode.nodeId;
                        curNode.allConnectedClients.Add(_client);
                        _clients.Add(clientID, _client);
                        Console.WriteLine("The given nodeID does not exist. The client had been added to the node with id " + curNode.nodeId);
                    }
                    else
                    {
                        Console.WriteLine("The Client cannot be added to none of the existing nodes. The client has not been added to the system");
                        clientHasBeenAdded = false;
                    }
                }
            }
            else
            {
                curNode = findFirstAvailableNode();
                if (curNode != null)
                {
                    _client.nodeIDConnectedTo = curNode.nodeId;
                    curNode.allConnectedClients.Add(_client);
                    _clients.Add(clientID, _client);
                    Console.WriteLine("The client had been added to the node with id " + curNode.nodeId);
                }
                else
                {
                    Console.WriteLine("The Client cannot be added to none of the existing nodes. The client has not been added to the system");
                    clientHasBeenAdded = false;
                }
            }

            if (clientHasBeenAdded)
                return clientID;
            else
                return 0;
        }

        private Node findFirstAvailableNode()
        {
            Node firstAvailableNode = null;
            foreach (KeyValuePair<uint, Node> entry in _nodes)
            {
                if (entry.Value.allConnectedClients.Count() < connectedClients_threshold)
                {
                    firstAvailableNode = entry.Value;
                    break;
                }
            }

            return firstAvailableNode;
        }

        public string RemoveClient(uint clientID)
        {
            if (!_clients.ContainsKey(clientID))
                return "clientID does not exit";

            Client clientToBeRemoved = _clients[clientID];
            Node nodeToBeUpdated = _nodes[clientToBeRemoved.nodeIDConnectedTo];

            List<Client> list = nodeToBeUpdated.allConnectedClients;
            foreach (Client client in list)
            {
                if (client.getClientID() == clientID)
                {
                    list.Remove(client);
                    break;
                }
            }

            nodeToBeUpdated.allConnectedClients = list;
            _nodes[clientToBeRemoved.nodeIDConnectedTo] = nodeToBeUpdated;

            _clients.Remove(clientID);
            removedIDs_clients.Enqueue(clientID);

            return "Client has been removed successfully";
        }

        private uint generateUniqueID(bool client)
        {
            uint toReturn;

            if (client)
            {
                // If there is any ID belongs to one previously removed client, we want to reuse that ID
                if (removedIDs_clients.Count() > 0)
                    toReturn = removedIDs_clients.Dequeue();
                else
                    toReturn = (uint)_clients.Count() + 1;
            }

            else
            {
                // If there is any ID belongs to one previously removed node, we want to reuse that ID
                if (removedIDs_nodes.Count() > 0)
                    toReturn = removedIDs_nodes.Dequeue();
                else
                    toReturn = (uint)_nodes.Count() + 1;
            }

            return toReturn;
        }
    }
}
