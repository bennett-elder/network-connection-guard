using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net;

namespace NetworkConnectionConsoleApplication1
{
    class Program
    {
        // name of your network adapter
        const string interfaceToCycle = "Ethernet";

        static void Main(string[] args)
        {
            // the host whose presence tells you whether your connection is down - normal people would probably just use 4.2.2.2
            const string hostBeyondTheWall = "4.2.2.2";

            // finding ip addy
            IPAddress[] addresslist = Dns.GetHostAddresses(hostBeyondTheWall);

            if (addresslist != null && addresslist.Length > 0)
            {
                IPAddress addy = addresslist[0];

                // start
                Console.WriteLine("Guarding the Realm...");
                Console.WriteLine("I care about " + addy.ToString());
                
                Ping pingSender = new Ping();

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);

                // Wait 10 seconds for a reply.
                int timeout = 5000;
                PingReply reply;
                int waitBetweenChecks = 30000;

                do
                {
                    try
                    {
                        reply = pingSender.Send(addy, timeout, buffer);
                        Console.WriteLine(reply.Status.ToString() + " at " + System.DateTime.Now.ToString());
                        if (reply.Status != IPStatus.Success)
                        {
                            Console.WriteLine("Cycling at " + System.DateTime.Now.ToString());
                            Cycle(interfaceToCycle);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Cycling at " + System.DateTime.Now.ToString());
                        Cycle(interfaceToCycle);
                    }

                    System.Threading.Thread.Sleep(waitBetweenChecks);
                } while (true);
                
            }
            else
            {
                Console.WriteLine("Host wasn't found at all");
            }
            
        }

        static void Cycle(string interfaceToCycle)
        {
            Disable(interfaceToCycle);
            System.Threading.Thread.Sleep(5000);
            Enable(interfaceToCycle);
        }

        static void Enable(string interfaceName)
        {
            System.Diagnostics.ProcessStartInfo psi =
                   new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" enable");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = psi;
            p.Start();
        }

        static void Disable(string interfaceName)
        {
            System.Diagnostics.ProcessStartInfo psi =
                new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = psi;
            p.Start();
        }
    }
}
