# network-connection-guard
Resets your network adapter for you when it sees the connection goes down.

## Description and motive
For years I've thought it would be handy to have a simple app that disables and enables your network adapter when it notices your computer has lost connectivity to a certain host.  I've never found an app that does that so I got fed up and made one. 

In Nov 2015 a temporary, flaky point-to-point wifi connection was hindering our ability to work remotely. The time had come. Here it is.

## Configuration
To configure it for you there are two options to change:

**interfaceToCycle**: this is the name of your network adapter

**hostBeyondTheWall**: this is the ip address or hostname that you want to periodically check to see if the connection is stil good

You could edit **timeout** for the ping timeout and **waitBetweenChecks** as well but I've seen good results with those default values.
