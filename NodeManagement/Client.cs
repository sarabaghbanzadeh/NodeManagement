namespace NodeManagement
{
    public class Client
    {
        private string fullName;
        private string cityName;
        private uint clientID;

        public uint nodeIDConnectedTo;

        public Client (string fullName, string cityName, uint clientID)
        {
            this.fullName = fullName;
            this.cityName = cityName;
            this.clientID = clientID;
        }

        public string getFullName ()
        {
            return fullName;
        }

        public string getCityName ()
        {
            return cityName;
        }

        public uint getClientID ()
        {
            return clientID;
        }

        public void setFullName (string fullName)
        {
            this.fullName = fullName;
        }

        public void setCityName (string cityName)
        {
            this.cityName = cityName;
        }

        public void setClientID (uint clientID)
        {
            this.clientID = clientID;
        }
    }
}
