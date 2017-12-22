using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UDP;

namespace TestPOC.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public List<string> Get()
        {
            UdpClient udpClient = new UdpClient(8083);
          
                UdpClient client = new UdpClient();
                IPEndPoint ip = new IPEndPoint(IPAddress.Parse("10.170.84.163"), 8083);
                byte[] bytes = Encoding.ASCII.GetBytes("Send Data....");
                client.Send(bytes, bytes.Length, ip);

                //IPEndPoint object will allow us to read datagrams sent from any source.
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);
                List<string> someStringList = new List<string>();
                someStringList.Add("This is the message you received:- " + returnData.ToString());
                someStringList.Add("This message was sent from:- " +
                                            RemoteIpEndPoint.Address.ToString() +
                                            " on their port number " +
                                            RemoteIpEndPoint.Port.ToString());
                udpClient.Close();
                client.Close();
                return someStringList;
        }
    }
}
