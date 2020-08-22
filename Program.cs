using System;
using System.Threading.Tasks;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;

namespace NwtServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server().Wait();
        }

        static async Task Server()
        {
            IEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
            IEventLoopGroup workerGroup = new MultithreadEventLoopGroup();

            ServerBootstrap bootstrap = new ServerBootstrap();
            bootstrap.Group(bossGroup, workerGroup)
                    .Channel<TcpServerSocketChannel>()
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        channel.Pipeline.AddLast("echo", new ServerHandler());
                    }));

            IChannel boundChannel = await bootstrap.BindAsync(12345);

            Console.ReadLine();

            await boundChannel.CloseAsync();
        }
    }
}