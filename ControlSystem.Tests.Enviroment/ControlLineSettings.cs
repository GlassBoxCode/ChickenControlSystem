﻿using System.Net;

namespace ControlSystem.Tests.Enviroment
{
    public class ControlLineSettings
    {
        public string Ip { get; set; }
        public int Port { get; set; }

        public IPEndPoint GetEndPoint()
        {
            return new IPEndPoint(IPAddress.Parse(Ip), Port);
        }
    }
}