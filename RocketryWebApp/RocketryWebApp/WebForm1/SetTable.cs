using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace RocketryWebApp.WebForm1
{
    public partial class WebForm1
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