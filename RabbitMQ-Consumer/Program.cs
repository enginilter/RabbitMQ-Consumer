using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


//RabbitMQ.Client NuGet Package ile install edilir.

//Bağlantı oluşturma
ConnectionFactory factory = new()
{
    UserName = "engin",
    Password = "engin",
    Port = AmqpTcpEndpoint.UseDefaultPort,
    HostName = "172.16.30.58",
    VirtualHost = "/"
};

//Bağlantıyı aktifleştirme
using IConnection connection = factory.CreateConnection();

//Kanal Oluşturma

using IModel channel = connection.CreateModel();


//Queue Oluşturma
channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, false, null);

//Mesaj okuma
EventingBasicConsumer consumer = new (channel);
//Hangi kuyruğu dinleyeceği
//Autoack: true ise mesajı okuduktan sonra otomatik olarak siler.
channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(" Mesaj Okundu {0}", message);
};

Console.Read();