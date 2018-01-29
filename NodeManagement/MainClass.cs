namespace NodeManagement
{
    using System;

    public class MainClass
    {
        public static void Main(String[] args)
        {
            NodeManager managerCenter = new NodeManager();

            while (true)
            {
                uint id;
                string city, name;

                Console.WriteLine("Which action do you want to performe? Please enter the corresponding number of the action");
                Console.WriteLine("\t 1- Add a new Node");
                Console.WriteLine("\t 2- Remove an already existed Node");
                Console.WriteLine("\t 3- Add a Client to the system");
                Console.WriteLine("\t 4- Remove a Client from the system");
                Console.WriteLine("\t 5- Update an already existed Node");
                Console.WriteLine("\t 6- Update the threshold of the metrics");
                Console.WriteLine("\t 7- Get the status of a Node");
                Console.WriteLine("\t 8- Get the information of all the existing Nodes");

                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    
                    // To Add a New Node
                    case 1:
                        Console.WriteLine("To which city do you want to add a new Node?");
                        city = Console.ReadLine();
                        id = managerCenter.AddNewNodeToDataCenter(city);
                        Console.WriteLine("The ID of the Node you just added to the system is " + id);
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                    // To Remove a Node
                    case 2:
                        Console.WriteLine("What is the ID of the node you want to remove from the system?");
                        id = (uint) Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(managerCenter.RemoveNode(id));
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                    // To add a Client to the system
                    case 3:
                        Console.WriteLine("What is the full name of the client you want to add?");
                        name = Console.ReadLine();
                        Console.WriteLine("To which city the client belongs?");
                        city = Console.ReadLine();
                        Console.WriteLine("What is the ID of the node you want to add the client to? Please enter 0 if there is no restriction");
                        id = (uint) Convert.ToInt32(Console.ReadLine());
                        id = managerCenter.AddNewClientToTheSystem(name, city, id);
                        if (id > 0)
                            Console.WriteLine("The ID of the client you just added to the system is " + id);
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                    // To remove a client from the system
                    case 4:
                        Console.WriteLine("What is the ID of the client you want to remove from the system?");
                        id = (uint) Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(managerCenter.RemoveClient(id));
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                    // To update a Node
                    case 5:
                        Console.WriteLine("What is the ID of the node you want to update?");
                        id = (uint) Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("What do you want to do with the node with ID " + id + "? Please enter the corresponding number of the action");
                        Console.WriteLine("\t 1- Make it offline");
                        Console.WriteLine("\t 2- Make it online");
                        int number = Convert.ToInt32(Console.ReadLine());
                        string status = "online";
                        if (number == 1)
                            status = "offline";
                        Console.WriteLine(managerCenter.UpdateNode(id, status));
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                    // To change the threshold of the metrics
                    case 6:
                        Console.WriteLine("What do you want to set as the threshold for UploadUtilization?");
                        float threshold_upload = float.Parse(Console.ReadLine());
                        Console.WriteLine("What do you want to set as the threshold for DownloadUtilization?");
                        float threshold_download = float.Parse(Console.ReadLine());
                        Console.WriteLine("What do you want to set as the threshold for ErrorRate?");
                        float threshold_ErrorRate = float.Parse(Console.ReadLine());
                        Console.WriteLine("What do you want to set as the threshold for ConnectedClients?");
                        uint threshold_ConnectedClients = (uint) Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(managerCenter.UpdateTheThresholds(threshold_upload, threshold_download, threshold_ErrorRate, threshold_ConnectedClients));
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                    // To get the status of an already existed Node
                    case 7:
                        Console.WriteLine("What is the ID of the node you want to get the status of?");
                        id = (uint) Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine(managerCenter.GetTheStatusOfTheNode(id));
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                    // To see the status of all the existing nodes
                    case 8:
                        managerCenter.BroadcastTheStatusOfAllTheNodes();
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                    // To have metrics updated and to get all the Nodes up-to-date
                    default:
                        Console.WriteLine(managerCenter.UpdateMetricsAutomatically());
                        break;
                }
            }
        }
    }
}

