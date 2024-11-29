namespace KP
{
    public class Equation
    {
        public string EquationText { get; set; }
        public double InitialCondition { get; set; }

        public Equation(string equationText, double initialCondition)
        {
            EquationText = equationText;
            InitialCondition = initialCondition;
        }
    }
}