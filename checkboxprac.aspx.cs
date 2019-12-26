using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace checkbox
{
    public partial class checkboxprac : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["xyz"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                get_ddlcountry();
                BindData();
            }
        }

        public void get_ddlcountry()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_CountryBind", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            ddlcountry.DataValueField = "cid";
            ddlcountry.DataTextField = "cname";
            ddlcountry.DataSource = dt;
            ddlcountry.DataBind();
            ddlcountry.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        public void BindData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_classmate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();

            grd.DataSource = ds;
            grd.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string HOBS = "";
            for (int i = 0; i < chkhobbies.Items.Count; i++)
            {
                if (chkhobbies.Items[i].Selected == true)
                {
                    HOBS += chkhobbies.Items[i].Text + ",";
                }
            }

            HOBS = HOBS.TrimEnd(',');

            if (btnsave.Text == "Save")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_classmate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "insert");
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Age", txtage.Text);
                cmd.Parameters.AddWithValue("@Country", ddlcountry.SelectedValue);
                cmd.Parameters.AddWithValue("@Hobbies", HOBS);
                cmd.Parameters.AddWithValue("@Iagree", chkiagree.Checked == true ? 1 : 0);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_classmate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "update");
                cmd.Parameters.AddWithValue("@id", ViewState["PP"]);
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Age", txtage.Text);
                cmd.Parameters.AddWithValue("@Country", ddlcountry.SelectedValue);
                cmd.Parameters.AddWithValue("@Hobbies", HOBS);
                cmd.Parameters.AddWithValue("@Iagree", chkiagree.Checked == true ? 1 : 0);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            BindData();
        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "A")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_classmate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "delete");
                cmd.Parameters.AddWithValue("@id", e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                BindData();
            }

            else if(e.CommandName == "B")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_classmate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", "edit");
                cmd.Parameters.AddWithValue("@id", e.CommandArgument);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                txtname.Text = dt.Rows[0]["Name"].ToString();
                txtage.Text = dt.Rows[0]["Age"].ToString();
                ddlcountry.SelectedValue = dt.Rows[0]["Country"].ToString();
                string[] arr = dt.Rows[0]["hobbies"].ToString().Split(',');
                for (int i = 0; i < chkhobbies.Items.Count; i++)
                {
                    for (int j = 0; j < arr.Length; j++)
                    {
                        if (chkhobbies.Items[i].Text == arr[j])
                        {
                            chkhobbies.Items[i].Selected = true;
                            break;
                        }
                    }
                }
                btnsave.Text = "Update";

                ViewState["PP"] = e.CommandArgument;
            }
        }
    }
}