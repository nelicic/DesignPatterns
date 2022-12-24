namespace DesignPatterns.Proxy
{
    public interface ISubject
    {
        void Request();
    }

    public class RealSubject : ISubject
    {
        public void Request()
        {
            Console.WriteLine("RealSubject: Handling Request.");
        }
    }

    public class Proxy : ISubject
    {
        private RealSubject _realSubject;

        public Proxy(RealSubject realSubject)
        {
            _realSubject = realSubject;
        }

        public void Request()
        {
            if (CheckAccess())
            {
                _realSubject.Request();

                LogAccess();
            }
        }

        public bool CheckAccess()
        {
            Console.WriteLine("Proxy: Checking access prior to firing a real request.");

            return true;
        }

        public void LogAccess()
        {
            Console.WriteLine("Proxy: Logging the time of request.");
        }
    }

    public class Client
    {
        public void Main()
        {
            Console.WriteLine("Client: Executing the client code with a real subject:");
            RealSubject realSubject = new RealSubject();
            realSubject.Request();

            Console.WriteLine();

            Console.WriteLine("Client: Executing the same client code with a proxy:");
            Proxy proxy = new Proxy(realSubject);
            proxy.Request();
        }
    }
}

namespace DesignPatterns.ProxyV2
{
    class RobotBombDefuser
    {
        private Random _random = new Random();
        private int _robotConfiguredWavelength = 41;
        private bool _isConnected = false;

        public void ConnectWireless(int communicationWaveLength)
        {
            if (communicationWaveLength == _robotConfiguredWavelength)
                _isConnected = IsConnectedImmitatingConnectivityIssues();
        }

        public bool IsConnected()
        {
            _isConnected = IsConnectedImmitatingConnectivityIssues();
            return _isConnected;
        }

        private bool IsConnectedImmitatingConnectivityIssues()
        {
            return _random.Next(0, 10) < 4;
        }

        public virtual void WalkStraightForward(int steps)
        {
            Console.WriteLine("Did {0} steps forward...", steps);
        }

        public virtual void TurnRight()
        {
            Console.WriteLine("Turned right...");
        }

        public virtual void TurnLeft()
        {
            Console.WriteLine("Turned left...");
        }

        public virtual void DefuseBomb()
        {
            Console.WriteLine("Cut red or green or blue wire...");
        }
    }

    class RobotBombDefuserProxy : RobotBombDefuser
    { 
        private RobotBombDefuser _robotBombDefuser;
        private int _communicationWaveLength;
        private int _connectionAttemps = 3;

        public RobotBombDefuserProxy(int communicationWaveLength)
        {
            _robotBombDefuser = new RobotBombDefuser();
            _communicationWaveLength = communicationWaveLength;
        }

        public virtual void WalkStraightForward(int steps)
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.WalkStraightForward(steps);
        }

        public virtual void TurnRight()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.TurnRight();
        }

        public virtual void TurnLeft()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.TurnLeft();
        }

        public virtual void DefuseBomb()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.DefuseBomb();
        }

        private void EnsureConnectedWithRobot()
        {
            if (_robotBombDefuser == null)
            {
                _robotBombDefuser = new RobotBombDefuser();
                _robotBombDefuser.ConnectWireless(_communicationWaveLength);
            }

            for (int i = 0; i < _connectionAttemps; i++)
            {
                if (_robotBombDefuser.IsConnected() != true)
                    _robotBombDefuser.ConnectWireless(_communicationWaveLength);
                else
                    break;
            }

            if (_robotBombDefuser.IsConnected() != true)
                throw new Exception("No connection");
        }
    }

    public class Client
    {
        public void Main()
        {
            int opNum = 0;
            try
            {
                var proxy = new RobotBombDefuserProxy(41);
                proxy.WalkStraightForward(100);
                opNum++;
                proxy.TurnRight();
                opNum++;
                proxy.WalkStraightForward(5);
                opNum++;
                proxy.DefuseBomb();
                opNum++;
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception has been caught with message: ({0}). Decided to have human operate robot there.", e.Message);
               
                PlanB(opNum);
            }
        }

        private static void PlanB(int nextOperationNum)
        {
            RobotBombDefuser humanOperatingRobotDirectly = new RobotBombDefuser();
            if (nextOperationNum == 0)
            {
                humanOperatingRobotDirectly.WalkStraightForward(100);
                nextOperationNum++;
            }
            if (nextOperationNum == 1)
            {
                humanOperatingRobotDirectly.TurnRight();
                nextOperationNum++;
            }
            if (nextOperationNum == 2)
            {
                humanOperatingRobotDirectly.WalkStraightForward(5);
                nextOperationNum++;
            }
            if (nextOperationNum == 3)
            {
                humanOperatingRobotDirectly.DefuseBomb();
            }
        }
    }
}