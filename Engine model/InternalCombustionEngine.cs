namespace Engine_model
{
    public class InternalCombustionEngine
    {
        public double M;
        public double V;
        public readonly double I;
        public readonly double OverheatTemperature;
        public readonly double Hm;
        public readonly double Hv;
        public readonly double C;
        public readonly int[] StartM;
        public readonly int[] StartV;

        public InternalCombustionEngine(
            double m,
            double v,
            double i,
            double overheatTemperature,
            double hm,
            double hv,
            double c,
            int[] startM,
            int[] startV
        )
        {
            M = m;
            V = v;
            I = i;
            OverheatTemperature = overheatTemperature;
            Hm = hm;
            Hv = hv;
            C = c;
            StartM = startM;
            StartV = startV;
        }
    }
}