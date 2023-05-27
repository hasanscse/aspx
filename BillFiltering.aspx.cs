using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using IA.Provider;
using System.Net;
using System.IO;
using System.Data;
using DynamicSoft.Utility;

namespace EDU.UI.Admin.License
{
    public partial class BillFiltering : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //gridload();
                cmbCategoryBind();
                cmbSubCategoryBind();
                cmdCity();
               
            }    
            
        }
        private void gridload()
        {
            
            string CATEGORY_ID = cmbCategory.SelectedValue.ToString();
            string SUBCATEGORY_ID = cmbSubCategory.SelectedValue.ToString(); ;
            string ZONE_ID = cmbZone.SelectedValue.ToString(); ;
            string BILL_MONTH = DropDownList1.SelectedValue.ToString();


            if (chkAllCategory.Checked)
            {
                CATEGORY_ID = "";

            }
            if (chkAllSubCategory.Checked)
            {
                SUBCATEGORY_ID = "";

            }
            if (chkAllZone.Checked)
            {
                ZONE_ID = "";

            }
            if (chkAllDropDownList1.Checked)
            {
                BILL_MONTH = "";

            }            
            try
            {
                SqlConnection con = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand("SELECT*,  case BILL_PAID_STATUS when '1' Then 'Paid' When '0' Then 'Unpaid' END STATUS, case ACTIVE_STATUS when '1' Then 'Yes' When '0' Then 'No' END A_STATUS from CUSTOMER_INFO c INNER JOIN BILL b on c.CUSTOMER_ID = b.CUSTOMER_ID INNER JOIN CATEGORY ca on c.CATEGORY_ID=ca.CATEGORY_ID INNER JOIN SUBCATEGORY s on c.SUBCATEGORY_ID=s.SUBCATEGORY_ID INNER JOIN ZONE z on c.ZONE_ID= z.ZONE_ID "
                + " where ((ca.CATEGORY_ID=" + CATEGORY_ID + ") OR (" + CATEGORY_ID + "=''))"
             + " AND ((s.SUBCATEGORY_ID='" + SUBCATEGORY_ID + "') OR (" + SUBCATEGORY_ID + "='' ))"
             + "  AND ((z.ZONE_ID=" + ZONE_ID + ") OR (" + ZONE_ID + "='' ))"

            + "  AND ((b.BILL_MONTH='" + BILL_MONTH + "') OR (" + BILL_MONTH + "='')) ", con);
                con.Open();
                SqlDataReader rdr = objSqlCommand.ExecuteReader();
                dgbillfiltering.AutoGenerateColumns = false;
                dgbillfiltering.DataSource = rdr;
                dgbillfiltering.DataBind();
            }
            catch { }
        }
        private void cmbCategoryBind()
        {
            try
            {
                SqlConnection conn = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand("Select CATEGORY_ID, CATEGORY_NAME from CATEGORY order by CATEGORY_NAME ASC ");
                objSqlCommand.Connection = conn;
                conn.Open();
                cmbCategory.DataSource = objSqlCommand.ExecuteReader();
                cmbCategory.DataTextField = "CATEGORY_NAME";
                cmbCategory.DataValueField = "CATEGORY_ID";
                cmbCategory.DataBind();
                conn.Close();
                cmbCategory.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            catch { }
        }
        private void cmbSubCategoryBind()
        {
            try
            {
                SqlConnection conn = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand("Select SUBCATEGORY_ID, SUBCATEGORY_NAME from SUBCATEGORY order by SUBCATEGORY_NAME ASC ");
                objSqlCommand.Connection = conn;
                conn.Open();
                cmbSubCategory.DataSource = objSqlCommand.ExecuteReader();
                cmbSubCategory.DataTextField = "SUBCATEGORY_NAME";
                cmbSubCategory.DataValueField = "SUBCATEGORY_ID";
                cmbSubCategory.DataBind();
                conn.Close();
                cmbSubCategory.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            catch { }
        }
        protected void cmdCity()
        {
            try
            {

                // dopCity.DataSource = OtherFetch.GetAllCITY(Session["CUSTOMER_ID"].ToString());

                SqlConnection conn = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand("Select CITY_ID, CITY_NAME from CITY order by CITY_NAME ASC  ");


                //  objSqlCommand.CommandText = "FSP_CITY_GA";
                // objSqlCommand.CommandType = CommandType.StoredProcedure;



                objSqlCommand.Connection = conn;
                conn.Open();

                cmbCity.DataSource = objSqlCommand.ExecuteReader();
                cmbCity.DataTextField = "CITY_NAME";
                cmbCity.DataValueField = "CITY_ID";
                cmbCity.DataBind();
                conn.Close();
                cmbCity.Items.Insert(0, new ListItem("--Select--", "0"));


            }
            catch { }




        }
      
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int j = 0;
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int i = Convert.ToInt32(row.RowIndex);
                string BILL_NO = (dgbillfiltering.Rows[i].FindControl("lblBILL_NO") as Label).Text;
                SqlConnection con = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand();
                objSqlCommand = new SqlCommand("update  BILL set BILL_PAID_STATUS=1  where  BILL_NO ='" + BILL_NO + "' ", con);

                con.Open();
                j = objSqlCommand.ExecuteNonQuery();
                con.Close();
            }
            catch { }
            if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Successfully !');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Update Failed !!!');", true);

            }


            try
            {
                string imid = "";
                string my_charge = "";
                string contact_number = "";
                string bill_month = "";

                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int i = Convert.ToInt32(row.RowIndex);
                string BILL_NO = (dgbillfiltering.Rows[i].FindControl("lblBILL_NO") as Label).Text;

                SqlConnection conn = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand("Select c.*,b.* from CUSTOMER_INFO c inner join BILL b  on c.CUSTOMER_ID=b.CUSTOMER_ID  Where c.CUSTOMER_ID='" + BILL_NO + "' ");
                objSqlCommand.Connection = conn;
                conn.Open();
                using (SqlDataReader reader = objSqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        imid = reader["CUSTOMER_ID"].ToString();
                        my_charge = reader["MY_CHARGE"].ToString();
                        contact_number = reader["CONTACT_NUMBER"].ToString();
                        bill_month = reader["BILL_MONTH"].ToString();


                    }
                }



                conn.Close();


                if (j > 0)
                {

                    string success = "+OK";
                    int returnmessage = 0;
                    string requestUrl = "";

                    // string text = "প্রিয় গ্রাহক, দয়া করে বিকাশের মাধ্যমে " + txtLastDateOfPayment.Text + " এর মধ্যে আপনার সফ্টওয়্যার " + DropDownList1.Text+ " বিল পরিশোধ করুন। মাসিক চার্জ: " + txtAmount.Text + "। বিকাশে পেমেন্ট এ ০১৮৮৫৫৫৪২০০(মার্চেন্ট)  নম্বর টি ব্যবহার করুন এবং আপনার আইডি  " + txtIMID.Text + " লিখুন রেফারেন্সে হিসাবে। ইনভেন্টরি মাস্টার সফটওয়্যার ব্যবহার করার জন্য আপনাকে ধন্যবাদ। ";
                    string text12 = "Dear User,We have received Software  bill TK  for " + my_charge + "  against your User ID: " + imid + " for month " + bill_month + ".Thank you for using Inventory Master Software";
                    //"' + imid + '".

                    //    string text = "প্রিয় গ্রাহক, দয়া করে বিকাশের মাধ্যমে " + txtLastDateOfPayment.Text + " এর মধ্যে আপনার সফ্টওয়্যার  " + DropDownList1.Text + " বিল পরিশোধ করুন। মাসিক চার্জ: " + bill_amount + "। বিকাশে পেমেন্ট এ ০১৮৮৫৫৫৪২০০(মার্চেন্ট)  নম্বর টি ব্যবহার করুন এবং আপনার আইডি  " + CUSTOMER_ID + " লিখুন রেফারেন্সে হিসাবে। ইনভেন্টরি মাস্টার সফটওয়্যার ব্যবহার করার জন্য আপনাকে ধন্যবাদ। ";




                    string SMS_API_KEY = "HMTzRSI7jVaV5EMiGz5tx4Y42oA5+ShIGqLDxhLVAyk=";
                    string SENDER_ID = "8804445649527";
                    string CLIENT_ID = "ea8e4c4e-0607-44f5-b0c3-af19b0798bd8";
                    requestUrl = "https://api.smsq.global/api/v2/SendSMS?ApiKey=" + SMS_API_KEY + "&ClientId=" + CLIENT_ID + "&SenderId=" + SENDER_ID + "&Message=" + text12 + "&MobileNumbers=88" + contact_number + "&Is_Unicode=True&Is_Flash=False";

                    HttpWebRequest http = (HttpWebRequest)HttpWebRequest.Create(requestUrl);
                    HttpWebResponse response = (HttpWebResponse)http.GetResponse();
                    using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                    {
                        string responseJson = sr.ReadToEnd();
                        char[] whitespace = new char[] { ' ', '\t' };
                        string[] message = responseJson.Split(null);
                        string m = message[0];
                        if (m == success.ToUpper() || m == "OK")
                            returnmessage = 1;
                        ///////update database
                        ////// //update client credit
                        else
                            ////// //returnmessage = "Send Failed" + " " + SMS.MOBILENO;
                            returnmessage = 1;
                    }
                }

            }


            catch { }

            gridload();


        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string imid = "";
            string my_charge = "";
            string contact_number = "";
            string bill_month = "";
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int i = Convert.ToInt32(row.RowIndex);
            string BILL_NO = (dgbillfiltering.Rows[i].FindControl("lblBILL_NO") as Label).Text;


            try
            {
                SqlConnection conn = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(objSqlCommand);
                DataTable dt = new DataTable();
                objSqlCommand.CommandText = "RSP_BILL_INVOICE";
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.Add(new SqlParameter("@BILL_NO", SqlDbType.VarChar, 130));
                objSqlCommand.Parameters["@BILL_NO"].Value = BILL_NO;
                objSqlCommand.Connection = conn;
                conn.Open();
                da.Fill(dt);
                DSSessionUtility.DSSessionContainer.REPORT_DATA_CONTAINER = dt;
                DSSessionUtility.DSSessionContainer.REPORT_FILE_NAME = "MonthlyBillInvoice.rpt";
                conn.Close();
                Response.Write("<script>window.open ('../Reports/ReportViewPDF.aspx','_blank');</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccess", "javascript:ShowReportWindow()", true);

            }

            catch { }
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                GridViewRow row = (GridViewRow)btn.NamingContainer;
                int i = Convert.ToInt32(row.RowIndex);
                string BILL_NO = (dgbillfiltering.Rows[i].FindControl("lblBILL_NO") as Label).Text;
                SqlConnection conn = ConnectionClass.GetConnection();
                Label lbldeleteid = (Label)row.FindControl("BILL_NO");
                conn.Open();
                SqlCommand cmd = new SqlCommand("delete FROM BILL where BILL_NO='" + BILL_NO + "'", conn);
                int j = cmd.ExecuteNonQuery();
                conn.Close();
                // Binddrop();
                if (j > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Delete Successfully!!');", true);
                    //string vMsg = "Delete Successfully !!!!!";
                    //Response.Write("<script>alert('" + vMsg + "')</script>");
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Delete Failed !!!');", true);

                    //string vMsg = "Delete Failed !!!!!";
                    //Response.Write("<script>alert('" + vMsg + "')</script>");

                }
                gridload();
            }
            catch { }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {     
            gridload();
        }
        protected void btnPrintALL_Click(object sender, EventArgs e)
        {

            string CATEGORY_ID = cmbCategory.SelectedValue.ToString();
            string SUBCATEGORY_ID = cmbSubCategory.SelectedValue.ToString();
            string ZONE_ID = cmbZone.SelectedValue.ToString();
            string BILL_MONTH = DropDownList1.SelectedValue.ToString();
            string CITY_ID = cmbCity.SelectedValue.ToString();
            string BILL_PAID_STATUS = cmbBillStatus.SelectedValue.ToString();
            string ACTIVE_STATUS = cmbActiveStatus.SelectedValue.ToString();
            if (chkAllCategory.Checked)
            {
                CATEGORY_ID = "";

            }
            if (chkAllSubCategory.Checked)
            {
                SUBCATEGORY_ID = "";

            }
            if (chkAllCity.Checked)
            {
                CITY_ID = "";

            }
            if (chkAllZone.Checked)
            {
                ZONE_ID = "";

            }

            if (chkAllDropDownList1.Checked)
            {
                BILL_MONTH = "";

            }
            if (chkAllbillStatus.Checked)
            {
                BILL_PAID_STATUS = "";

            }
            if (chKAllas.Checked)
            {
                ACTIVE_STATUS = "";

            }


            try
            {


                SqlConnection conn = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(objSqlCommand);
                DataTable dt = new DataTable();
                objSqlCommand.CommandText = "RSP_BILL_INVOICE_LIST";
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.Add(new SqlParameter("@CATEGORY_ID", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@CATEGORY_ID"].Value = CATEGORY_ID;

                objSqlCommand.Parameters.Add(new SqlParameter("@SUBCATEGORY_ID", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@SUBCATEGORY_ID"].Value = SUBCATEGORY_ID;

                objSqlCommand.Parameters.Add(new SqlParameter("@CITY_ID", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@CITY_ID"].Value = CITY_ID;

                objSqlCommand.Parameters.Add(new SqlParameter("@ZONE_ID", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@ZONE_ID"].Value = ZONE_ID;
                
                objSqlCommand.Parameters.Add(new SqlParameter("@BILL_MONTH", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@BILL_MONTH"].Value = BILL_MONTH;

                objSqlCommand.Parameters.Add(new SqlParameter("@BILL_PAID_STATUS", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@BILL_PAID_STATUS"].Value = BILL_PAID_STATUS;

                objSqlCommand.Parameters.Add(new SqlParameter("@ACTIVE_STATUS", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@ACTIVE_STATUS"].Value = ACTIVE_STATUS;
                objSqlCommand.Connection = conn;
                conn.Open();
                da.Fill(dt);
                DSSessionUtility.DSSessionContainer.REPORT_DATA_CONTAINER = dt;
                DSSessionUtility.DSSessionContainer.REPORT_FILE_NAME = "MonthlyBillInvoiceList.rpt";
                conn.Close();
                Response.Write("<script>window.open ('../Reports/ReportViewPDF.aspx','_blank');</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccess", "javascript:ShowReportWindow()", true);

            }

            catch { }
        }
        protected void btnOutSideDhakaPrint_Click(object sender, EventArgs e)
        {

            string CATEGORY_ID = cmbCategory.SelectedValue.ToString();
            string SUBCATEGORY_ID = cmbSubCategory.SelectedValue.ToString();
            string ZONE_ID = cmbZone.SelectedValue.ToString();
            string BILL_MONTH = DropDownList1.SelectedValue.ToString();
            string CITY_ID = cmbCity.SelectedValue.ToString();
            string BILL_PAID_STATUS = cmbBillStatus.SelectedValue.ToString();
            string ACTIVE_STATUS = cmbActiveStatus.SelectedValue.ToString();
            

            if (chkAllCategory.Checked)
            {
                CATEGORY_ID = "";

            }
            if (chkAllSubCategory.Checked)
            {
                SUBCATEGORY_ID = "";

            }
            if (chkAllCity.Checked)
            {
                CITY_ID = "";

            }
            if (chkAllZone.Checked)
            {
                ZONE_ID = "";

            }

            if (chkAllDropDownList1.Checked)
            {
                BILL_MONTH = "";

            }
            if (chkAllbillStatus.Checked)
            {
                BILL_PAID_STATUS = "";

            }
            if (chKAllas.Checked)
            {
                ACTIVE_STATUS = "";

            }


            try
            {


                SqlConnection conn = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(objSqlCommand);
                DataTable dt = new DataTable();
                objSqlCommand.CommandText = "RSP_BILL_OUT_SIDE_INVOICE_LIST";
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.Add(new SqlParameter("@CATEGORY_ID", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@CATEGORY_ID"].Value = CATEGORY_ID;

                objSqlCommand.Parameters.Add(new SqlParameter("@SUBCATEGORY_ID", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@SUBCATEGORY_ID"].Value = SUBCATEGORY_ID;

                objSqlCommand.Parameters.Add(new SqlParameter("@CITY_ID", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@CITY_ID"].Value = CITY_ID;

                objSqlCommand.Parameters.Add(new SqlParameter("@ZONE_ID", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@ZONE_ID"].Value = ZONE_ID;

                objSqlCommand.Parameters.Add(new SqlParameter("@BILL_MONTH", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@BILL_MONTH"].Value = BILL_MONTH;

                objSqlCommand.Parameters.Add(new SqlParameter("@BILL_PAID_STATUS", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@BILL_PAID_STATUS"].Value = BILL_PAID_STATUS;

                objSqlCommand.Parameters.Add(new SqlParameter("@ACTIVE_STATUS", SqlDbType.VarChar, 25));
                objSqlCommand.Parameters["@ACTIVE_STATUS"].Value = ACTIVE_STATUS;
                objSqlCommand.Connection = conn;
                conn.Open();
                da.Fill(dt);
                DSSessionUtility.DSSessionContainer.REPORT_DATA_CONTAINER = dt;
                DSSessionUtility.DSSessionContainer.REPORT_FILE_NAME = "MonthlyBillInvoiceList.rpt";
                conn.Close();
                Response.Write("<script>window.open ('../Reports/ReportViewPDF.aspx','_blank');</script>");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccess", "javascript:ShowReportWindow()", true);

            }

            catch { }
        }
        protected void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int CITY_ID = Convert.ToInt32(cmbCity.SelectedValue);

                bind_zone(CITY_ID);


            }
            catch { }


        }

        private void bind_zone(int CITY_ID)
        {

            try
            {
                SqlConnection conn = ConnectionClass.GetConnection();
                SqlCommand objSqlCommand = new SqlCommand("Select* from ZONE  Where CITY_ID='" + CITY_ID + "' order by ZONE_NAME ASC");
                objSqlCommand.Connection = conn;
                conn.Open();
                cmbZone.Items.Clear();
                cmbZone.DataSource = objSqlCommand.ExecuteReader();
                cmbZone.DataTextField = "ZONE_NAME";
                cmbZone.DataValueField = "ZONE_ID";
                cmbZone.DataBind();
                conn.Close();
                cmbZone.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch { }

        }



    }
}