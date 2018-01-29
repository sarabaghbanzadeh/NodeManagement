namespace NodeManagement
{
    using System;
    using System.Collections.Generic;

    public class Node
    {
        public List<Client> allConnectedClients;

        // Basic properties
        public uint nodeId;
        public string city;

        // State
        public DateTime onlineTime;
        public bool isOnline;

        // Metrics
        public float uploadUtilization;
        public float downloadUtilization;
        public float errorRate;
        public uint connectedClients;

        public Node(uint nodeId, string city, float up_threshold, float dl_threshold, float er_threshold)
        {
            allConnectedClients = new List<Client>();
            connectedClients = 0;

            this.nodeId = nodeId;
            this.city = city;

            isOnline = true;
            onlineTime = DateTime.Now;

            SimulateRandomMetrics(new Random());
            
            if (uploadUtilization > up_threshold)
                uploadUtilization = up_threshold;
            if (downloadUtilization > dl_threshold)
                downloadUtilization = dl_threshold;
            if (errorRate > er_threshold)
                errorRate = er_threshold;
        }

        private void ResetMetrics()
        {
            // Clear metrics back to 0. List of clients should not be changed.
            uploadUtilization = 0.0f;
            downloadUtilization = 0.0f;
            errorRate = 0.0f;
        }

        public void setOnline(float up_threshold, float dl_threshold, float er_threshold)
        {
            isOnline = true;
            onlineTime = DateTime.Now;
            SimulateRandomMetrics(new Random());

            if (uploadUtilization > up_threshold)
                uploadUtilization = up_threshold;
            if (downloadUtilization > dl_threshold)
                downloadUtilization = dl_threshold;
            if (errorRate > er_threshold)
                errorRate = er_threshold;
        }

        public void setOffline()
        {
            isOnline = false;
            ResetMetrics();
        }

        public float getUploadUtilization()
        {
            return uploadUtilization;
        }

        public float getDownloadUtilization()
        {
            return downloadUtilization;
        }

        public float getErrorRate()
        {
            return errorRate;
        }

        public void setErrorRate(float errorRate)
        {
            this.errorRate = errorRate;
        }

        public void setDownloadUtilization(float downloadUtilization)
        {
            this.downloadUtilization = downloadUtilization;
        }

        public void setUploadUtilization(float uploadUtilization)
        {
            this.uploadUtilization = uploadUtilization;
        }

        public void SimulateRandomMetrics(Random _rnd)
        {
            // Generate random values to simulate metrics.
            uploadUtilization = (float)_rnd.NextDouble();
            downloadUtilization = (float)_rnd.NextDouble();
            errorRate = (float)_rnd.NextDouble();
        }

        public override string ToString()
        {
            string toReturn = "Node with the id " + nodeId + " has the following status: \n";

            toReturn += "\t Has been located in " + city + "\n";

            if (isOnline)
            {
                int time = DateTime.Now.Minute - onlineTime.Minute;
                toReturn += "\t Is online for last " + time + " minutes. \n";
            }
            else
                toReturn += "\t Is currently oflline. \n";

            toReturn += "\t The value of uploadUtilization is " + uploadUtilization + "\n";
            toReturn += "\t The value of downloadUtilization is " + downloadUtilization + "\n";
            toReturn += "\t The value of errorRate is " + errorRate + "\n";
            toReturn += "\t The number of clients added to the node is " + allConnectedClients.Count + "\n";

            foreach (Client client in allConnectedClients)
                toReturn += "\t\t Client with ID " + client.getClientID() + " is connected to this node. \n";

            return toReturn;
        }
    }
}
