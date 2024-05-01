using Receiver;

var broker = new Broker("Receiver", "amqp://guest:guest@localhost:5672", "subscribe", "subscriber", "newsletter");

broker.Subscribe();
Console.ReadKey();