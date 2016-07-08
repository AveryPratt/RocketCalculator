namespace RocketryWebApp.Calculator
{
    public partial class CalculatorWebForm
    {
        private void setTable()
        {
            setRocketName();
            setFooterValues();
            setRowVisibility();
            deleteAboveRows(StageNumber);
            setColumnVisibility();
            determineCalculateButtonVisibility();
        }
    }
}