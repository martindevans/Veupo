using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using NerveMeter.Data;
using NerveMeter.Web;
using Ninject;

namespace NerveMeter
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();

            IDisposable[] disposables = SetupDataCollection(kernel);

            WebCore.Start(kernel);

            IObservable<SensorReading> dataSource = null;

            Console.WriteLine("Press any key to escape");
            Console.Read();
        }

        private static IDisposable[] SetupDataCollection(IKernel kernel)
        {
            IObservable<SensorReading> rawSignal = null;
            IObservable<WaveletAnalysis> waveletAnalysis = null;
            IObservable<MotorUnitActionPotential> motorPotentials = null;

            List<IDisposable> disposables = new List<IDisposable>();

            WebsocketObserver<SensorReading> sensorSocket = new WebsocketObserver<SensorReading>(63471);
            //disposables.Add(rawSignal.Subscribe(sensorSocket));
            kernel.Bind<WebsocketObserver<SensorReading>>().ToConstant(sensorSocket);

            WebsocketObserver<WaveletAnalysis> waveletSocket = new WebsocketObserver<WaveletAnalysis>(63472);
            //disposables.Add(waveletAnalysis.Subscribe(waveletSocket));
            kernel.Bind<WebsocketObserver<WaveletAnalysis>>().ToConstant(waveletSocket);

            WebsocketObserver<MotorUnitActionPotential> motorSocket = new WebsocketObserver<MotorUnitActionPotential>(63473);
            //disposables.Add(motorPotentials.Subscribe(motorSocket));
            kernel.Bind<WebsocketObserver<MotorUnitActionPotential>>().ToConstant(motorSocket);

            return disposables.ToArray();
        }
    }
}
