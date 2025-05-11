using Exer1.Datasets;
using Exer1.Datasets.OrderDatasetTableAdapters;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Exer1
{
	public partial class ReportForm : System.Web.UI.Page
	{
		private int orderId { get; set; }
        static bool _isSqlTypesLoaded = false;

		public ReportForm()
		{
            if (!_isSqlTypesLoaded)
            {
                SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~"));
                _isSqlTypesLoaded = true;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
		{
			orderId = Convert.ToInt32(Request.QueryString[0]);

			OrderDetailTableAdapter da = new OrderDetailTableAdapter();

			DataTable dt = new DataTable();
			dt.Merge(da.GetData(orderId));

			if (!IsPostBack)
			{
				ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reports/OrderReport.rdlc");
				ReportViewer1.LocalReport.DataSources.Clear();
				ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
				ReportViewer1.LocalReport.Refresh();
			}
			
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
			Response.Redirect("/MainForm.aspx");
        }
    }
}