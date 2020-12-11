using System;
using System.Text.RegularExpressions;

namespace Engine_model
{
    public class TestStand
    {
        private static int _numberOfMandV;
        private static double _engineTemperature;
        private static double _ambientTemperature;
        private static int _runTime;
        private static InternalCombustionEngine _engine;
        private const int _maxTime = 1800;
        private const double _errorTime = 10e-2;

        public TestStand(InternalCombustionEngine engine)
        {
            _engine = engine;
            _engine.M = _engine.StartM[_numberOfMandV];
            _engine.V = _engine.StartV[_numberOfMandV];
            _engineTemperature = _ambientTemperature;
        }

        public void SetTemperatureData(int temperature)
        {
            _ambientTemperature = temperature;
        }

        public string GetTestData()
        {
            _runTime = StartEngine();
            return GetTimeData(_runTime);
        }

        private string GetTimeData(int time)
        {
            if (time >= _maxTime)
            {
                return "At this ambient temperature, the engine does not overheat.";
            }
            else
            {
                return "Time of engine overheating: " + time + " sec";
            }
        }

        private static int StartEngine()
        {
            var a = _engine.M * _engine.I;
            var eps = _engine.OverheatTemperature - _engineTemperature;
            var time = 0;
            while (eps > _errorTime && time < _maxTime)
            {
                time++;
                _engine.V += a;
                if (_numberOfMandV < _engine.StartM.Length - 2)
                    _numberOfMandV += _engine.V < _engine.StartV[_numberOfMandV + 1] ? 0 : 1;
                var up = _engine.V - _engine.StartV[_numberOfMandV];
                double down = _engine.StartV[_numberOfMandV + 1] - _engine.StartV[_numberOfMandV];
                double factor = _engine.StartM[_numberOfMandV + 1] - _engine.StartM[_numberOfMandV];
                _engine.M = up / down * factor + _engine.StartM[_numberOfMandV];
                var Vh = _engine.M * _engine.Hm * _engine.V * _engine.V * _engine.Hv;
                var Vc = _engine.C * (_ambientTemperature - _engineTemperature);
                _engineTemperature += Vc + Vh;

                a = _engine.M * _engine.I;

                eps = _engine.OverheatTemperature - _engineTemperature;
            }

            return time;
        }
    }
}