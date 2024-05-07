using Producer.Services;

var service = new ServiceBusService("", "email", "response");

await service.StartSubscribingAsync();

while (true)
{
    Console.WriteLine("Enter your email: ");
    var enteredEmail = Console.ReadLine();

    if (enteredEmail != null)
    {
        await service.PublishAsync(enteredEmail);
    }

    else
    {
        Console.WriteLine("You didn't enter a valid email.");
    }
    
}