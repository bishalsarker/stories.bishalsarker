using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mySite.Models
{
    public class CategoryModel
    {
        public string cat_id { get; set; }
        public string cat_name { get; set; }
        public string response { get; set; }

        SqlConnection conn;
        Utility ut = new Utility();

        public CategoryModel()
        {
            Config db = new Config();
            conn = new SqlConnection(db.getConnStr());
        }
        private bool openConn()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        private bool closeConn()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        public void addCatagory()
        {
            string query = "insert into bs_category(cat_name) values('" + ut.encode(this.cat_name) + "')";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
        public CategoryModel getCatagory()
        {
            CategoryModel cat = new CategoryModel();
            string query = "select * from bs_category where cat_id='" + this.cat_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cat.cat_name = ut.decode(reader["cat_name"] + "");
                }

                reader.Close();
                closeConn();
                cat.response = "200";
            }
            else
            {
                cat.response = "500";
            }

            return cat;
        }
        public List<CategoryModel> getAllCatagory()
        {
            List<CategoryModel> catList = new List<CategoryModel>();
            string query = "select * from bs_category;";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    catList.Add(new CategoryModel
                    {
                        cat_id = reader["cat_id"] + "",
                        cat_name = ut.decode(reader["cat_name"] + "")
                    });
                }

                reader.Close();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }

            return catList;
        }
        public void updateCatagory()
        {
            string query = "update bs_category set cat_name='" + ut.encode(this.cat_name) + "' where cat_id='" + this.cat_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
        public void deleteCatagory()
        {
            string query = "delete from bs_category where cat_id='" + this.cat_id + "'";
            if (openConn() == true)
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                closeConn();
                this.response = "200";
            }
            else
            {
                this.response = "500";
            }
        }
    }
}