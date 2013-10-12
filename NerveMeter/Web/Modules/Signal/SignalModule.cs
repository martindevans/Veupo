using Nancy;
using NerveMeter.Data;

namespace NerveMeter.Web.Modules.Signal
{
    public class SignalModule
        :NancyModule
    {
        private readonly WebsocketObserver<SensorReading> _signal;
        private readonly WebsocketObserver<WaveletAnalysis> _wavelet;

        public SignalModule(WebsocketObserver<SensorReading> signal, WebsocketObserver<WaveletAnalysis> wavelet)
            :base("data/signal")
        {
            _signal = signal;
            _wavelet = wavelet;

            Get["/raw"] = RawSignal;
            Get["/raw/graph"] = GraphSignal;

            Get["/wavelet"] = WaveletSignal;
            Get["/wavelet/graph"] = GraphWaveletSignal;
        }

        private object RawSignal(object arg)
        {
            return View["RawData.cshtml", new { Endpoint = _signal.Endpoint }];
        }

        private object GraphSignal(object arg)
        {
            return "graph of raw signal";
        }

        private object WaveletSignal(object arg)
        {
            return View["RawData.cshtml", new { Endpoint = _wavelet.Endpoint }];
        }

        private object GraphWaveletSignal(object arg)
        {
            return "graph of wavelet analysis";
        }
    }
}
